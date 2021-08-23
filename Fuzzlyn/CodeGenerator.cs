using Fuzzlyn.Methods;
using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    internal class CodeGenerator
    {
        internal const string ClassNameForStaticMethods = "Program";

        private int checksumSiteId;

        public CodeGenerator(FuzzlynOptions options)
        {
            Options = options;
            Random = new Randomizer(options);
            Types = new TypeManager(Random);
            Statics = new StaticsManager(Random, Types);
            Methods = new MethodManager(Random, Types, Statics);
        }

        public FuzzlynOptions Options { get; }
        public Randomizer Random { get; }
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }
        public MethodManager Methods { get; }

        private void GenerateTypes() => Types.GenerateTypes();
        private void GenerateMethods() => Methods.GenerateMethods(GenerateChecksumSiteId);

        public CompilationUnitSyntax GenerateProgram()
        {
            GenerateTypes();
            GenerateMethods();
            return OutputProgram();
        }

        private CompilationUnitSyntax OutputProgram()
        {
            CompilationUnitSyntax unit = CompilationUnit();

            (List<MethodDeclarationSyntax> staticFuncs, Dictionary<FuzzType, List<MethodDeclarationSyntax>> typeMethods) = Methods.OutputMethods();

            IEnumerable<MemberDeclarationSyntax> types = Types.OutputTypes(typeMethods);

            // Append 'Program' class containing statics and methods, followed by main method
            MemberDeclarationSyntax programClass =
                ClassDeclaration(ClassNameForStaticMethods)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithMembers(OutputProgramMembers(staticFuncs).ToSyntaxList());

            types = types.Concat(new[] { programClass });

            unit = unit.WithMembers(types.ToSyntaxList());
            unit = unit.WithLeadingTrivia(OutputHeader().Select(Comment));

            return unit;
        }

        private IEnumerable<MemberDeclarationSyntax> OutputProgramMembers(List<MethodDeclarationSyntax> methods)
        {
            if (Options.EnableChecksumming)
            {
                yield return
                    FieldDeclaration(
                        VariableDeclaration(
                            ParseTypeName("Fuzzlyn.Execution.IRuntime"),
                            SingletonSeparatedList(
                                VariableDeclarator("s_rt"))))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)));
            }

            foreach (FieldDeclarationSyntax stat in Statics.OutputStatics())
                yield return stat;

            ParameterListSyntax parameters = ParameterList();

            if (Options.EnableChecksumming)
            {
                parameters =
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier("rt"))
                            .WithType(ParseTypeName("Fuzzlyn.Execution.IRuntime"))));
            }

            yield return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    "Main")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
                .WithParameterList(parameters)
                .WithBody(Block(GenMainStatements()));

            foreach (MethodDeclarationSyntax method in methods)
                yield return method;

            IEnumerable<StatementSyntax> GenMainStatements()
            {
                if (Options.EnableChecksumming)
                {
                    yield return
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("s_rt"),
                                IdentifierName("rt")));
                }

                yield return
                    ExpressionStatement(
                        InvocationExpression(
                            IdentifierName(methods[0].Identifier)));

                if (Options.EnableChecksumming)
                {
                    IEnumerable<StatementSyntax> staticChecksums =
                        FuncBodyGenerator.GenChecksumming(false, Statics.Fields.Select(s => new ScopeValue(s.Type, s.CreateAccessor(false), int.MaxValue, false)), GenerateChecksumSiteId);

                    foreach (StatementSyntax checksumStatement in staticChecksums)
                        yield return checksumStatement;
                }
            }
        }

        private string GenerateChecksumSiteId() => $"c_{checksumSiteId++}";

        private IEnumerable<string> OutputHeader()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            yield return $"// Generated by Fuzzlyn v{version.Major}.{version.Minor} on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            yield return $"// Run on {RuntimeInformation.FrameworkDescription} on {RuntimeInformation.ProcessArchitecture} {GetOS()}";
            yield return $"// Seed: {Random.Seed}";
        }

        private static string GetOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "Linux";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                return "FreeBSD";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "MacOS";

            return "Unknown OS";
        }
    }
}
