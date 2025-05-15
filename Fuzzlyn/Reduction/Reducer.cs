using Fuzzlyn.ExecutionServer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Reduction;

internal class Reducer(ExecutionServerPool pool, Compiler compiler, CompilerOptions baseCompilerOpts, CompilerOptions diffCompilerOpts, CompilationUnitSyntax original, ulong reducerSeed, string reduceDebugGitDir)
{
    private CompilerOptions _baseCompilerOpts = baseCompilerOpts;
    private CompilerOptions _diffCompilerOpts = diffCompilerOpts;
    private readonly ExecutionServerPool _pool = pool;
    private readonly Compiler _compiler = compiler;
    private readonly Rng _rng = Rng.FromSplitMix64Seed(reducerSeed);
    private int _varCounter;
    private readonly string _reduceDebugGitDir = reduceDebugGitDir;
    private readonly Stopwatch _timer = new();
    private int _serverTimeouts;
    private int _serverCrashes;
    private TimeSpan _nextUpdate;

    public CompilationUnitSyntax Original { get; } = original;
    public CompilationUnitSyntax Reduced { get; private set; }

    public CompilationUnitSyntax Reduce()
    {
        _timer.Restart();

        CompileResult @base = _compiler.Compile(Original, _baseCompilerOpts);
        CompileResult diff = _compiler.Compile(Original, _diffCompilerOpts);

        Func<CompilationUnitSyntax, bool> isInteresting;
        if (@base.RoslynException != null || diff.RoslynException != null)
        {
            CompilerOptions opts = @base.RoslynException != null ? _baseCompilerOpts : _diffCompilerOpts;
            isInteresting = program => _compiler.Compile(program, opts).RoslynException != null;
        }
        else if (@base.CompileErrors.Length > 0 || diff.CompileErrors.Length > 0)
        {
            CompilerOptions opts = @base.CompileErrors.Length > 0 ? _baseCompilerOpts : _diffCompilerOpts;
            isInteresting = program =>
            {
                CompileResult recompiled = _compiler.Compile(program, opts);
                if (recompiled.CompileErrors.Length <= 0)
                    return false;

                return recompiled.CompileErrors[0].Id == (@base.CompileErrors.Length > 0 ? @base.CompileErrors[0] : diff.CompileErrors[0]).Id;
            };
        }
        else
        {
            var origPair = new ProgramPair(false, @base.Assembly, diff.Assembly);
            RunSeparatelyResults targetResults = _pool.RunPairOnPool(origPair, TimeSpan.FromSeconds(20), false);

            if (targetResults.Kind == RunSeparatelyResultsKind.Timeout)
            {
                // Currently some programs take so long that we cannot really differentiate between "hang" and "still executing".
                // So we do not allow to reduce programs that time out.
                throw new InvalidOperationException("Program times out");
            }

            if (targetResults.Kind == RunSeparatelyResultsKind.Success)
            {
                ProgramPairResults origResults = targetResults.Results;
                if (origResults.BaseResult.Kind != ProgramResultKind.HitsJitAssert &&
                    origResults.DiffResult.Kind != ProgramResultKind.HitsJitAssert &&
                    origResults.BaseResult.Checksum == origResults.DiffResult.Checksum &&
                    origResults.BaseResult.ExceptionType == origResults.DiffResult.ExceptionType)
                {
                    throw new InvalidOperationException("Program has no errors");
                }
            }

            isInteresting = prog =>
            {
                RunSeparatelyResults results = CompileAndRun(prog, trackOutput: false, keepPoolNonEmptyEagerly: targetResults.Kind == RunSeparatelyResultsKind.Crash);
                if (results == null)
                    return false;

                if (results.Kind == RunSeparatelyResultsKind.Timeout)
                {
                    _serverTimeouts++;
                    return false;
                }

                if (results.Kind == RunSeparatelyResultsKind.Crash)
                    _serverCrashes++;

                if (targetResults.Kind == RunSeparatelyResultsKind.Crash)
                {
                    // If we are looking for crash, then we can return a result immediately
                    return results.Kind == RunSeparatelyResultsKind.Crash;
                }

                Debug.Assert(targetResults.Kind == RunSeparatelyResultsKind.Success);

                if (results.Kind != RunSeparatelyResultsKind.Success)
                {
                    return false;
                }

                // If we are looking for a JIT assert, then ensure we got a JIT assert.
                if (targetResults.Results.DiffResult.Kind == ProgramResultKind.HitsJitAssert)
                {
                    return results.Results.DiffResult.Kind == ProgramResultKind.HitsJitAssert &&
                           JitAssertsEqual(results.Results.DiffResult.JitAssertError, targetResults.Results.DiffResult.JitAssertError);
                }

                if (targetResults.Results.BaseResult.Kind == ProgramResultKind.HitsJitAssert)
                {
                    return results.Results.BaseResult.Kind == ProgramResultKind.HitsJitAssert &&
                           JitAssertsEqual(results.Results.BaseResult.JitAssertError, targetResults.Results.BaseResult.JitAssertError);
                }

                if (targetResults.Results.BaseResult.ExceptionType != targetResults.Results.DiffResult.ExceptionType)
                {
                    // Original example throws different exceptions in debug and release.
                    // Make sure that 1) the new results throws/runs successfully in the same situations
                    // (e.g. we do not want to suddenly find an assert here) and 2) that the same or no
                    // exception is thrown.
                    bool hasSameResultKinds =
                        results.Results.BaseResult.Kind == targetResults.Results.BaseResult.Kind &&
                        results.Results.DiffResult.Kind == targetResults.Results.DiffResult.Kind;

                    bool throwsSameExceptions =
                        results.Results.BaseResult.ExceptionType == targetResults.Results.BaseResult.ExceptionType &&
                        results.Results.DiffResult.ExceptionType == targetResults.Results.DiffResult.ExceptionType;

                    return hasSameResultKinds && throwsSameExceptions;
                }

                // Original example throws same exception in debug and release, so it is the checksum that differs.
                return results.Results.BaseResult.Checksum != results.Results.DiffResult.Checksum;
            };
        }

        // Save original comments as simplification may remove it by removing an unnecessary type.
        SyntaxTriviaList originalTrivia = Original.GetLeadingTrivia();
        UpdateReduced("Removing header comments", Original.WithLeadingTrivia());
        CoarseSimplify(isInteresting);

        List<(string, IEnumerator<SyntaxNode>)> simplifierEnumerators = new();
        bool first = true;
        bool any = true;
        while (any)
        {
            any = false;
            while (true)
            {
                if (!SimplifyFromList("Statements", Reduced.DescendantNodes().Where(n => n is StatementSyntax).ToList()))
                    break;
                any = true;
            }
            FinishConsoleUpdates();

            while (true)
            {
                if (!SimplifyFromList("Expressions", Reduced.DescendantNodes().Where(n => n is ExpressionSyntax).ToList()))
                    break;
                any = true;
            }
            FinishConsoleUpdates();

            while (true)
            {
                List<SyntaxNode> members =
                    Reduced.DescendantNodesAndSelf().Where(n => n is MemberDeclarationSyntax || n is CompilationUnitSyntax).ToList();

                if (!SimplifyFromList("Members", members))
                    break;
                any = true;
            }
            FinishConsoleUpdates();

            first = false;

            bool SimplifyFromList(string simplifyType, List<SyntaxNode> list)
            {
                void Update(int index, bool force)
                {
                    string crashesString = "";
                    if (_serverCrashes > 0)
                        crashesString = $" Server crashes: {_serverCrashes}.";

                    string timeoutsString = "";
                    if (_serverTimeouts > 0)
                        timeoutsString = $" Server timeouts: {_serverTimeouts}.";

                    WriteUpdateToConsole($"\rSimplifying {simplifyType}. Total elapsed: {_timer.Elapsed:hh\\:mm\\:ss}.{crashesString}{timeoutsString} Iter: {index}/{list.Count}", force);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    Update(i + 1, false);

                    // Fisher-Yates shuffle.
                    int nodeIndex = _rng.Next(i, list.Count);
                    SyntaxNode node = list[nodeIndex];
                    list[nodeIndex] = list[i];

                    // Do not optimize checksum args and call itself.
                    // We still want to remove these statements, however, so we focus on the expression only.
                    InvocationExpressionSyntax invocParent = node.FirstAncestorOrSelf<InvocationExpressionSyntax>();
                    if (invocParent != null && IsChecksumCall(invocParent))
                        continue;

                    try
                    {
                        GetSimplifierEnumerators(node, !first, simplifierEnumerators);

                        while (simplifierEnumerators.Count > 0)
                        {
                            for (int j = 0; j < simplifierEnumerators.Count; j++)
                            {
                                // Avoid blowing the variable counts up too much. Save it so we can restore it
                                // if the new node is not interesting.
                                int origVarCounter = _varCounter;

                                (string name, IEnumerator<SyntaxNode> enumerator) = simplifierEnumerators[j];
                                if (!enumerator.MoveNext())
                                {
                                    enumerator.Dispose();
                                    simplifierEnumerators.RemoveAt(j);
                                    j--;
                                    continue;
                                }

                                CompilationUnitSyntax candidate = Reduced.ReplaceNode(node, enumerator.Current);
                                if (isInteresting(candidate))
                                {
                                    UpdateReduced($"Simplify with {name}", candidate, true);
                                    return true;
                                }

                                _varCounter = origVarCounter;
                            }
                        }
                    }
                    finally
                    {
                        foreach (var (_, enumerator) in simplifierEnumerators)
                            enumerator.Dispose();

                        simplifierEnumerators.Clear();
                    }
                }

                Update(list.Count, true);
                return false;
            }
        }

        List<SyntaxTrivia> outputComments = GetOutputComments(@base, diff).Select(Comment).ToList();

        MakeStandalone();
        double oldSizeKiB = Original.NormalizeWhitespace().ToString().Length / 1024.0;
        double newSizeKiB = Reduced.NormalizeWhitespace().ToString().Length / 1024.0;
        string sizeComment =
            FormattableString.Invariant(
                $"// Reduced from {oldSizeKiB:F1} KiB to {newSizeKiB:F1} KiB in {_timer.Elapsed:hh\\:mm\\:ss}");

        SyntaxTriviaList newTrivia = originalTrivia.Add(Comment(sizeComment)).AddRange(outputComments);

        UpdateReduced("Add header comments", Reduced.WithLeadingTrivia(newTrivia));

        return Reduced;
    }

    private readonly Regex _numberRegex = new Regex(@"[0-9A-Fa-f]+", RegexOptions.Compiled);

    private bool JitAssertsEqual(string left, string right)
    {
        return _numberRegex.Replace(left, "X") == _numberRegex.Replace(right, "X");
    }

    private bool _wroteUpdate;
    private void WriteUpdateToConsole(string text, bool force)
    {
        if (_timer.Elapsed < _nextUpdate && !force)
            return;

        _nextUpdate = _timer.Elapsed + TimeSpan.FromMilliseconds(500);

        if (Console.IsOutputRedirected)
            return;

        Console.Write("\r" + text.PadRight(Console.BufferWidth));
        _wroteUpdate = true;
    }

    private void FinishConsoleUpdates()
    {
        if (!Console.IsOutputRedirected && _wroteUpdate)
            Console.WriteLine();

        _wroteUpdate = false;
    }

    private RunSeparatelyResults CompileAndRun(CompilationUnitSyntax prog, bool trackOutput, bool keepPoolNonEmptyEagerly)
    {
        CompileResult progBase = _compiler.Compile(prog, _baseCompilerOpts);
        CompileResult progDiff = _compiler.Compile(prog, _diffCompilerOpts);

        if (progBase.Assembly == null || progDiff.Assembly == null)
            return null;

        ProgramPair pair = new(trackOutput, progBase.Assembly, progDiff.Assembly);
        RunSeparatelyResults results = _pool.RunPairOnPool(pair, TimeSpan.FromSeconds(20), keepPoolNonEmptyEagerly);
        return results;
    }

    private void UpdateReduced(string update, CompilationUnitSyntax newReduced, bool checkNew = false)
    {
        Trace.Assert(!checkNew || newReduced != Reduced);
        Reduced = newReduced;
        if (_reduceDebugGitDir == null)
            return;

        string path = Path.Combine(_reduceDebugGitDir, "example.cs");
        if (!Directory.Exists(_reduceDebugGitDir))
        {
            Directory.CreateDirectory(_reduceDebugGitDir);
            Git("init");
            File.WriteAllText(path, Original.NormalizeWhitespace().ToFullString());
            Git("add .");
            Git("commit -m Original");
        }

        File.WriteAllText(path, newReduced.NormalizeWhitespace().ToFullString());
        Git("add .");
        Git($"commit --allow-empty -m \"{update}\"");

        void Git(string cmd)
        {
            using (Process process = new())
            {
                ProcessStartInfo startInfo = new()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "git",
                    Arguments = cmd,
                    WorkingDirectory = _reduceDebugGitDir,
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
        }
    }


    /// <summary>
    /// Perform coarse simplification by removing statements with a binary search.
    /// </summary>
    private void CoarseSimplify(Func<CompilationUnitSyntax, bool> isInteresting)
    {
        // Step 1: Declare all non-ref variables at the start of the methods with a default value.
        // This will hopefully allow us to remove more assignments than otherwise.
        Dictionary<SyntaxNode, SyntaxNode> replacements = new();

        foreach (MethodDeclarationSyntax method in Reduced.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            List<LocalDeclarationStatementSyntax> decls =
                method.DescendantNodes()
                .OfType<LocalDeclarationStatementSyntax>()
                .Where(decl => decl.Declaration.Variables.Count == 1 &&
                               decl.Declaration.Variables[0] != null &&
                               !(decl.Declaration.Type is RefTypeSyntax))
                .ToList();

            if (decls.Count == 0)
                continue;

            Dictionary<SyntaxNode, SyntaxNode> assignmentReplacements = new();
            foreach (LocalDeclarationStatementSyntax decl in decls)
            {
                assignmentReplacements.Add(
                    decl,
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(decl.Declaration.Variables[0].Identifier),
                                decl.Declaration.Variables[0].Initializer.Value)));
            }

            MethodDeclarationSyntax withoutLocals =
                method.ReplaceNodes(
                    assignmentReplacements.Keys,
                    (orig, _) => assignmentReplacements[orig]);

            IEnumerable<LocalDeclarationStatementSyntax> newDecls =
                decls
                .Select(
                    d => d.ReplaceNode(
                        d.Declaration.Variables[0].Initializer.Value,
                        DefaultExpression(d.Declaration.Type)));

            MethodDeclarationSyntax withLocalsAtTop =
                withoutLocals.WithBody(
                    Block(
                        withoutLocals.Body.Statements.InsertRange(
                            0,
                            newDecls)));

            replacements.Add(method, withLocalsAtTop);
        }

        CompilationUnitSyntax localsMoved = Reduced.ReplaceNodes(replacements.Keys, (orig, _) => replacements[orig]);
        if (isInteresting(localsMoved))
            UpdateReduced("Move locals up", localsMoved);

        // Step 2: Remove by binary searches. Prefer large ones first to remove as much as fast as possible.
        List<(string TypeName, string MethodName)> names =
            Reduced.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(m => m.Body != null)
            .OrderByDescending(m => m.DescendantNodes().Count())
            .Select(m => (((TypeDeclarationSyntax)m.Parent).Identifier.Text, m.Identifier.Text))
            .ToList();

        void Update(int index, bool force)
        {
            WriteUpdateToConsole($"Simplifying Coarsely. Total elapsed: {_timer.Elapsed:hh\\:mm\\:ss}. Method {index}/{names.Count}.", force);
        }

        for (int i = 0; i < names.Count; i++)
        {
            bool isMethodInteresting(MethodDeclarationSyntax orig, MethodDeclarationSyntax @new)
                => isInteresting(Reduced.ReplaceNode(orig, @new));

            Update(i + 1, false);
            var method = Reduced.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => ((TypeDeclarationSyntax)m.Parent).Identifier.Text == names[i].TypeName && m.Identifier.Text == names[i].MethodName);
            var newMethod = (MethodDeclarationSyntax)new CoarseStatementRemover(isMethodInteresting).Visit(method);
            UpdateReduced($"Bulk remove statements in {names[i].TypeName}.{names[i].MethodName}", Reduced.ReplaceNode(method, newMethod));
        }
        Update(names.Count, true);
        FinishConsoleUpdates();
    }

    private IEnumerable<string> GetOutputComments(CompileResult ogBaseCompile, CompileResult ogDiffCompile)
    {
        if (ogBaseCompile.RoslynException != null)
        {
            yield return $"// Roslyn throws '{ogBaseCompile.RoslynException.GetType()}' when compiling in debug";
            yield break;
        }
        if (ogDiffCompile.RoslynException != null)
        {
            yield return $"// Roslyn throws '{ogDiffCompile.RoslynException.GetType()}' when compiling in release";
            yield break;
        }

        if (ogBaseCompile.CompileErrors.Length > 0 || ogDiffCompile.CompileErrors.Length > 0)
        {
            CompilerOptions compileOpts =
                ogBaseCompile.CompileErrors.Length > 0 ? _baseCompilerOpts : _diffCompilerOpts;

            CompileResult result = _compiler.Compile(Reduced.NormalizeWhitespace(), compileOpts);
            yield return $"// Roslyn gives '{result.CompileErrors[0]}'";
            yield break;
        }

        RunSeparatelyResults results = CompileAndRun(Reduced, trackOutput: true, keepPoolNonEmptyEagerly: false);
        if (results == null)
        {
            yield return $"// Unexpected compiler error";
            yield break;
        }

        IEnumerable<string> Lines(string message) => message.Replace("\r", "").Split('\n');

        switch (results.Kind)
        {
            case RunSeparatelyResultsKind.Crash:
                if (!string.IsNullOrWhiteSpace(results.CrashError))
                {
                    yield return $"// Exits with error:";
                    foreach (string line in Lines(results.CrashError))
                        yield return $"// {line}";

                    yield break;
                }

                yield return $"// Crashes the runtime";
                yield break;
            case RunSeparatelyResultsKind.Timeout:
                yield return $"// Times out";
                yield break;
            case RunSeparatelyResultsKind.Success:
                ProgramPairResults pairResult = results.Results;
                if (pairResult.DiffResult.Kind == ProgramResultKind.HitsJitAssert)
                {
                    yield return $"// Hits JIT assert for {_diffCompilerOpts.Name}:";
                    foreach (string line in Lines(pairResult.DiffResult.JitAssertError).SkipWhile(l => l == "JIT assert failed:"))
                        yield return $"// {line}";

                    yield break;
                }

                if (pairResult.BaseResult.Kind == ProgramResultKind.HitsJitAssert)
                {
                    yield return $"// Hits JIT assert for {_baseCompilerOpts.Name}:";
                    foreach (string line in Lines(pairResult.BaseResult.JitAssertError).SkipWhile(l => l == "JIT assert failed:"))
                        yield return $"// {line}";

                    yield break;
                }

                yield return $"// {_baseCompilerOpts.Name}: {FormatResult(pairResult.BaseResult, pairResult.BaseFirstUnmatch)}";
                yield return $"// {_diffCompilerOpts.Name}: {FormatResult(pairResult.DiffResult, pairResult.DiffFirstUnmatch)}";
                break;

                string FormatResult(ProgramResult result, ChecksumSite unmatch)
                {
                    if (pairResult.BaseResult.ExceptionType != pairResult.DiffResult.ExceptionType)
                    {
                        if (result.ExceptionType != null)
                            return $"Throws '{result.ExceptionType}'";

                        return "Runs successfully";
                    }

                    if (pairResult.BaseResult.NumChecksumCalls != pairResult.DiffResult.NumChecksumCalls)
                        return $"Prints {result.NumChecksumCalls} line(s)";

                    if (unmatch != null)
                        return $"Outputs {unmatch.Value}";

                    return "";
                }
        }
    }

    private void MakeStandalone()
    {
        // At this point we have a program like the following:
        // public class Program
        // {
        //     public static Fuzzlyn.ExecutionServer.IRuntime s_rt;
        //     public static bool s_2;
        //     public static S0 s_4;
        //     public static uint[] s_5;
        //     public static byte s_6;
        //     public static S0[] s_10;
        //     public static S0[, ] s_20 = new S0[, ]{{new S0()}};
        //     public static void Main(Fuzzlyn.ExecutionServer.IRuntime rt)
        //     {
        //         s_rt = rt;
        //         S0 vr2 = default(S0);
        //         new S0().M4(vr2.F2, vr2.M6(ref s_4.F2, s_2, ref s_20[0, 0], ref s_4.F3));
        //     }
        // 
        //     public static void M8(S0 arg0, long arg1)
        //     {
        //         Program.s_rt.Checksum("c_45", arg1);
        //     }
        // 
        //     public static void M7(S0 argThis, uint[] arg0, ref byte arg1, ref S0[] arg2, int arg3)
        //     {
        //     }
        // }
        //
        // we want to create a standalone example that reproduces the issue. We will start out
        // with an example that most matches how Fuzzlyn works by creating a matching interface
        // and adding a wrapper Main function that calls the program in a collectible ALC.

        CompilationUnitSyntax prog = Reduced;

        prog = prog.AddMembers(
            ParseMemberDeclaration(@"
public interface IRuntime
{
    void WriteLine<T>(string site, T value);
}"),
            ParseMemberDeclaration(@"
public class Runtime : IRuntime
{
    public void WriteLine<T>(string site, T value) => System.Console.WriteLine(value);
}"),

            ParseMemberDeclaration(@"
public class CollectibleALC : System.Runtime.Loader.AssemblyLoadContext
{
    public CollectibleALC() : base(true) { }
}"));

        // Add the new main method
        MemberDeclarationSyntax newMainMethod =
            ParseMemberDeclaration(@"
public static void Main()
{
    CollectibleALC alc = new CollectibleALC();
    System.Reflection.Assembly asm = alc.LoadFromAssemblyPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
    System.Reflection.MethodInfo mi = asm.GetType(typeof(Program).FullName).GetMethod(nameof(MainInner));
    System.Type runtimeTy = asm.GetType(typeof(Runtime).FullName);
    mi.Invoke(null, new object[] { System.Activator.CreateInstance(runtimeTy) });
}");

        MethodDeclarationSyntax oldMainMethod =
            prog.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => m.Identifier.Text == "Main");

        // Change the arg from Fuzzlyn.ExecutionServer.IRuntime rt => IRuntime rt
        MethodDeclarationSyntax newInnerMainMethod =
            oldMainMethod
            .WithIdentifier(Identifier("MainInner"))
            .WithParameterList(ParseParameterList("(IRuntime rt)"));

        prog = prog.ReplaceNode(oldMainMethod, [newMainMethod, newInnerMainMethod]);

        // Now change references of Checksum -> WriteLine and Fuzzlyn.ExecutionServer.IRuntime => IRuntime
        Dictionary<SyntaxNode, SyntaxNode> replacements = new();
        foreach (SyntaxNode node in prog.DescendantNodes())
        {
            // Change type of s_rt field
            if (node is FieldDeclarationSyntax field && field.Declaration.Variables.Count == 1 &&
                field.Declaration.Variables[0].Identifier.Text == "s_rt")
            {
                replacements.Add(node, ParseMemberDeclaration("public static IRuntime s_rt;"));
                continue;
            }

            // Convert s_rt.Checksum calls to s_rt.WriteLine
            if (node is ExpressionStatementSyntax expStmt &&
                expStmt.Expression is InvocationExpressionSyntax invoc &&
                invoc.Expression is MemberAccessExpressionSyntax mem)
            {
                string id = mem.Name.Identifier.Text;
                switch (id)
                {
                    case "Checksum":
                        break;
                    case "ChecksumSingle":
                    case "ChecksumDouble":
                        ExpressionSyntax floatOrDouble = invoc.ArgumentList.Arguments[^1].Expression;
                        ExpressionSyntax bits =
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("System"),
                                        IdentifierName("BitConverter")),
                                    IdentifierName(id == "ChecksumSingle" ? "SingleToUInt32Bits" : "DoubleToUInt64Bits")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            floatOrDouble))));
                        replacements.Add(floatOrDouble, bits);
                        break;
                    case "ChecksumSingles":
                    case "ChecksumDoubles":
                        ExpressionSyntax vectorFloatOrDouble = invoc.ArgumentList.Arguments[^1].Expression;
                        ExpressionSyntax vectorBits =
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    vectorFloatOrDouble,
                                    IdentifierName(id == "ChecksumSingles" ? "AsUInt32" : "AsUInt64")));
                        replacements.Add(vectorFloatOrDouble, vectorBits);
                        break;
                    default:
                        continue;
                };

                replacements.Add(mem.Name, IdentifierName("WriteLine"));
            }
        }

        prog = prog.ReplaceNodes(replacements.Keys, (orig, _) => replacements[orig]);

        bool TryReplacement(string update, Func<CompilationUnitSyntax, CompilationUnitSyntax> reducer)
        {
            CompilationUnitSyntax newNode = reducer(Reduced);
            if (IsStandaloneProgramInteresting(newNode))
            {
                UpdateReduced(update, newNode);
                return true;
            }

            return false;
        }

        if (!TryReplacement("Make standalone", old => prog))
            return;

        if (TryReplacement("Remove ALC", RemoveALC))
        {
            TryReplacement("Inline runtime creation and merge mains", InlineRuntimeCreationAndMergeMains);

            if (TryReplacement("Remove call site args from WriteLine calls", RemoveCallSiteArgs))
            {
                TryReplacement("Inline WriteLine calls", InlineWriteLineCalls);
            }
        }
    }

    private bool IsStandaloneProgramInteresting(CompilationUnitSyntax prog)
    {
        string tempAsmPath = Path.Combine(Path.GetTempPath(), "fuzzlyn-" + Guid.NewGuid().ToString("N") + ".dll");
        try
        {
            (string debugStdout, string debugStderr, int debugExitCode) = ExecuteInSubProcess(prog, _baseCompilerOpts.AsConsoleApp(), tempAsmPath);
            (string releaseStdout, string releaseStderr, int releaseExitCode) = ExecuteInSubProcess(prog, _diffCompilerOpts.AsConsoleApp(), tempAsmPath);
            if (debugStderr.Contains("Assert failure") || debugStderr.Contains("JIT assert failed"))
            {
                return true;
            }

            if (releaseStderr.Contains("Assert failure") || releaseStderr.Contains("JIT assert failed"))
            {
                return true;
            }

            if (debugExitCode == unchecked((int)0xc0000005) || releaseExitCode == unchecked((int)0xc0000005))
            {
                return true;
            }

            string[] debugStderrLines = debugStderr.ReplaceLineEndings().Split(Environment.NewLine);
            string[] releaseStderrLines = releaseStderr.ReplaceLineEndings().Split(Environment.NewLine);

            // Look for exception type
            string debugExceptionLine = debugStderrLines.FirstOrDefault(l => l.Contains("Unhandled exception."));
            string releaseExceptionLine = releaseStderrLines.FirstOrDefault(l => l.Contains("Unhandled exception."));
            if (debugExceptionLine != releaseExceptionLine)
            {
                return true;
            }

            if (debugStdout != releaseStdout)
            {
                return true;
            }

            return false;
        }
        finally
        {
            try
            {
                File.Delete(tempAsmPath);
            }
            catch
            {
            }
        }

        (string stdout, string stderr, int exitCode) ExecuteInSubProcess(CompilationUnitSyntax node, CompilerOptions opts, string tempAsmPath)
        {
            CompileResult result = _compiler.Compile(node, opts);
            if (result.Assembly == null)
            {
                throw new Exception(
                    "Got compiler errors when creating standalone repro:"
                    + Environment.NewLine
                    + string.Join(Environment.NewLine, result.CompileErrors)
                    + Environment.NewLine + Environment.NewLine
                    + node.NormalizeWhitespace().ToFullString());
            }

            File.WriteAllBytes(tempAsmPath, result.Assembly);

            ProcessStartInfo info = new()
            {
                FileName = _pool.Host,
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            info.ArgumentList.Add(tempAsmPath);

            Helpers.SetExecutionEnvironmentVariables(info.EnvironmentVariables);

            if (_pool.SpmiOptions != null)
                Helpers.SetSpmiCollectionEnvironmentVariables(info.EnvironmentVariables, _pool.SpmiOptions);

            // WER mode is inherited by child, so if this is a crash we can
            // make sure no WER dialog opens by disabling it for our own
            // process.
            using (new DisableWERModal())
            {
                Process p = Process.Start(info);
                StringBuilder stdout = new();
                StringBuilder stderr = new();
                p.OutputDataReceived += (sender, args) => stdout.AppendLine(args.Data);
                p.ErrorDataReceived += (sender, args) => stderr.AppendLine(args.Data);
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();

                return (stdout.ToString(), stderr.ToString(), p.ExitCode);
            }
        }
    }

    private CompilationUnitSyntax RemoveALC(CompilationUnitSyntax program)
    {
        MethodDeclarationSyntax mainMethod =
            program.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => m.Identifier.Text == "Main");

        MemberDeclarationSyntax newMain =
            ParseMemberDeclaration(@"public static void Main() => MainInner(new Runtime());");

        CompilationUnitSyntax withNewMain = program.ReplaceNode(mainMethod, newMain);
        MemberDeclarationSyntax alcType =
            withNewMain.DescendantNodes()
                      .OfType<ClassDeclarationSyntax>()
                      .Single(m => m.Identifier.Text == "CollectibleALC");

        return withNewMain.ReplaceNode(alcType, (SyntaxNode)null);
    }

    private CompilationUnitSyntax InlineRuntimeCreationAndMergeMains(CompilationUnitSyntax program)
    {
        // Remove old Main method
        MethodDeclarationSyntax mainMethod =
            program.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => m.Identifier.Text == "Main");
        program = program.ReplaceNode(mainMethod, (SyntaxNode)null);

        // Remove arg from MainInner and rename
        MethodDeclarationSyntax mainInnerMethod =
            program.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => m.Identifier.Text == "MainInner");

        MethodDeclarationSyntax newMain = mainInnerMethod.WithIdentifier(Identifier("Main"));
        newMain = newMain.WithParameterList(ParameterList());

        foreach (SyntaxNode node in newMain.DescendantNodes())
        {
            if (node is ExpressionStatementSyntax expStmt &&
                expStmt.Expression is AssignmentExpressionSyntax asgn &&
                asgn.Left is IdentifierNameSyntax id &&
                id.Identifier.Text == "s_rt")
            {
                newMain = newMain.ReplaceNode(expStmt.Expression, ParseExpression("s_rt = new Runtime()"));
                break;
            }
        }

        return program.ReplaceNode(mainInnerMethod, newMain);
    }

    private CompilationUnitSyntax RemoveCallSiteArgs(CompilationUnitSyntax program)
    {
        Dictionary<SyntaxNode, SyntaxNode> replacements = new();
        foreach (SyntaxNode node in program.DescendantNodes())
        {
            if (node is ExpressionStatementSyntax expStmt &&
                expStmt.Expression is InvocationExpressionSyntax invoc &&
                invoc.Expression is MemberAccessExpressionSyntax mem &&
                mem.Name.Identifier.Text is "WriteLine")
            {
                replacements.Add(invoc, invoc.WithArgumentList(invoc.ArgumentList.WithArguments(invoc.ArgumentList.Arguments.RemoveAt(0))));
            }
        }

        MemberDeclarationSyntax oldIRuntime =
            program.DescendantNodes().OfType<InterfaceDeclarationSyntax>().Single(m => m.Identifier.Text == "IRuntime");

        MemberDeclarationSyntax oldRuntime =
            program.DescendantNodes().OfType<ClassDeclarationSyntax>().Single(m => m.Identifier.Text == "Runtime");

        replacements[oldIRuntime] = ParseMemberDeclaration(@"
public interface IRuntime
{
    void WriteLine<T>(T value);
}");

        replacements[oldRuntime] = ParseMemberDeclaration(@"
public class Runtime : IRuntime
{
    public void WriteLine<T>(T value) => System.Console.WriteLine(value);
}");

        return program.ReplaceNodes(replacements.Keys, (orig, _) => replacements[orig]);
    }

    private CompilationUnitSyntax InlineWriteLineCalls(CompilationUnitSyntax program)
    {
        Dictionary<SyntaxNode, SyntaxNode> replacements = new();
        foreach (SyntaxNode node in program.DescendantNodes())
        {
            TryRewriteWriteLine(node);
            TryDeleteRuntimeTypes(node);
            TryDeleteRuntimeField(node);
            TryDeleteRuntimeAssignment(node);
        }

        return program.ReplaceNodes(replacements.Keys, (old, _) => replacements[old]);

        void TryRewriteWriteLine(SyntaxNode node)
        {
            if (node is not ExpressionStatementSyntax expStmt ||
                expStmt.Expression is not InvocationExpressionSyntax invoc ||
                invoc.Expression is not MemberAccessExpressionSyntax mem)
            {
                return;
            }

            switch (mem.Name.Identifier.Text)
            {
                case "WriteLine":
                case "WriteLineSingle":
                case "WriteLineDouble":
                    ArgumentSyntax arg = invoc.ArgumentList.Arguments[^1];

                    string calledFunc = mem.Name.Identifier.Text switch
                    {
                        "WriteLineSingle" => "SingleToUInt32Bits",
                        "WriteLineDouble" => "DoubleToUInt64Bits",
                        _ => null,
                    };

                    if (calledFunc != null)
                    {
                        arg =
                            arg.WithExpression(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("System"),
                                            IdentifierName("BitConverter")),
                                        IdentifierName(calledFunc)))
                                .WithArgumentList(
                                    ArgumentList(
                                        SingletonSeparatedList(
                                            Argument(
                                                arg.Expression)))));
                    }

                    InvocationExpressionSyntax newCall =
                        InvocationExpression(
                            ParseExpression("System.Console.WriteLine"),
                            ArgumentList(
                                SingletonSeparatedList(arg)));
                    replacements.Add(invoc, newCall);
                    break;
            }
        }

        void TryDeleteRuntimeTypes(SyntaxNode node)
        {
            if (node is TypeDeclarationSyntax typeDecl &&
                typeDecl.Identifier.Text is "Runtime" or "IRuntime")
            {
                replacements.Add(node, null);
            }
        }

        void TryDeleteRuntimeField(SyntaxNode node)
        {
            if (node is FieldDeclarationSyntax field && field.Declaration.Variables.Count == 1 &&
                field.Declaration.Variables[0].Identifier.Text == "s_rt")
            {
                replacements.Add(node, null);
            }
        }

        void TryDeleteRuntimeAssignment(SyntaxNode node)
        {
            if (node is ExpressionStatementSyntax expStmt &&
                expStmt.Expression is AssignmentExpressionSyntax asgn &&
                asgn.Left is IdentifierNameSyntax id &&
                id.Identifier.Text == "s_rt")
            {
                replacements.Add(node, null);
            }
        }
    }

    private struct DisableWERModal : IDisposable
    {
        public DisableWERModal()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prevent post-mortem debuggers from launching here. We get
                // runtime crashes sometimes and the parent Fuzzlyn process will
                // handle it.
                SetErrorMode(ErrorModes.SEM_NOGPFAULTERRORBOX);
            }
        }

        public void Dispose()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prevent post-mortem debuggers from launching here. We get
                // runtime crashes sometimes and the parent Fuzzlyn process will
                // handle it.
                SetErrorMode(ErrorModes.SYSTEM_DEFAULT);
            }
        }

        [Flags]
        private enum ErrorModes
        {
            SYSTEM_DEFAULT = 0x0,
            SEM_FAILCRITICALERRORS = 0x0001,
            SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
            SEM_NOGPFAULTERRORBOX = 0x0002,
            SEM_NOOPENFILEERRORBOX = 0x8000
        }

        [DllImport("kernel32.dll")]
        private static extern ErrorModes SetErrorMode(ErrorModes uMode);
    }

    private bool IsChecksumCall(InvocationExpressionSyntax invoc)
    {
        return invoc.Expression is MemberAccessExpressionSyntax mem &&
               mem.Name.Identifier.Text is "Checksum" or "ChecksumSingle" or "ChecksumDouble" or "ChecksumSingles" or "ChecksumDoubles";
    }

    private bool IsAwaitOrGetResultOfAwaiterCall(SyntaxNode node)
    {
        if (node is AwaitExpressionSyntax)
            return true;

        if (node is InvocationExpressionSyntax invoc && IsGetResultOfAwaiterCall(invoc))
            return true;

        return false;
    }

    private bool IsGetResultOfAwaiterCall(InvocationExpressionSyntax invoc)
    {
        return invoc.Expression is MemberAccessExpressionSyntax mem &&
               mem.Name.Identifier.Text is "GetResult" &&
               mem.Expression is InvocationExpressionSyntax innerInvoc &&
               innerInvoc.Expression is MemberAccessExpressionSyntax innerMem &&
               innerMem.Name.Identifier.Text is "GetAwaiter";
    }

    private readonly List<(string name, SimplifierAttribute info, Func<SyntaxNode, object> simp)> _simplifiers =
        new();

    private void GetSimplifierEnumerators(
        SyntaxNode node, bool late, List<(string name, IEnumerator<SyntaxNode> enumerator)> simplifyEnumerators)
    {
        if (_simplifiers.Count == 0)
        {
            MethodInfo[] methods = typeof(Reducer).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            IEnumerable<MethodInfo> prioritized =
                methods
                .Select(m => (method: m, attrib: m.GetCustomAttribute<SimplifierAttribute>()))
                .Where(t => t.attrib != null)
                .OrderByDescending(t => t.attrib.Priority)
                .Select(t => t.method);

            foreach (MethodInfo method in prioritized)
            {
                SimplifierAttribute attrib = method.GetCustomAttribute<SimplifierAttribute>();
                if (attrib == null)
                    continue;

                Delegate dlg;
                if (method.ReturnType == typeof(SyntaxNode))
                    dlg = Delegate.CreateDelegate(typeof(Func<SyntaxNode, SyntaxNode>), this, method);
                else
                    dlg = Delegate.CreateDelegate(typeof(Func<SyntaxNode, IEnumerable<SyntaxNode>>), this, method);

                _simplifiers.Add((method.Name, attrib, (Func<SyntaxNode, object>)dlg));
            }
        }

        foreach (var (name, info, simplifier) in _simplifiers)
        {
            if (info.Late && !late)
                continue;

            object simplified = simplifier(node);
            IEnumerator<SyntaxNode> enumerator = null;
            if (simplified == null || simplified is SyntaxNode)
            {
                if (simplified != node)
                    enumerator = new List<SyntaxNode>(1) { (SyntaxNode)simplified }.GetEnumerator();
            }
            else
                enumerator = ((IEnumerable<SyntaxNode>)simplified).GetEnumerator();

            if (enumerator != null)
                simplifyEnumerators.Add((name, enumerator));
        }
    }

    // Simplify statement containing call to just the call.
    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyStatementWithCall(SyntaxNode node)
    {
        if (node is not ExpressionStatementSyntax stmt)
            yield break;

        // Take descendant nodes of expression to avoid simplifying M(); to M();
        // Also, do not ascend into awaiter calls to avoid simplifying e.g.
        // "M().GetAwaiter().GetResult()" or "await M()" into M()

        List<InvocationExpressionSyntax> calls =
            stmt.Expression.DescendantNodes(node => !IsAwaitOrGetResultOfAwaiterCall(node)).OfType<InvocationExpressionSyntax>().ToList();
        if (calls.Count <= 0)
            yield break;

        foreach (InvocationExpressionSyntax invoc in calls)
        {
            yield return ExpressionStatement(invoc);
        }
    }

    // Simplify "int a = expr;" to "int a;"
    [Simplifier]
    private SyntaxNode SimplifyLocalRemoveInitializer(SyntaxNode node)
    {
        if (node is not LocalDeclarationStatementSyntax local || local.Declaration.Variables.Count != 1)
            return node;

        return
            local.WithDeclaration(
                local.Declaration
                .WithVariables(
                    SingletonSeparatedList(
                        local.Declaration.Variables[0].WithInitializer(null))));
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> MakeInstanceMethodsStatic(SyntaxNode node)
    {
        if (node is not CompilationUnitSyntax comp)
            yield break;

        ClassDeclarationSyntax programClass =
            comp.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .FirstOrDefault(cls => cls.Identifier.ValueText == CodeGenerator.ClassNameForStaticMethods);
        if (programClass == null)
            yield break;

        foreach (TypeDeclarationSyntax type in comp.DescendantNodes().OfType<TypeDeclarationSyntax>())
        {
            if (type == programClass)
                continue;

            foreach (MemberDeclarationSyntax member in type.Members)
            {
                if (member is not MethodDeclarationSyntax meth)
                    continue;

                if (meth.Modifiers.Any(mod => mod.IsKind(SyntaxKind.StaticKeyword)))
                    continue;

                // Add this parameter. Currently this is never byref although we might want to do that for structs.
                ParameterSyntax thisParam =
                    Parameter(
                        Identifier("argThis"))
                    .WithType(IdentifierName(type.Identifier));

                ExpressionSyntax useThisArg = IdentifierName("argThis");
                MethodDeclarationSyntax newMeth = meth.WithParameterList(ParameterList(meth.ParameterList.Parameters.Insert(0, thisParam)));
                // Replace uses in body of 'this' with arg
                newMeth =
                    newMeth.ReplaceNodes(
                        newMeth.DescendantNodes().OfType<ThisExpressionSyntax>(),
                        (origNode, node) => useThisArg);

                SyntaxNode CreateNewMemberAccess(SyntaxNode origNode, SyntaxNode node)
                {
                    if (node is not MemberAccessExpressionSyntax mem)
                        return node;

                    if (mem.Expression is not IdentifierNameSyntax id)
                        return node;

                    if (id.Identifier.ValueText != programClass.Identifier.ValueText)
                        return node;

                    return mem.Name;
                }

                // Replace references to 'Program' in body
                newMeth =
                    newMeth.ReplaceNodes(
                        newMeth.DescendantNodes(),
                        CreateNewMemberAccess);

                // Add static
                newMeth = newMeth.AddModifiers(Token(SyntaxKind.StaticKeyword));

                SyntaxNode CreateNewInvoc(SyntaxNode origNode, SyntaxNode node)
                {
                    if (node is not InvocationExpressionSyntax invoc)
                        return node;

                    if (invoc.Expression is not MemberAccessExpressionSyntax mem)
                        return node;

                    if (mem.Name is not IdentifierNameSyntax id)
                        return node;

                    if (id.Identifier.ValueText != meth.Identifier.ValueText)
                        return node;

                    ExpressionSyntax access;
                    if (node.FirstAncestorOrSelf<ClassDeclarationSyntax>()?.Identifier.ValueText == programClass.Identifier.ValueText)
                        access = IdentifierName(meth.Identifier.ValueText);
                    else
                        access = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(programClass.Identifier.ValueText), IdentifierName(meth.Identifier.ValueText));

                    return invoc.WithExpression(access).WithArgumentList(ArgumentList(invoc.ArgumentList.Arguments.Insert(0, Argument(mem.Expression))));
                }

                var reps = new Dictionary<SyntaxNode, SyntaxNode>
                {
                    [programClass] = programClass.AddMembers(newMeth),
                    [meth] = null,
                };
                // Add new member into class and remove old member
                CompilationUnitSyntax compWithMovedFunc =
                    comp.ReplaceNodes(reps.Keys, (origNode, node) => reps[origNode]);

                // Now replace calls to this function globally
                compWithMovedFunc = compWithMovedFunc.ReplaceNodes(compWithMovedFunc.DescendantNodes(), CreateNewInvoc);

                yield return compWithMovedFunc;
            }
        }
    }

    // Inline calls. We only do this late since it is better to reduce each call before inlining.
    [Simplifier(Late = true)]
    private IEnumerable<SyntaxNode> InlineCall(SyntaxNode node)
    {
        if (node is not ClassDeclarationSyntax cls)
            yield break;

        IEnumerable<InvocationExpressionSyntax> invocs =
            cls.DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .Where(i => i.Expression is IdentifierNameSyntax);

        foreach (InvocationExpressionSyntax invoc in invocs)
        {
            MethodDeclarationSyntax target =
                cls.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(m => m.Identifier.Text == ((IdentifierNameSyntax)invoc.Expression).Identifier.Text);

            bool isAsync = target.Modifiers.Any(SyntaxKind.AsyncKeyword);
            if (isAsync)
            {
                continue;
            }

            // Cannot yet inline functions that have multiple returns, or returns in different position than last...
            int numReturns = target.DescendantNodes().Count(s => s is ReturnStatementSyntax);
            if (numReturns > 1 || (numReturns == 1 && !(target.Body.Statements.Last() is ReturnStatementSyntax)))
                continue;

            Debug.Assert(target != null);

            List<StatementSyntax> finalStatements = new();
            Dictionary<string, string> idReplacements = new();
            // Since we may assign to arguments, we need to introduce a local for each argument.
            // We also need to do this because a variable may go through an implicit conversion when
            // being passed as an arg, so the assignments here replicate that as well.
            foreach (var (param, arg) in target.ParameterList.Parameters.Zip(invoc.ArgumentList.Arguments, (p, a) => (p, a)))
            {
                LocalDeclarationStatementSyntax argLocal;
                string argLocalName;
                if (!arg.RefKindKeyword.IsKind(SyntaxKind.None))
                    (argLocal, argLocalName) = MakeLocalDecl(RefExpression(arg.Expression), RefType(param.Type));
                else
                    (argLocal, argLocalName) = MakeLocalDecl(arg.Expression, param.Type);

                finalStatements.Add(argLocal);
                idReplacements.Add(param.Identifier.Text, argLocalName);
            }

            // Rename locals to avoid clashes
            foreach (VariableDeclaratorSyntax varDecl in
                target.Body.DescendantNodes().OfType<VariableDeclaratorSyntax>())
            {
                idReplacements.Add(varDecl.Identifier.Text, MakeLocalName());
            }

            string valueName = null;
            foreach (StatementSyntax stmt in target.Body.Statements)
            {
                IEnumerable<SyntaxToken> tokens =
                    stmt.DescendantTokens()
                        .Where(t => t.IsKind(SyntaxKind.IdentifierToken) && idReplacements.ContainsKey(t.Text));

                StatementSyntax newStmt =
                    stmt.ReplaceTokens(tokens, (orig, _) => Identifier(idReplacements[orig.Text]));

                if (newStmt is ReturnStatementSyntax ret)
                {
                    if (ret.Expression == null)
                        continue;

                    (newStmt, valueName) = MakeLocalDecl(ret.Expression, isAsync ? UnwrapTaskType(target.ReturnType) : target.ReturnType);
                }

                finalStatements.Add(newStmt);
            }

            ExpressionSyntax replaceExpr = invoc;
            if (isAsync)
            {
                replaceExpr =
                    replaceExpr.FirstAncestorOrSelf<AwaitExpressionSyntax>() ??
                    replaceExpr.FirstAncestorOrSelf<InvocationExpressionSyntax>(IsGetResultOfAwaiterCall) ??
                    replaceExpr;
            }

            bool replaceUsage = !(replaceExpr.Parent is ExpressionStatementSyntax);
            Debug.Assert(!replaceUsage || valueName != null, "We need to replace usage but no return statement was found");

            StatementSyntax containingInvoc = replaceExpr.FirstAncestorOrSelf<StatementSyntax>();
            if (replaceUsage)
            {
                finalStatements.Add(containingInvoc.ReplaceNode(replaceExpr, IdentifierName(valueName)));
            }

            BlockSyntax containingBlock = replaceExpr.FirstAncestorOrSelf<BlockSyntax>();
            SyntaxNode newNode =
                node.ReplaceNode(
                    containingBlock,
                    containingBlock.WithStatements(
                        containingBlock.Statements.ReplaceRange(containingInvoc, finalStatements)));

            yield return newNode;
        }
    }

    // Remove statements
    [Simplifier(Priority = 1)]
    private SyntaxNode SimplifyStatementRemove(SyntaxNode node)
    {
        if (!(node is StatementSyntax) || !(node.Parent is BlockSyntax))
            return node;

        return null;
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyIf(SyntaxNode node)
    {
        if (node is not IfStatementSyntax @if)
            yield break;

        // Simplify to then block
        yield return @if.Statement;

        BlockSyntax thenBlock = @if.Statement as BlockSyntax;

        // Extract condition for empty if
        if (thenBlock != null && thenBlock.Statements.Count == 0 && @if.Else == null)
        {
            var (local, name) = MakeLocalDecl(@if.Condition, PredefinedType(Token(SyntaxKind.BoolKeyword)));
            IfStatementSyntax newIf = @if.WithCondition(IdentifierName(name));
            yield return Block(local, newIf);
        }

        if (@if.Else == null)
            yield break;

        // Simplify to else block
        yield return @if.Else.Statement;
        // Remove else block
        yield return @if.WithElse(null);

        // Flip if (expr) { } else { else; }  to "if (!expr) { else; }"
        if (thenBlock != null && thenBlock.Statements.Count <= 0)
        {
            IfStatementSyntax newIf =
                IfStatement(
                    PrefixUnaryExpression(
                        SyntaxKind.LogicalNotExpression,
                        ParenthesizedExpression(@if.Condition)),
                @if.Else.Statement);

            yield return newIf;
        }
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifySwitch(SyntaxNode node)
    {
        if (node is not SwitchStatementSyntax @switch)
            yield break;

        foreach (var section in @switch.Sections)
        {
            // Remove the 'break;' statement
            if (section.Statements.Count > 0)
            {
                var lastStmt = section.Statements.Last();
                if (lastStmt is BreakStatementSyntax)
                    yield return Block(section.Statements.RemoveAt(section.Statements.Count - 1));
                else
                    yield return Block(section.Statements);
            }
        }
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyFor(SyntaxNode node)
    {
        if (node is not ForStatementSyntax @for)
            yield break;

        // Simplify 'for (var a = expr; b; c) stmt' to
        // var a = expr; stmt
        VariableDeclarationSyntax decl = @for.Declaration;
        StatementSyntax body = @for.Statement;
        yield return Block(LocalDeclarationStatement(decl), body);
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyDo(SyntaxNode node)
    {
        if (node is not DoStatementSyntax @do)
            yield break;

        // Simplify 'do stmt while (c)' to
        // stmt
        StatementSyntax body = @do.Statement;
        yield return Block(body);
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyBinaryExpression(SyntaxNode node)
    {
        if (node is not BinaryExpressionSyntax bin)
            yield break;

        yield return bin.Left;
        yield return bin.Right;
    }

    [Simplifier]
    private SyntaxNode SimplifyCast(SyntaxNode node)
    {
        if (node is not CastExpressionSyntax cast)
            return node;

        return cast.Expression;
    }

    [Simplifier]
    private SyntaxNode SimplifyParenthesized(SyntaxNode node)
    {
        if (node is not ParenthesizedExpressionSyntax p)
            return node;

        if (Helpers.RequiresParentheses(p.Expression) &&
            !(node.Parent is EqualsValueClauseSyntax))
        {
            return node;
        }

        return p.Expression;
    }

    [Simplifier]
    private SyntaxNode SimplifyPrefixUnaryExpression(SyntaxNode node)
    {
        if (node is not PrefixUnaryExpressionSyntax p)
            return node;

        return p.Operand;
    }

    [Simplifier]
    private SyntaxNode SimplifyPostfixUnaryExpression(SyntaxNode node)
    {
        if (node is not PostfixUnaryExpressionSyntax p)
            return node;

        return p.Operand;
    }

    [Simplifier]
    private SyntaxNode SimplifyInitializer(SyntaxNode node)
    {
        if (node is not InitializerExpressionSyntax init)
            return node;

        if (init.Kind() != SyntaxKind.ArrayInitializerExpression || init.Expressions.Count < 1)
            return node;

        return init.WithExpressions(SingletonSeparatedList(init.Expressions[0]));
    }

    [Simplifier]
    private SyntaxNode SimplifyBlockFlatten(SyntaxNode node)
    {
        if (node is not BlockSyntax block || !block.Statements.Any(s => s is BlockSyntax))
            return node;

        return block.WithStatements(
            block.Statements.SelectMany(s => s is BlockSyntax bs ? bs.Statements : Enumerable.Repeat(s, 1)).ToSyntaxList());
    }

    [Simplifier]
    private SyntaxNode RemoveMethodDeclaration(SyntaxNode node)
    {
        if (node is not MethodDeclarationSyntax method || method.Identifier.Text == "Main")
            return node;

        return null;
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> RemoveBasesFromBaseList(SyntaxNode node)
    {
        if (node is not TypeDeclarationSyntax type)
            yield break;

        if (type.BaseList == null)
            yield break;

        if (type.BaseList.Types.Count == 1)
        {
            yield return type.WithBaseList(null);
            yield break;
        }

        for (int i = 0; i < type.BaseList.Types.Count; i++)
            yield return type.WithBaseList(BaseList(type.BaseList.Types.RemoveAt(i)));
    }

    [Simplifier]
    private SyntaxNode RemoveTypeDeclaration(SyntaxNode node)
    {
        if (node is not TypeDeclarationSyntax type || type.Identifier.Text == "Program")
            return node;

        return null;
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyField(SyntaxNode node)
    {
        if (node is not FieldDeclarationSyntax field)
            yield break;

        // Remove
        yield return null;

        if (field.Declaration.Variables.Count == 1 &&
            field.Declaration.Variables[0].Initializer != null)
        {
            // Remove initializer
            yield return field.ReplaceNode(field.Declaration.Variables[0].Initializer, (SyntaxNode)null);
        }

    }

    [Simplifier]
    private IEnumerable<SyntaxNode> RemoveMethodArgument(SyntaxNode node)
    {
        if (node is not CompilationUnitSyntax unit)
            yield break;

        IEnumerable<(ParameterSyntax pm, int index)> methodParams =
            unit.DescendantNodes().OfType<BaseMethodDeclarationSyntax>()
            .Where(m => m is not MethodDeclarationSyntax normalMethod || normalMethod.Identifier.Text != "Main")
            .SelectMany(m => m.ParameterList.Parameters.Select((p, i) => (p, i)));

        foreach (var (parameter, index) in methodParams)
        {
            BaseMethodDeclarationSyntax method = (BaseMethodDeclarationSyntax)parameter.Parent.Parent;
            Func<SyntaxNode, bool> isInvoc;
            Func<SyntaxNode, ArgumentListSyntax> getInvocArgs;
            if (method is MethodDeclarationSyntax)
            {
                isInvoc = n => n is InvocationExpressionSyntax invoc &&
                          invoc.Expression is IdentifierNameSyntax id &&
                          id.Identifier.Text == ((MethodDeclarationSyntax)method).Identifier.Text;
                getInvocArgs = n => ((InvocationExpressionSyntax)n).ArgumentList;
            }
            else if (method is ConstructorDeclarationSyntax)
            {
                isInvoc = n => n is ObjectCreationExpressionSyntax creation &&
                               creation.Type is IdentifierNameSyntax id &&
                               id.Identifier.Text == ((ConstructorDeclarationSyntax)method).Identifier.Text;
                getInvocArgs = n => ((ObjectCreationExpressionSyntax)n).ArgumentList;
            }
            else
                continue;

            SyntaxNode methodWithoutParam;
            if (method is ConstructorDeclarationSyntax && method.ParameterList.Parameters.Count == 1)
            {
                // Remove constructors if they get 0 args
                methodWithoutParam = null;
            }
            else
            {
                methodWithoutParam =
                    method.ReplaceNode(
                        method.ParameterList,
                        method.ParameterList.WithParameters(method.ParameterList.Parameters.RemoveAt(index)));
            }

            SyntaxNode newNode = unit.ReplaceNode(method, methodWithoutParam);
            while (true)
            {
                SyntaxNode invoc =
                    newNode.DescendantNodes().FirstOrDefault(
                        i => isInvoc(i) && getInvocArgs(i).Arguments.Count == method.ParameterList.Parameters.Count);

                if (invoc == null)
                    break;

                ArgumentListSyntax args = getInvocArgs(invoc);
                ArgumentListSyntax newArgs = args.WithArguments(args.Arguments.RemoveAt(index));
                newNode = newNode.ReplaceNode(args, newArgs);
            }

            yield return newNode;
        }
    }

    // Simplify an invocation by extracting only a arg. In some cases
    // we get rid of the interesting behavior by extracting all args, so we can
    // fall back to this in that case (which is also why this is marked late).
    [Simplifier(Late = true)]
    private IEnumerable<SyntaxNode> SimplifyInvocationExtractArg(SyntaxNode node)
    {
        // Simplify a random invocation in a block from
        // M(expr1, expr2)
        // or
        // a = M(expr1, expr2)
        // or
        // var a = M(expr1, expr2)
        // to
        // var arg1 = expr1;
        // [var a = ] M(arg1, expr2)
        // for each argument

        if (node is not BlockSyntax block)
            yield break;

        IEnumerable<InvocationExpressionSyntax> invocs =
            block.DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .Where(i => i.FirstAncestorOrSelf<BlockSyntax>() == block);

        foreach (InvocationExpressionSyntax invoc in invocs)
        {
            if (IsChecksumCall(invoc))
                continue;

            int statementIndex =
                block.Statements.Select((s, i) => (s, i)).Single(t => t.s.Contains(invoc)).i;

            // If we enumerate in order we will always extract args so that the evaluation order is still correct.
            foreach (ArgumentSyntax arg in invoc.ArgumentList.Arguments)
            {
                if (arg.Expression is IdentifierNameSyntax || arg.Expression is LiteralExpressionSyntax)
                    continue;

                var (local, name) = MakeLocalDecl(arg.Expression);
                BlockSyntax newBlock = block.ReplaceNode(arg, Argument(IdentifierName(name)));
                newBlock = newBlock.WithStatements(newBlock.Statements.Insert(statementIndex, local));
                yield return newBlock;
            }
        }
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> RemoveAsync(SyntaxNode node)
    {
        if (node is not CompilationUnitSyntax unit)
            yield break;

        IEnumerable<MethodDeclarationSyntax> asyncMethods =
            unit.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .Where(m => m is MethodDeclarationSyntax method && method.Modifiers.Any(SyntaxKind.AsyncKeyword));

        foreach (MethodDeclarationSyntax asyncMethod in asyncMethods)
        {
            Func<SyntaxNode, bool> isInvoc =
                n => n is InvocationExpressionSyntax invoc &&
                invoc.Expression is IdentifierNameSyntax id &&
                id.Identifier.Text == asyncMethod.Identifier.Text;

            Func<SyntaxNode, SyntaxNode> taskResultAncestor =
                n => (SyntaxNode)n.FirstAncestorOrSelf<AwaitExpressionSyntax>() ??
                n.FirstAncestorOrSelf<InvocationExpressionSyntax>(IsGetResultOfAwaiterCall);

            MethodDeclarationSyntax methodWithoutAsync = asyncMethod.WithModifiers(asyncMethod.Modifiers.RemoveAt(asyncMethod.Modifiers.IndexOf(SyntaxKind.AsyncKeyword)));
            methodWithoutAsync = methodWithoutAsync.WithReturnType(UnwrapTaskType(methodWithoutAsync.ReturnType));

            SyntaxNode newNode = unit.ReplaceNode(asyncMethod, methodWithoutAsync);
            while (true)
            {
                SyntaxNode invoc = newNode.DescendantNodes().FirstOrDefault(i => isInvoc(i) && taskResultAncestor(i) != null);

                if (invoc == null)
                    break;

                newNode = newNode.ReplaceNode(taskResultAncestor(invoc), invoc);
            }

            yield return newNode;
        }
    }

    private static TypeSyntax UnwrapTaskType(TypeSyntax taskType)
        => taskType is GenericNameSyntax generic ? generic.TypeArgumentList.Arguments[0] : PredefinedType(Token(SyntaxKind.VoidKeyword));

    // Combine code like "ulong var0; var0 = 123" to "ulong var0 = 123"
    [Simplifier]
    private IEnumerable<SyntaxNode> CombineLocalAssignmentsInBlock(SyntaxNode node)
    {
        if (node is not BlockSyntax block)
            yield break;

        foreach (StatementSyntax statement in block.Statements)
        {
            if (statement is not LocalDeclarationStatementSyntax local)
                continue;

            if (local.Declaration.Variables.Count != 1 ||
                local.Declaration.Variables[0].Initializer != null)
                continue;

            string localId = local.Declaration.Variables[0].Identifier.Text;
            foreach (ExpressionStatementSyntax expStmt in block.DescendantNodes().OfType<ExpressionStatementSyntax>())
            {
                if (expStmt.Expression is not AssignmentExpressionSyntax asgn)
                    continue;

                if (asgn.Left is not IdentifierNameSyntax id)
                    continue;

                if (id.Identifier.Text != localId)
                    continue;

                LocalDeclarationStatementSyntax newLocal =
                    local
                    .WithDeclaration(
                        local.Declaration.WithVariables(
                            SingletonSeparatedList(
                                local.Declaration.Variables[0].WithInitializer(
                                    EqualsValueClause(asgn.Right)))));

                int localIndex = block.Statements.IndexOf(local);

                BlockSyntax newBlock =
                    block.ReplaceNode(expStmt, newLocal);

                newBlock = newBlock.WithStatements(newBlock.Statements.RemoveAt(localIndex));
                yield return newBlock;
                // Only check with first assignment
                break;
            }
        }
    }

    // Inlines locals of the forms
    // var a = b;
    // var a = literal;
    // ref int a = ref b;
    // All occurences of a are replaced by the right-hand side.
    [Simplifier]
    private IEnumerable<SyntaxNode> InlineLocals(SyntaxNode node)
    {
        if (node is not BlockSyntax block)
            yield break;

        foreach (LocalDeclarationStatementSyntax local in block.Statements.OfType<LocalDeclarationStatementSyntax>())
        {
            if (local.Declaration.Variables.Count != 1)
                continue;

            VariableDeclaratorSyntax var = local.Declaration.Variables[0];
            if (var.Initializer == null)
                continue;

            bool isIdentifier = var.Initializer.Value is IdentifierNameSyntax;
            bool isLiteral = var.Initializer.Value is LiteralExpressionSyntax;
            bool isRefIdentifier = var.Initializer.Value is RefExpressionSyntax lclRes &&
                                   lclRes.Expression is IdentifierNameSyntax;

            if (!isIdentifier && !isLiteral && !isRefIdentifier)
                continue;

            string toReplace = local.Declaration.Variables[0].Identifier.Text;
            SyntaxNode replaceWith = local.Declaration.Variables[0].Initializer.Value;

            if (replaceWith is RefExpressionSyntax res)
                replaceWith = res.Expression;

            BlockSyntax newNode =
                block.WithStatements(block.Statements.Remove(local));

            newNode =
                newNode.ReplaceNodes(
                    newNode.DescendantNodes().OfType<IdentifierNameSyntax>().Where(id => id.Identifier.Text == toReplace),
                    (orig, _) => replaceWith);

            yield return newNode;
        }
    }

    [Simplifier(Late = true)]
    private IEnumerable<SyntaxNode> SimplifyConstant(SyntaxNode node)
    {
        if (node is not LiteralExpressionSyntax literal || literal.Kind() != SyntaxKind.NumericLiteralExpression)
            yield break;

        if (literal.Token.Text == "0" || literal.Token.Text == "1" || literal.Token.Text == "-1")
            yield break;

        // Do not change constants in conditions of loops
        if (literal.FirstAncestorOrSelf<StatementSyntax>() is ForStatementSyntax or WhileStatementSyntax or DoStatementSyntax)
            yield break;

        yield return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0));
        yield return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1));
        yield return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(-1));
    }

    // Add ': this()' to struct constructor to allow further simplification of struct
    [Simplifier]
    private SyntaxNode SimplifyStructAddConstructorInitializers(SyntaxNode node)
    {
        if (node is not StructDeclarationSyntax @struct)
            return node;

        StructDeclarationSyntax newStruct = @struct;
        while (true)
        {
            ConstructorDeclarationSyntax ctor =
                newStruct.Members.OfType<ConstructorDeclarationSyntax>().FirstOrDefault(c => c.Initializer == null);

            if (ctor == null)
                return newStruct;

            newStruct = newStruct.ReplaceNode(
                ctor,
                ctor.WithInitializer(
                    ConstructorInitializer(
                        SyntaxKind.ThisConstructorInitializer,
                        ArgumentList())));
        }
    }

    // Simplify ref-local. ref T a = ref b => T a = b
    [Simplifier]
    private SyntaxNode SimplifyLocalRemoveRef(SyntaxNode node)
    {
        if (node is not LocalDeclarationStatementSyntax local || local.Declaration.Variables.Count != 1 ||
            local.Declaration.Type is not RefTypeSyntax refTypeSyntax)
            return node;

        VariableDeclaratorSyntax var = local.Declaration.Variables[0];
        if (var.Initializer == null)
            return node;

        Debug.Assert(var.Initializer.Value is RefExpressionSyntax);

        ExpressionSyntax innerExpr = ((RefExpressionSyntax)var.Initializer.Value).Expression;
        SyntaxNode newNode =
            LocalDeclarationStatement(
                VariableDeclaration(
                    refTypeSyntax.Type,
                    SingletonSeparatedList(
                        VariableDeclarator(
                            var.Identifier)
                        .WithInitializer(
                            EqualsValueClause(
                                innerExpr)))));

        return newNode;
    }

    [Simplifier]
    private IEnumerable<SyntaxNode> SimplifyTry(SyntaxNode node)
    {
        if (node is not TryStatementSyntax @try)
            yield break;

        yield return @try.Block;

        if (@try.Finally != null)
        {
            yield return @try.Finally.Block;
            yield return Block(@try.Block, @try.Finally.Block);
            // Try finally first and then block. Useful when the try-block contains a
            // return.
            yield return Block(@try.Finally.Block, @try.Block);
        }
    }

    [Simplifier]
    private SyntaxNode SimplifyMethodRemoveReturn(SyntaxNode node)
    {
        if (node is not MethodDeclarationSyntax method)
            return node;

        if (method.ReturnType is PredefinedTypeSyntax returnType &&
            returnType.Keyword.IsKind(SyntaxKind.VoidKeyword))
        {
            return node;
        }

        SyntaxNode newNode =
            method.WithReturnType(PredefinedType(Token(SyntaxKind.VoidKeyword)));
        newNode = newNode.ReplaceNodes(newNode.DescendantNodes().OfType<ReturnStatementSyntax>(), (cur, old) => ReturnStatement());
        return newNode;
    }

    private (LocalDeclarationStatementSyntax local, string name) MakeLocalDecl(ExpressionSyntax expr, TypeSyntax type = null)
    {
        string name = MakeLocalName();

        LocalDeclarationStatementSyntax local =
            LocalDeclarationStatement(
                VariableDeclaration(
                    type ?? IdentifierName("var"),
                    SingletonSeparatedList(
                        VariableDeclarator(name)
                        .WithInitializer(
                            EqualsValueClause(
                                expr)))));

        return (local, name);
    }

    private string MakeLocalName() => $"vr{_varCounter++}";

    [AttributeUsage(AttributeTargets.Method)]
    private class SimplifierAttribute : Attribute
    {
        public int Priority { get; set; }
        public bool Late { get; set; }
    }
}
