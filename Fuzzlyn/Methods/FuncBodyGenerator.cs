using Fuzzlyn.ExecutionServer;
using Fuzzlyn.ProbabilityDistributions;
using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods;

internal class FuncBodyGenerator(
    List<FuncGenerator> funcs,
    Randomizer random,
    TypeManager types,
    StaticsManager statics,
    ApiManager apis,
    Func<string> genChecksumSiteId,
    int funcIndex,
    FuzzType returnType,
    bool isAsync,
    bool isInPrimaryClass)
{
    private readonly List<FuncGenerator> _funcs = funcs;
    private readonly Randomizer _random = random;
    private readonly TypeManager _types = types;
    private readonly StaticsManager _statics = statics;
    private readonly ApiManager _apis = apis;
    private readonly Func<string> _genChecksumSiteId = genChecksumSiteId;
    private readonly int _funcIndex = funcIndex;
    private readonly FuzzType _returnType = returnType;
    private readonly bool _isAsync = isAsync;
    private readonly bool _isInPrimaryClass = isInPrimaryClass;

    private readonly List<ScopeFrame> _scope = new();
    private int _tryCatchCount;
    private int _finallyCount;
    private int _varCounter;
    private int _statementLevel = -1;
    private int _awaitDisallowed;

    private FuzzlynOptions Options => _random.Options;

    public BlockSyntax Block { get; private set; }
    public int NumStatements { get; private set; }
    // Represents the transitive call counts from this function to other functions.
    public Dictionary<int, long> CallCounts { get; } = new();

    public void Generate(List<ScopeValue> initialScope)
    {
        Block = GenBlock(initialScope, true);
    }

    private StatementSyntax GenStatement(bool allowReturn = true)
    {
        if (_finallyCount > 0)
            allowReturn = false;

        while (true)
        {
            ProbabilityDistribution statementTypeDist =
                (_isAsync && _awaitDisallowed == 0) ? Options.StatementTypeAsyncDist : Options.StatementTypeDist;

            StatementKind kind = (StatementKind)statementTypeDist.Sample(_random.Rng);

            if ((kind == StatementKind.Block ||
                 kind == StatementKind.If ||
                 kind == StatementKind.Switch ||
                 kind == StatementKind.TryCatch ||
                 kind == StatementKind.TryFinally ||
                 kind == StatementKind.Loop) &&
                ShouldRejectRecursion())
                continue;

            if (kind == StatementKind.Return && !allowReturn)
                continue;

            if (kind == StatementKind.Throw && (_tryCatchCount == 0))
                continue;

            if (_isAsync && kind is StatementKind.TryCatch or StatementKind.TryFinally && Options.GenExtensions.Contains(Extension.RuntimeAsync))
            {
                // All the EH transformations are not yet fully supported by Roslyn
                continue;
            }

            switch (kind)
            {
                case StatementKind.Block:
                    return GenBlock();
                case StatementKind.Assignment:
                    return GenAssignmentStatement();
                case StatementKind.Call:
                    return GenCallStatement(tryExisting: ShouldRejectRecursion());
                case StatementKind.If:
                    return GenIf();
                case StatementKind.Switch:
                    return GenSwitch();
                case StatementKind.Throw:
                    return GenThrow();
                case StatementKind.TryCatch:
                    return GenTryCatch();
                case StatementKind.TryFinally:
                    return GenTryFinally();
                case StatementKind.Return:
                    return GenReturn();
                case StatementKind.Loop:
                    return GenLoop();
                case StatementKind.Yield:
                    return GenYield();
                default:
                    throw new Exception("Unreachable");
            }
        }

        bool ShouldRejectRecursion()
            => Options.StatementRejection.Reject(_statementLevel, _random.Rng);
    }

    private BlockSyntax GenBlock(
        IEnumerable<ScopeValue> vars = null,
        bool root = false,
        int numStatements = -1)
    {
        if (numStatements == -1)
            numStatements = Options.BlockStatementCountDist.Sample(_random.Rng);

        _statementLevel++;

        ScopeFrame scope = new();
        _scope.Add(scope);

        if (vars != null)
            scope.Values.AddRange(vars);

        BlockSyntax block = Block(GenStatements());

        _scope.RemoveAt(_scope.Count - 1);

        _statementLevel--;

        return block;

        IEnumerable<StatementSyntax> GenStatements()
        {
            StatementSyntax retStmt = null;

            int numGenerated = 0;
            if (numStatements > 0)
            {
                while (true)
                {
                    StatementSyntax stmt = GenStatement(allowReturn: !root);

                    if (stmt is ReturnStatementSyntax)
                    {
                        retStmt = stmt;
                        break;
                    }

                    NumStatements++;
                    yield return stmt;

                    numGenerated++;
                    if (numGenerated < numStatements)
                        continue;

                    // For first block we ensure we get a minimum amount of statements
                    if (root && _funcIndex == 0)
                    {
                        if (_funcs.Sum(f => f.NumStatements) < Options.ProgramMinStatements)
                            continue;
                    }

                    break;
                }
            }

            if (Options.EnableChecksumming)
            {
                foreach (StatementSyntax stmt in GenChecksumming(prefixRuntimeAccess: !_isInPrimaryClass, scope.Values, _genChecksumSiteId))
                    yield return stmt;
            }

            if (root && retStmt == null && _returnType != null)
                retStmt = GenReturn();

            if (retStmt != null)
            {
                NumStatements++;
                yield return retStmt;
            }
        }
    }

    private ExpressionSyntax AccessStatic(StaticField stat)
        => stat.CreateAccessor(prefixWithClass: !_isInPrimaryClass);

    private StatementSyntax GenAssignmentStatement()
    {
        LValueInfo lvalue = null;
        if (!_random.FlipCoin(Options.AssignToNewVarProb))
            lvalue = GenExistingLValue(null, int.MinValue);

        if (lvalue == null)
        {
            FuzzType newType = _types.PickType(_isAsync ? 0 : Options.LocalIsByRefProb);
            // Determine if we should create a new local. We do this with a certain probabilty,
            // or always if the new type is a by-ref type (we cannot have static by-refs).
            if (newType is RefType || _random.FlipCoin(Options.NewVarIsLocalProb))
            {
                ScopeValue variable;
                string varName = $"var{_varCounter++}";
                ExpressionSyntax rhs;
                if (newType is RefType newRt)
                {
                    LValueInfo rhsLV = GenLValue(newRt.InnerType, int.MinValue);
                    variable = new ScopeValue(newType, IdentifierName(varName), rhsLV.RefEscapeScope, readOnly: false);
                    rhs = RefExpression(rhsLV.Expression);
                }
                else
                {
                    rhs = GenExpression(newType);
                    variable = new ScopeValue(newType, IdentifierName(varName), -(_scope.Count - 1), readOnly: false);
                }

                LocalDeclarationStatementSyntax decl =
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            variable.Type.GenReferenceTo(),
                            SingletonSeparatedList(
                                VariableDeclarator(varName)
                                .WithInitializer(
                                    EqualsValueClause(rhs)))));

                _scope.Last().Values.Add(variable);

                return decl;
            }

            StaticField newStatic = _statics.GenerateNewField(newType);
            bool isAsyncHoistable = !Options.GenExtensions.Contains(Extension.RuntimeAsync);
            lvalue = new LValueInfo(AccessStatic(newStatic), newType, int.MaxValue, readOnly: false, asyncHoistable: isAsyncHoistable, null);
        }

        using var genAwaits = new GenerateAwaitsScope(this);

        if (!lvalue.AsyncHoistable)
        {
            genAwaits.Disallow();
        }

        // Determine if we should generate a ref-reassignment. In that case we cannot do anything
        // clever, like generate compound assignments.
        FuzzType rhsType = lvalue.Type;
        if (lvalue.Type is RefType rt)
        {
            if (_random.FlipCoin(Options.AssignGenRefReassignProb))
            {
                RefExpressionSyntax refRhs = RefExpression(GenLValue(rt.InnerType, lvalue.RefEscapeScope).Expression);
                return
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            lvalue.Expression,
                            refRhs));
            }

            // We have a ref-type, but are not generating a ref-reassign, so lift the type and make a normal assignment.
            rhsType = rt.InnerType;
        }

        SyntaxKind assignmentKind = SyntaxKind.SimpleAssignmentExpression;
        // Determine if we should generate compound assignment.
        if (rhsType.AllowedAdditionalAssignmentKinds.Length > 0 && _random.FlipCoin(Options.CompoundAssignmentProb))
            assignmentKind = _random.NextElement(rhsType.AllowedAdditionalAssignmentKinds);

        // Early our for simple cases.
        if (assignmentKind == SyntaxKind.PreIncrementExpression ||
            assignmentKind == SyntaxKind.PreDecrementExpression)
        {
            return ExpressionStatement(PrefixUnaryExpression(assignmentKind, lvalue.Expression));
        }

        if (assignmentKind == SyntaxKind.PostIncrementExpression ||
            assignmentKind == SyntaxKind.PostDecrementExpression)
        {
            return ExpressionStatement(PostfixUnaryExpression(assignmentKind, lvalue.Expression));
        }

        // Right operand of shifts are always ints.
        if (assignmentKind == SyntaxKind.LeftShiftAssignmentExpression ||
            assignmentKind == SyntaxKind.RightShiftAssignmentExpression)
        {
            rhsType = _types.GetPrimitiveType(SyntaxKind.IntKeyword);
        }

        ExpressionSyntax right = GenExpression(rhsType);
        // For modulo and division we don't want to throw divide-by-zero exceptions,
        // so always or right-hand-side with 1.
        if ((assignmentKind is SyntaxKind.ModuloAssignmentExpression or SyntaxKind.DivideAssignmentExpression) &&
            (rhsType is not PrimitiveType { Info: { IsFloat: true } }))
        {
            right =
                CastExpression(
                    rhsType.GenReferenceTo(),
                    ParenthesizedExpression(
                        BinaryExpression(
                            SyntaxKind.BitwiseOrExpression,
                            ParenthesizeIfNecessary(right),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))));
        }

        return ExpressionStatement(AssignmentExpression(assignmentKind, lvalue.Expression, right));
    }

    private StatementSyntax GenCallStatement(bool tryExisting)
    {
        // If we are supposed to try existing first, then do not allow new
        ExpressionSyntax call = GenCall(null, allowNew: !tryExisting);

        while (call == null)
        {
            // There are no existing, so allow new until we get a new one
            call = GenCall(null, true);
        }

        return ExpressionStatement(call);
    }

    private StatementSyntax GenYield()
    {
        return
            ExpressionStatement(
                AwaitExpression(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("Task"),
                            IdentifierName("Yield")))));
    }

    private StatementSyntax GenIf()
    {
        ExpressionSyntax guard;
        int attempts = 0;
        do
        {
            guard = GenExpression(new PrimitiveType(SyntaxKind.BoolKeyword));
        } while (IsLiteralOrCastOfLiteral(guard) && attempts++ < 20);

        StatementSyntax gen;
        if (_random.FlipCoin(0.5))
        {
            gen = IfStatement(guard, GenBlock());
        }
        else
        {
            gen = IfStatement(guard, GenBlock(), ElseClause(GenBlock()));
        }
        return gen;
    }

    private StatementSyntax GenSwitch()
    {
        ExpressionSyntax guard;
        int attempts = 0;
        do
        {
            guard = GenExpression(new PrimitiveType(SyntaxKind.IntKeyword));
        } while (IsLiteralOrCastOfLiteral(guard) && attempts++ < 20);

        // C# has a rich 'switch' syntax. Here, for now, generate only a simple switch on int type with dense
        // numeric cases, to attempt to get Roslyn to generate a 'switch' IL instruction.

        List<SwitchSectionSyntax> switchSections = new();
        int caseCount = Options.SwitchCaseCountDist.Sample(_random.Rng);
        for (int caseNumber = 0; caseNumber < caseCount; caseNumber++)
        {
            ExpressionSyntax caseValue = LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(caseNumber));
            switchSections.Add(
                SwitchSection()
                .WithLabels(
                    SingletonList<SwitchLabelSyntax>(
                        CaseSwitchLabel(caseValue)))
                .WithStatements(
                    List<StatementSyntax>(
                        new StatementSyntax[] {
                            GenBlock(),
                            BreakStatement() })));
        }

        // Add a default case (which is mostly likely what will get executed when we run).
        switchSections.Add(
            SwitchSection()
            .WithLabels(
                SingletonList<SwitchLabelSyntax>(
                    DefaultSwitchLabel()))
            .WithStatements(
                List<StatementSyntax>(
                    new StatementSyntax[] {
                        GenBlock(),
                        BreakStatement() })));

        SyntaxList<SwitchSectionSyntax> switchSectionSyntax = switchSections.ToSyntaxList();
        StatementSyntax gen = SwitchStatement(guard, switchSectionSyntax);
        return gen;
    }

    private StatementSyntax GenThrow()
    {
        Debug.Assert(_tryCatchCount > 0);

        return
            ThrowStatement(
                ObjectCreationExpression(
                    QualifiedName(
                        IdentifierName("System"),
                        IdentifierName("Exception")))
                .WithArgumentList(
                    ArgumentList()));
    }

    private StatementSyntax GenTryCatch()
    {
        int tryStatements = Options.BlockStatementCountDist.Sample(_random.Rng);
        bool doCatchWhen = _random.FlipCoin(0.2);

        // Don't allow 'throw' inside 'try' with a 'catch when' clause; we don't know that it will be caught.
        if (!doCatchWhen)
            _tryCatchCount++;
        BlockSyntax body = GenBlock(numStatements: tryStatements);
        if (!doCatchWhen)
            _tryCatchCount--;

        if (doCatchWhen)
        {
            // Make it a "catch when" clause

            int catchStatements = Options.CatchBlockStatementCountDist.Sample(_random.Rng);
            BlockSyntax catchBody = GenBlock(numStatements: catchStatements);
            ExpressionSyntax whenExpression;
            using (var genAwaits = new GenerateAwaitsScope(this))
            {
                genAwaits.Disallow();

                whenExpression = GenExpression(new PrimitiveType(SyntaxKind.BoolKeyword));
            }

            return
                TryStatement(
                    body,
                    SingletonList(
                        CatchClause()
                        .WithDeclaration(
                            CatchDeclaration(
                                QualifiedName(
                                    IdentifierName("System"),
                                    IdentifierName("Exception"))))
                        .WithFilter(
                            CatchFilterClause(whenExpression))
                        .WithBlock(catchBody)),
                    null); // no 'finally'
        }
        else
        {
            // Generate "try/catch/catch/...". We use a fixed set of Exception classes,
            // with System.Exception at the end.
            QualifiedNameSyntax[] catchClauseExceptionNames =
            [
                QualifiedName(IdentifierName("System"), IdentifierName("Exception")),
                QualifiedName(IdentifierName("System"), IdentifierName("ArgumentNullException")),
                QualifiedName(IdentifierName("System"), IdentifierName("ArgumentOutOfRangeException")),
                QualifiedName(IdentifierName("System"), IdentifierName("InvalidOperationException")),
                QualifiedName(IdentifierName("System"), IdentifierName("NotImplementedException")),
                QualifiedName(IdentifierName("System"), IdentifierName("NotSupportedException")),
                QualifiedName(IdentifierName("System"), IdentifierName("ObjectDisposedException")),
                QualifiedName(IdentifierName("System"), IdentifierName("IndexOutOfRangeException")),
                QualifiedName(IdentifierName("System"), IdentifierName("InvalidCastException"))
            ];

            int totalCatchClauses = Options.CatchCountDist.Sample(_random.Rng);
            List<CatchClauseSyntax> catchClauses = new();

            for (int catchNum = totalCatchClauses - 1; catchNum >= 0; catchNum--)
            {
                int catchStatements = Options.CatchBlockStatementCountDist.Sample(_random.Rng);
                BlockSyntax catchBody = GenBlock(numStatements: catchStatements);
                catchClauses.Add(
                    CatchClause()
                    .WithDeclaration(
                        CatchDeclaration(catchClauseExceptionNames[catchNum]))
                    .WithBlock(catchBody));
            }

            SyntaxList<CatchClauseSyntax> catchClausesSyntax = catchClauses.ToSyntaxList();
            return TryStatement(body, catchClausesSyntax, null); // null == no 'finally'
        }
    }

    private StatementSyntax GenTryFinally()
    {
        int numStatements = Options.BlockStatementCountDist.Sample(_random.Rng);
        int tryStatements = _random.Next(numStatements);
        int finallyStatements = numStatements - tryStatements;
        if (_random.FlipCoin(0.5))
        {
            // Randomly swap the number of statements between try and finally. This means that 'finally' will
            // have zero statements as many times as 'try'.
            (tryStatements, finallyStatements) = (finallyStatements, tryStatements);
        }

        BlockSyntax body = GenBlock(numStatements: tryStatements);
        _finallyCount++;
        BlockSyntax finallyBody = GenBlock(numStatements: finallyStatements);
        _finallyCount--;
        return
            TryStatement(
                body,
                List<CatchClauseSyntax>(),
                FinallyClause(finallyBody));
    }

    private StatementSyntax GenReturn()
    {
        if (_returnType == null)
            return ReturnStatement();

        ExpressionSyntax expr;
        if (_returnType is RefType rt)
            expr = RefExpression(GenLValue(rt.InnerType, 1).Expression);
        else
            expr = GenExpression(_returnType);

        return ReturnStatement(expr);
    }

    private StatementSyntax GenLoop()
    {
        if (_random.FlipCoin(_random.Options.ForLoopProb))
        {
            return GenForLoop();
        }
        else
        {
            return GenDoLoop();
        }
    }

    private StatementSyntax GenForLoop()
    {
        string varName = $"lvar{_varCounter++}";
        SyntaxKind op = (SyntaxKind)Options.LoopIndexTypeDist.Sample(_random.Rng);
        PrimitiveType indexPrimType = _types.GetPrimitiveType(op);
        ScopeValue indexVar = new(indexPrimType, IdentifierName(varName), -(_scope.Count - 1), true);
        (ExpressionSyntax lowerBound, ExpressionSyntax upperBound) = LiteralGenerator.GenPrimitiveLiteralLoopBounds(_random, indexPrimType);

        bool upCountedLoop = _random.FlipCoin(_random.Options.UpCountedLoopProb);
        SyntaxKind endCondition = upCountedLoop ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression;
        SyntaxKind nextValueExpression = upCountedLoop ? SyntaxKind.PostIncrementExpression : SyntaxKind.PostDecrementExpression;
        if (!upCountedLoop)
        {
            (lowerBound, upperBound) = (upperBound, lowerBound); // swap the bounds
        }

        VariableDeclarationSyntax decl =
            VariableDeclaration(
                indexVar.Type.GenReferenceTo(),
                SingletonSeparatedList(
                    VariableDeclarator(varName)
                    .WithInitializer(
                        EqualsValueClause(
                            lowerBound))));

        ExpressionSyntax cond =
            BinaryExpression(
                endCondition,
                IdentifierName(varName),
                upperBound);

        ExpressionSyntax incr =
            PostfixUnaryExpression(nextValueExpression, indexVar.Expression);

        BlockSyntax block = GenBlock([indexVar]);

        ForStatementSyntax @for = ForStatement(decl, SeparatedList<ExpressionSyntax>(), cond, SingletonSeparatedList(incr), block);
        return @for;
    }

    private StatementSyntax GenDoLoop()
    {
        string varName = $"lvar{_varCounter++}";
        SyntaxKind op = (SyntaxKind)Options.LoopIndexTypeDist.Sample(_random.Rng);
        PrimitiveType indexPrimType = _types.GetPrimitiveType(op);
        ScopeValue indexVar = new(indexPrimType, IdentifierName(varName), -(_scope.Count - 1), true);
        (ExpressionSyntax lowerBound, ExpressionSyntax upperBound) = LiteralGenerator.GenPrimitiveLiteralLoopBounds(_random, indexPrimType);

        bool upCountedLoop = _random.FlipCoin(_random.Options.UpCountedLoopProb);
        SyntaxKind endCondition = upCountedLoop ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression;
        SyntaxKind nextValueExpression = upCountedLoop ? SyntaxKind.PreIncrementExpression : SyntaxKind.PreDecrementExpression;
        if (!upCountedLoop)
        {
            (lowerBound, upperBound) = (upperBound, lowerBound); // swap the bounds
        }

        VariableDeclarationSyntax decl =
            VariableDeclaration(
                indexVar.Type.GenReferenceTo(),
                SingletonSeparatedList(
                    VariableDeclarator(varName)
                    .WithInitializer(
                        EqualsValueClause(
                            lowerBound))));

        ExpressionSyntax cond =
            BinaryExpression(
                endCondition,
                PrefixUnaryExpression(nextValueExpression, indexVar.Expression),
                upperBound);

        BlockSyntax innerBlock = GenBlock();

        DoStatementSyntax @do = DoStatement(innerBlock, cond);

        BlockSyntax outerBlock = Block(LocalDeclarationStatement(decl), @do);

        return outerBlock;
    }

    private int _expressionLevel = -1;
    private ExpressionSyntax GenExpression(FuzzType type)
    {
        _expressionLevel++;

        Debug.Assert(!(type is RefType));
        ExpressionSyntax gen = null;
        do
        {
            ExpressionKind kind;
            if (type is VectorType)
            {
                kind = (ExpressionKind)Options.VectorExpressionTypeDist.Sample(_random.Rng);
            }
            else
            {
                kind = (ExpressionKind)Options.ExpressionTypeDist.Sample(_random.Rng);
            }

            switch (kind)
            {
                case ExpressionKind.MemberAccess:
                    gen = GenMemberAccess(type);
                    break;
                case ExpressionKind.Literal:
                    gen = GenLiteral(type);
                    break;
                case ExpressionKind.Unary:
                    if (AllowRecursion())
                        gen = GenUnary(type);
                    break;
                case ExpressionKind.Binary:
                    if (AllowRecursion())
                        gen = GenBinary(type);
                    break;
                case ExpressionKind.Call:
                    if (AllowRecursion())
                        gen = GenCall(type, true);
                    break;
                case ExpressionKind.Increment:
                    if (AllowRecursion())
                        gen = GenIncDec(type, true);
                    break;
                case ExpressionKind.Decrement:
                    if (AllowRecursion())
                        gen = GenIncDec(type, false);
                    break;
                case ExpressionKind.NewObject:
                    if (AllowRecursion())
                        gen = GenNewObject(type);
                    break;
                default:
                    throw new Exception("Unreachable");
            }
        }
        while (gen == null);

        bool AllowRecursion()
            => !Options.ExpressionRejection.Reject(_expressionLevel, _random.Rng);

        _expressionLevel--;

        return gen;
    }

    /// <summary>Returns an lvalue.</summary>
    private LValueInfo GenLValue(FuzzType type, int minRefEscapeScope)
    {
        Debug.Assert(type != null);

        LValueInfo lv = GenExistingLValue(type, minRefEscapeScope);

        if (lv == null)
        {
            StaticField newStatic = _statics.GenerateNewField(type);
            bool isAsyncHoistable = !Options.GenExtensions.Contains(Extension.RuntimeAsync);
            lv = new LValueInfo(AccessStatic(newStatic), type, int.MaxValue, readOnly: false, asyncHoistable: isAsyncHoistable, null);
        }

        return lv;
    }

    private LValueInfo GenExistingLValue(FuzzType type, int minRefEscapeScope)
    {
        Debug.Assert(type == null || !(type is RefType));

        LValueKind kind = (LValueKind)Options.ExistingLValueDist.Sample(_random.Rng);

        if (kind == LValueKind.RefReturningCall)
        {
            List<FuncGenerator> refReturningFuncs = new();
            foreach (FuncGenerator func in _funcs.Skip(_funcIndex + 1))
            {
                if (func.ReturnType is not RefType rt)
                    continue;

                if (type == null || type == rt.InnerType)
                    refReturningFuncs.Add(func);
            }

            // If we don't have a ref-returning function, fall back to local/static by retrying.
            // The reason we don't always fall back to trying other options is that otherwise
            // we may stack overflow in cases like ref int M(ref int a), if we have no locals
            // or statics of type int. We would keep generating method calls in this case.
            // This is kind of a hack, although it is pretty natural to pick statics/locals for
            // lvalues, so it is not that big of a deal.
            if (refReturningFuncs.Count == 0)
                return GenExistingLValue(type, minRefEscapeScope);

            FuncGenerator funcToCall = _random.NextElement(refReturningFuncs);

            ExpressionSyntax invocExpr;
            ArgumentSyntax[] args;
            int argsMinRefEscapeScope;

            using (var genAwaits = new GenerateAwaitsScope(this))
            {
                invocExpr = GenInvocFuncExpr(funcToCall, genAwaits);
                // When calling a func that returns a by-ref, we need to take into account that the C# compiler
                // performs 'escape-analysis' on by-refs. If we want to produce a non-local by-ref we are only
                // allowed to pass non-local by-refs. The following example illustrates the analysis performed
                // by the compiler:
                // ref int M(ref int b, ref int c) {
                //   int a = 2;
                //   return ref Max(ref a, ref b); // compiler error, returned by-ref could point to 'a' and escape
                //   return ref Max(ref b, ref c); // ok, returned ref cannot point to local
                // }
                // ref int Max(ref int a, ref int b) { ... }
                // This is the reason for the second arg passed to GenArgs. For example, if we want a call to return
                // a ref that may escape, we need a minimum ref escape scope of 1, since 0 could pass refs to locals,
                // and the result of that call would not be valid to return.
                args = GenArgs(funcToCall.Parameters.Select(p => p.Type).ToArray(), null, minRefEscapeScope, out argsMinRefEscapeScope, genAwaits);
            }

            InvocationExpressionSyntax invoc =
                InvocationExpression(
                    invocExpr,
                    ArgumentList(
                        SeparatedList(
                            args)));

            return new LValueInfo(invoc, ((RefType)funcToCall.ReturnType).InnerType, argsMinRefEscapeScope, readOnly: false, asyncHoistable: false, null);
        }

        List<LValueInfo> lvalues = CollectVariablePaths(type, minRefEscapeScope, kind == LValueKind.Local, kind == LValueKind.Static, requireAssignable: true);
        if (lvalues.Count == 0)
        {
            // We typically fall back to generating a static, so just try to find a static if we found no local.
            if (kind != LValueKind.Local)
                return null;

            lvalues = CollectVariablePaths(type, minRefEscapeScope, false, true, requireAssignable: true);
            if (lvalues.Count == 0)
                return null;
        }

        return _random.NextElement(lvalues);
    }

    /// <summary>
    /// Generates an access to a random member:
    /// * A static variable
    /// * A local variable
    /// * An argument
    /// * A field in one of these, if aggregate type
    /// * An element in one of these, if array type
    /// </summary>
    private ExpressionSyntax GenMemberAccess(FuzzType ft)
    {
        List<LValueInfo> paths =
            _random.FlipCoin(Options.MemberAccessSelectLocalProb)
            ? CollectVariablePaths(ft, int.MinValue, true, false, requireAssignable: false)
            : CollectVariablePaths(ft, int.MinValue, false, true, requireAssignable: false);

        return paths.Count > 0 ? _random.NextElement(paths).Expression : null;
    }

    /// <summary>
    /// Collect lvalues of the specified type.
    /// </summary>
    /// <param name="type">The type, or null to collect all lvalues.</param>
    /// <param name="minRefEscapeScope">The minimum scope that refs taken of the lvalue must be able to escape to.</param>
    /// <param name="collectLocals">Whether to collect locals.</param>
    /// <param name="collectStatics">Whether to collect statics.</param>
    /// <param name="requireAssignable">Whether the collected lvalues must be able to appear as the LHS of an assignment with an RHS of type <paramref name="type"/>.</param>
    private List<LValueInfo> CollectVariablePaths(FuzzType type, int minRefEscapeScope, bool collectLocals, bool collectStatics, bool requireAssignable)
    {
        List<LValueInfo> paths = new();

        if (collectLocals)
        {
            foreach (ScopeFrame sf in _scope)
            {
                foreach (ScopeValue variable in sf.Values)
                {
                    AppendVariablePaths(paths, variable, asyncHoistable: !Options.GenExtensions.Contains(Extension.RuntimeAsync));
                }
            }
        }

        if (collectStatics)
        {
            foreach (StaticField stat in _statics.Fields)
            {
                AppendVariablePaths(paths, new ScopeValue(stat.Type, AccessStatic(stat), int.MaxValue, false), asyncHoistable: !Options.GenExtensions.Contains(Extension.RuntimeAsync));
            }
        }

        Debug.Assert(type == null || !(type is RefType), "Cannot collect variables of ref type");

        // Verify that a type is allowed. Often we want to implicitly promote
        // a by-ref to its inner type, because we are taking a ref anyway or using
        // its value, and it is automatically promoted.
        // This checks for both of these cases:
        // ref int lhs = ...;
        // int rhs1 = ...;
        // ref int rhs2 = ...;
        // lhs = ref rhs1; // must be supported
        // lhs = ref rhs2; // must be supported
        bool IsAllowedType(FuzzType other)
        {
            if (type == null)
                return true;

            if (other is RefType rt)
                other = rt.InnerType; // Implicit deref

            if (other == type)
                return true;

            if (!requireAssignable)
            {
                // Allow implicit conversion from collected variable to type
                if (type is InterfaceType it && other is AggregateType agg && agg.Implements(it))
                    return true;
            }

            return false;
        }

        paths.RemoveAll(lv => lv.RefEscapeScope < minRefEscapeScope || (lv.ReadOnly && requireAssignable) || !IsAllowedType(lv.Type));
        return paths;
    }

    private ExpressionSyntax GenLiteral(FuzzType type)
    {
        return LiteralGenerator.GenLiteral(_types, _random, type);
    }

    private ExpressionSyntax GenUnary(FuzzType type)
    {
        if (type is not PrimitiveType pt)
            return null;

        if (pt.Keyword == SyntaxKind.BoolKeyword)
        {
            return PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, ParenthesizeIfNecessary(GenNonLiteralExpression(type)));
        }

        SyntaxKind op = pt.Keyword switch
        {
            // Unary minus is not valid for ulong, so there we always pick ~
            SyntaxKind.ULongKeyword => SyntaxKind.BitwiseNotExpression,
            SyntaxKind.FloatKeyword or SyntaxKind.DoubleKeyword => (SyntaxKind)Options.UnaryFloatDist.Sample(_random.Rng),
            _ => (SyntaxKind)Options.UnaryFloatDist.Sample(_random.Rng),
        };

        ExpressionSyntax expr = PrefixUnaryExpression(op, ParenthesizeIfNecessary(GenNonLiteralExpression(type)));

        UnOpTable table = op switch
        {
            SyntaxKind.UnaryPlusExpression => UnOpTable.UnaryPlus,
            SyntaxKind.UnaryMinusExpression => UnOpTable.UnaryMinus,
            SyntaxKind.BitwiseNotExpression => UnOpTable.BitwiseNot,
            _ => throw new Exception("Unexpected syntax kind sampled for unary integer op: " + op),
        };

        Debug.Assert(table.GetResultType(pt.Keyword).HasValue);
        if (table.GetResultType(pt.Keyword).Value != pt.Keyword)
            expr = CastExpression(type.GenReferenceTo(), ParenthesizedExpression(expr));

        return expr;
    }

    private ExpressionSyntax GenBinary(FuzzType type)
    {
        if (type is not PrimitiveType pt)
            return null;

        if (pt.Keyword == SyntaxKind.BoolKeyword)
            return GenBoolProducingBinary();

        return GenNumericProducingBinary(pt);
    }

    private ExpressionSyntax GenBoolProducingBinary()
    {
        PrimitiveType leftType;
        PrimitiveType rightType;
        SyntaxKind op = (SyntaxKind)Options.BinaryBoolDist.Sample(_random.Rng);
        if (op == SyntaxKind.LogicalAndExpression || op == SyntaxKind.LogicalOrExpression ||
            op == SyntaxKind.ExclusiveOrExpression || op == SyntaxKind.BitwiseAndExpression ||
            op == SyntaxKind.BitwiseOrExpression)
        {
            // &&, ||, ^, &, | must be between bools to produce bool
            PrimitiveType boolType = _types.GetPrimitiveType(SyntaxKind.BoolKeyword);
            leftType = rightType = boolType;
        }
        else if (op == SyntaxKind.EqualsExpression || op == SyntaxKind.NotEqualsExpression)
        {
            leftType = _types.PickPrimitiveType(f => true);
            rightType = _types.PickPrimitiveType(f => BinOpTable.Equality.GetResultType(leftType.Keyword, f.Keyword).HasValue);
        }
        else
        {
            Debug.Assert(op == SyntaxKind.LessThanOrEqualExpression || op == SyntaxKind.LessThanExpression ||
                         op == SyntaxKind.GreaterThanOrEqualExpression || op == SyntaxKind.GreaterThanExpression);

            leftType = _types.PickPrimitiveType(f => f.Keyword != SyntaxKind.BoolKeyword);
            rightType = _types.PickPrimitiveType(f => BinOpTable.Relop.GetResultType(leftType.Keyword, f.Keyword).HasValue);
        }

        var (left, right) = GenLeftRightForBinary(leftType, rightType);

        return BinaryExpression(op, ParenthesizeIfNecessary(left), ParenthesizeIfNecessary(right));
    }

    private ExpressionSyntax ParenthesizeIfNecessary(ExpressionSyntax expr)
    {
        if (Helpers.RequiresParentheses(expr))
            return ParenthesizedExpression(expr);

        return expr;
    }

    private ExpressionSyntax GenNumericProducingBinary(PrimitiveType type)
    {
        Debug.Assert(type.Info.IsNumeric);
        SyntaxKind op = (SyntaxKind)Options.BinaryIntegralDist.Sample(_random.Rng);

        PrimitiveType leftType;
        BinOpTable table;
        if (op is SyntaxKind.LeftShiftExpression or SyntaxKind.RightShiftExpression)
        {
            table = BinOpTable.Shifts;
            leftType = _types.PickPrimitiveType(f => f.Info.IsNumeric && !f.Info.IsFloat);
        }
        else if (op is SyntaxKind.BitwiseAndExpression or SyntaxKind.BitwiseOrExpression or SyntaxKind.ExclusiveOrExpression)
        {
            table = BinOpTable.BitwiseOps;
            leftType = _types.PickPrimitiveType(f => f.Info.IsNumeric && !f.Info.IsFloat);
        }
        else
        {
            table = BinOpTable.Arithmetic;
            leftType = _types.PickPrimitiveType(f => f.Info.IsNumeric);
        }

        PrimitiveType rightType =
            _types.PickPrimitiveType(f => table.GetResultType(leftType.Keyword, f.Keyword).HasValue);

        var (left, right) = GenLeftRightForBinary(leftType, rightType);

        var resultType = table.GetResultType(leftType.Keyword, rightType.Keyword).Value;

        if ((op is SyntaxKind.ModuloExpression or SyntaxKind.DivideExpression) &&
            (resultType is not SyntaxKind.FloatKeyword and not SyntaxKind.DoubleKeyword))
        {
            // Integer division can throw -- or by 1 to make sure this doesn't happen.
            right = CastExpression(
                rightType.GenReferenceTo(),
                ParenthesizedExpression(
                    BinaryExpression(
                        SyntaxKind.BitwiseOrExpression,
                        ParenthesizeIfNecessary(right),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))));
        }

        ExpressionSyntax expr = BinaryExpression(op, ParenthesizeIfNecessary(left), ParenthesizeIfNecessary(right));

        if (table.GetResultType(leftType.Keyword, rightType.Keyword) != type.Keyword)
            expr = CastExpression(type.GenReferenceTo(), ParenthesizedExpression(expr));

        return expr;
    }

    private static bool IsLiteralOrCastOfLiteral(ExpressionSyntax expression)
    {
        return expression is LiteralExpressionSyntax || expression is CastExpressionSyntax { Expression: LiteralExpressionSyntax };
    }

    private (ExpressionSyntax, ExpressionSyntax) GenLeftRightForBinary(FuzzType leftType, FuzzType rightType)
    {
        ExpressionSyntax left = GenExpression(leftType);
        // There are two reasons we don't allow both left and right to be literals:
        // 1. If the computation overflows, this gives a C# compiler error
        // 2. The compiler is required to constant fold these expressions which is not interesting.
        if (IsLiteralOrCastOfLiteral(left))
            return (left, GenNonLiteralExpression(rightType));

        return (left, GenExpression(rightType));
    }

    private ExpressionSyntax GenNonLiteralExpression(FuzzType type)
    {
        ExpressionSyntax gen;
        do
        {
            gen = GenExpression(type);
        } while (IsLiteralOrCastOfLiteral(gen));

        return gen;
    }

    private ExpressionSyntax GenCall(FuzzType type, bool allowNew)
    {
        Debug.Assert(!(type is RefType), "Cannot GenCall to ref type -- use GenExistingLValue for that");

        FuncGenerator func = null;
        FuzzType returnType = null;
        FuzzType[] parameterTypes = null;
        ParameterMetadata[] parameterMetadata = null;
        ExpressionSyntax invocFuncExpr = null;
        bool addCastsOnArgs = false;

        if (allowNew && _random.FlipCoin(Options.GenNewFunctionProb) && !Options.FuncGenRejection.Reject(_funcs.Count, _random.Rng))
        {
            type ??= _types.PickType(Options.ReturnTypeIsByRefProb);

            func = new FuncGenerator(_funcs, _random, _types, _statics, _apis, _genChecksumSiteId);
            func.Generate(returnType: type, randomizeParams: true);
        }
        else
        {
            if (_apis.Any() && _random.FlipCoin(Options.GenCallToApiProb))
            {
                Api api = _apis.PickRandomApi(type);
                if (api == null)
                    return null;
                type ??= api.ReturnType;
                returnType = api.ReturnType;
                parameterTypes = api.ParameterTypes;
                parameterMetadata = api.ParameterMetadata;
                invocFuncExpr = ParseExpression($"{api.ClassName}.{api.MethodName}");
                // Add casts on args to force selection of right overload.
                // GenExpression(type) may generate a more precise type than
                // 'type' that can cause a different overload to be selected.
                addCastsOnArgs = true;
            }
            else
            {
                IEnumerable<FuncGenerator> funcs =
                    _funcs
                    .Skip(_funcIndex + 1)
                    .Where(candidate =>
                    {
                        // Make sure we do not get too many leaf calls. Here we compute what the new transitive
                        // number of calls would be to each function, and if it's too much, reject this candidate.
                        // Note that we will never reject calling a leaf function directly, even if the +1 puts
                        // us over the cap. That is intentional. We only want to limit the exponential growth
                        // which happens when functions call functions multiple times, and those functions also
                        // call functions multiple times.
                        foreach (var (transFunc, transNumCall) in candidate.CallCounts)
                        {
                            CallCounts.TryGetValue(transFunc, out long curNumCalls);
                            if (curNumCalls + transNumCall > Options.SingleFunctionMaxTotalCalls)
                                return false;
                        }

                        return true;
                    });

                if (type != null)
                    funcs = funcs.Where(f => f.ReturnType.IsCastableTo(type) || (f.ReturnType is RefType rt && rt.InnerType.IsCastableTo(type)));

                List<FuncGenerator> list = funcs.ToList();
                if (list.Count == 0)
                    return null;

                func = _random.NextElement(list);
                type ??= func.ReturnType;
            }
        }

        ArgumentSyntax[] args;

        using (var genAwaits = new GenerateAwaitsScope(this))
        {
            // Update transitive call counts before generating args, so we decrease chance of
            // calling the same methods in the arg expressions.
            if (func != null)
            {
                CallCounts.TryGetValue(func.FuncIndex, out long numCalls);
                CallCounts[func.FuncIndex] = numCalls + 1;

                foreach (var (transFunc, transNumCalls) in func.CallCounts)
                {
                    CallCounts.TryGetValue(transFunc, out long curNumCalls);
                    CallCounts[transFunc] = curNumCalls + transNumCalls;
                }

                returnType = func.ReturnType;
                parameterTypes = func.Parameters.Select(p => p.Type).ToArray();
                invocFuncExpr = GenInvocFuncExpr(func, genAwaits);
            }

            args = GenArgs(parameterTypes, parameterMetadata, 0, out _, genAwaits);
        }

        if (addCastsOnArgs)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].WithExpression(CastExpression(parameterTypes[i].GenReferenceTo(), ParenthesizeIfNecessary(args[i].Expression)));
            }
        }

        ExpressionSyntax invoc =
            InvocationExpression(
                invocFuncExpr,
                ArgumentList(
                    SeparatedList(args)));

        if (func != null && func.IsAsync)
        {
            if (_isAsync && _awaitDisallowed == 0)
            {
                invoc = AwaitExpression(invoc);
            }
            else
            {
                invoc =
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    invoc,
                                    IdentifierName("GetAwaiter"))),
                            IdentifierName("GetResult")));
            }
        }

        if (returnType == type || (returnType is AggregateType agg && type is InterfaceType it && agg.Implements(it)) || (returnType is RefType retRt && retRt.InnerType == type))
            return invoc;

        return CastExpression(type.GenReferenceTo(), invoc);
    }

    // Generates the expression that comes before the parentheses of the call.
    private ExpressionSyntax GenInvocFuncExpr(FuncGenerator func, GenerateAwaitsScope genAwaitsForArgs)
    {
        FuzzType funcThisType = (FuzzType)func.InstanceType ?? func.InterfaceType;
        if (_isInPrimaryClass && funcThisType == null)
        {
            // Both are in primary class, call directly without any class prefix
            return IdentifierName(func.Name);
        }

        if (funcThisType == null)
        {
            // Callee is in primary class, invoke it as static function with prefix
            return
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(CodeGenerator.ClassNameForStaticMethods),
                    IdentifierName(func.Name));
        }

        // Instance method, generate receiver
        ExpressionSyntax receiver = GenExpression(funcThisType);

        if (funcThisType is AggregateType { IsClass: false })
        {
            // If the receive is a struct then we may be calling this on an
            // unhoistable lvalue. In that case disallow awaits for the
            // remaining arguments, to avoid generating e.g. "GetS().Foo(await
            // Something())".
            genAwaitsForArgs.Disallow();
        }

        return
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                ParenthesizeIfNecessary(receiver),
                IdentifierName(func.Name));
    }

    private ArgumentSyntax[] GenArgs(FuzzType[] parameterTypes, ParameterMetadata[] parameterMetadata, int minRefEscapeScope, out int argsMinRefEscapeScope, GenerateAwaitsScope genAwaitsForArgs)
    {
        ArgumentSyntax[] args = new ArgumentSyntax[parameterTypes.Length];
        argsMinRefEscapeScope = int.MaxValue;

        for (int i = 0; i < args.Length; i++)
        {
            FuzzType paramType = parameterTypes[i];
            if (paramType is RefType rt)
            {
                LValueInfo lv = GenLValue(rt.InnerType, minRefEscapeScope);
                argsMinRefEscapeScope = Math.Min(argsMinRefEscapeScope, lv.RefEscapeScope);
                args[i] = Argument(lv.Expression).WithRefKindKeyword(Token(SyntaxKind.RefKeyword));

                if (!lv.AsyncHoistable)
                {
                    genAwaitsForArgs.Disallow();
                }
                continue;
            }

            if (parameterMetadata != null && parameterMetadata[i] is ParameterMetadata pm && pm.ConstantExpected)
            {
                Debug.Assert(paramType is PrimitiveType);
                PrimitiveType pt = (PrimitiveType)paramType;

                Debug.Assert(pt.Info.IsNumeric);
                args[i] = Argument(pt.Info.GenInRange(pm.ConstantMin, pm.ConstantMax, _random.Rng));
                continue;
            }

            args[i] = Argument(GenExpression(paramType));
        }

        return args;
    }

    private ExpressionSyntax GenIncDec(FuzzType type, bool isIncrement)
    {
        SyntaxKind[] acceptedTypes =
        [
            SyntaxKind.ULongKeyword,
            SyntaxKind.LongKeyword,
            SyntaxKind.UIntKeyword,
            SyntaxKind.IntKeyword,
            SyntaxKind.UShortKeyword,
            SyntaxKind.ShortKeyword,
            SyntaxKind.ByteKeyword,
            SyntaxKind.SByteKeyword,
            SyntaxKind.FloatKeyword,
            SyntaxKind.DoubleKeyword,
        ];

        if (type is not PrimitiveType pt || !acceptedTypes.Contains(pt.Keyword))
            return null;

        LValueInfo subject = GenExistingLValue(type, int.MinValue);
        if (subject == null)
            return null;

        ExpressionSyntax gen = PostfixUnaryExpression(
            isIncrement ? SyntaxKind.PostIncrementExpression : SyntaxKind.PostDecrementExpression,
            subject.Expression);

        return gen;
    }

    private ExpressionSyntax GenNewObject(FuzzType type)
    {
        if (type is not AggregateType at)
        {
            if (type is InterfaceType it)
            {
                at = _random.NextElement(_types.GetImplementingTypes(it));
            }
            else if (type is VectorType vt)
            {
                int? numElems = vt.NumElements();
                VectorCreationKind creationKind;
                do
                {
                    creationKind = (VectorCreationKind)Options.CreateVectorKindDist.Sample(_random.Rng);
                } while (creationKind == VectorCreationKind.Create && (!numElems.HasValue || numElems.Value > 4));

                return GenVectorCreation(creationKind, vt, () => GenExpression(vt.ElementType));
            }
            else
            {
                return null;
            }
        }

        ObjectCreationExpressionSyntax creation =
            ObjectCreationExpression(at.GenReferenceTo())
            .WithArgumentList(
                ArgumentList(
                    SeparatedList(
                        at.Fields.Select(f => Argument(GenExpression(f.Type))))));

        return creation;
    }

    internal static ExpressionSyntax GenVectorCreation(VectorCreationKind creationKind, VectorType vt, Func<ExpressionSyntax> genElement)
    {
        int numArgs = creationKind switch
        {
            VectorCreationKind.Create => vt.NumElements().Value,
            _ => 1
        };

        ArgumentSyntax[] arguments = new ArgumentSyntax[numArgs];
        for (int i = 0; i < arguments.Length; i++)
        {
            ExpressionSyntax expr = CastExpression(vt.ElementType.GenReferenceTo(), genElement());
            arguments[i] = Argument(expr);
        }

        SimpleNameSyntax memberName = creationKind switch
        {
            VectorCreationKind.Create => IdentifierName("Create"),
            VectorCreationKind.CreateBroadcast =>
                GenericName("Create")
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SingletonSeparatedList(
                            vt.ElementType.GenReferenceTo()))),

            _ => IdentifierName("CreateScalar"),
        };

        string baseName = vt.GetBaseName();
        bool isVectorTCreateScalar = vt.Width == VectorTypeWidth.WidthUnknown && creationKind == VectorCreationKind.CreateScalar;
        if (isVectorTCreateScalar)
        {
            // No CreateScalar for Vector<T> yet. We will use
            // Vector128.CreateScalar(x).AsVector().
            baseName = "Vector128";
        }

        ExpressionSyntax invoc =
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(baseName),
                    memberName))
            .WithArgumentList(
                ArgumentList(SeparatedList(arguments)));

        if (isVectorTCreateScalar)
        {
            invoc = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, invoc, IdentifierName("AsVector")));
        }

        return invoc;
    }

    internal static IEnumerable<StatementSyntax> GenChecksumming(bool prefixRuntimeAccess, IEnumerable<ScopeValue> variables, Func<string> siteIdGenerator)
    {
        List<LValueInfo> paths = new();
        foreach (ScopeValue variable in variables)
            AppendVariablePaths(paths, variable, asyncHoistable: true);

        paths.RemoveAll(lv =>
        {
            FuzzType innerType = lv.Type is RefType rt ? rt.InnerType : lv.Type;
            return innerType is not PrimitiveType and not VectorType;
        });

        foreach (LValueInfo lvalue in paths)
        {
            string checksumSiteId = siteIdGenerator();
            LiteralExpressionSyntax id =
                LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal(checksumSiteId));

            ExpressionSyntax expr = lvalue.Expression;

            FuzzType innerType = lvalue.Type is RefType rt ? rt.InnerType : lvalue.Type;

            string checksumFuncName = "Checksum";
            if (innerType is PrimitiveType pt && pt.Info.IsFloat)
            {
                checksumFuncName = pt.Keyword == SyntaxKind.FloatKeyword ? "ChecksumSingle" : "ChecksumDouble";
            }
            else if (innerType is VectorType vt && vt.ElementType.Info.IsFloat)
            {
                checksumFuncName = vt.ElementType.Keyword == SyntaxKind.FloatKeyword ? "ChecksumSingles" : "ChecksumDoubles";
            }

            ExpressionSyntax checksumCall;

            if (prefixRuntimeAccess)
            {
                checksumCall =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(CodeGenerator.ClassNameForStaticMethods),
                            IdentifierName("s_rt")),
                        IdentifierName(checksumFuncName));
            }
            else
            {
                checksumCall =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("s_rt"),
                        IdentifierName(checksumFuncName));
            }

            ExpressionStatementSyntax stmt =
                ExpressionStatement(
                    InvocationExpression(checksumCall)
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList(new[] { Argument(id), Argument(expr) }))));

            yield return stmt.WithAdditionalAnnotations(new SyntaxAnnotation("ChecksumSiteId", checksumSiteId));
        }
    }

    private static void AppendVariablePaths(List<LValueInfo> paths, ScopeValue var, bool asyncHoistable)
    {
        AddPathsRecursive(var.Expression, var.Type, var.RefEscapeScope, var.ReadOnly);

        void AddPathsRecursive(ExpressionSyntax curAccess, FuzzType curType, int curRefEscapeScope, bool readOnly)
        {
            LValueInfo info = new(curAccess, curType, curRefEscapeScope, readOnly, asyncHoistable: asyncHoistable, var);
            paths.Add(info);

            if (curType is RefType rt)
                curType = rt.InnerType;

            switch (curType)
            {
                case ArrayType arr:
                    AddPathsRecursive(
                        ElementAccessExpression(
                            curAccess,
                            BracketedArgumentList(
                                SeparatedList(
                                    Enumerable.Repeat(
                                        Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                                        arr.Rank)))),
                        arr.ElementType,
                        curRefEscapeScope: int.MaxValue,
                        readOnly: false);
                    break;
                case AggregateType agg:
                    foreach (AggregateField field in agg.Fields)
                    {
                        AddPathsRecursive(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                curAccess,
                                IdentifierName(field.Name)),
                            field.Type,
                            curRefEscapeScope: agg.IsClass ? int.MaxValue : curRefEscapeScope,
                            readOnly: readOnly && !agg.IsClass);
                    }
                    break;
            }
        }
    }

    private class GenerateAwaitsScope : IDisposable
    {
        public FuncBodyGenerator _gen;
        private int _numDisallowed;

        public GenerateAwaitsScope(FuncBodyGenerator gen)
        {
            _gen = gen;
        }

        public void Disallow()
        {
            _numDisallowed++;
            _gen._awaitDisallowed++;
        }

        public void Dispose()
        {
            _gen._awaitDisallowed -= -_numDisallowed;
            _numDisallowed = 0;
        }
    }
}

internal enum StatementKind
{
    Block,
    Assignment,
    Call,
    If,
    Switch,
    Return,
    Throw,
    TryCatch,
    TryFinally,
    Loop,
    Yield,
}

internal enum ExpressionKind
{
    MemberAccess,
    Literal,
    Unary,
    Binary,
    Assignment,
    Call,
    Increment,
    Decrement,
    NewObject,
}
