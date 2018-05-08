using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    partial class RandomSyntaxGenerator
    {
        [Recursive]
        public BlockSyntax GenBlock(
            List<StatementSyntax> statements)
            => Block(statements.ToSyntaxList());

        public BreakStatementSyntax GenBreak()
            => BreakStatement();

        [Recursive]
        public CheckedStatementSyntax GenCheckedStatement(BlockSyntax block)
            => CheckedStatement(SyntaxKind.CheckedStatement, block);
        
        [Recursive]
        public ForEachStatementSyntax GenForEachStatementSyntax(
            TypeSyntax type,
            string identifier,
            ExpressionSyntax expression,
            StatementSyntax statement)
            => ForEachStatement(type, identifier, expression, statement);

        [Recursive]
        public ForEachVariableStatementSyntax GenForEachVariableStatementSyntax(
            ExpressionSyntax variable,
            ExpressionSyntax expression,
            StatementSyntax statement)
            => ForEachVariableStatement(variable, expression, statement);

        public ContinueStatementSyntax GenContinueStatement()
            => ContinueStatement();

        [Recursive]
        public DoStatementSyntax GenDoStatement(StatementSyntax statement, ExpressionSyntax condition)
            => DoStatement(statement, condition);

        public EmptyStatementSyntax GenEmptyStatement()
            => EmptyStatement();

        [Recursive]
        public ExpressionStatementSyntax GenExpressionStatement(ExpressionSyntax expression)
            => ExpressionStatement(expression);

        [Recursive]
        public FixedStatementSyntax GenFixedStatement(VariableDeclarationSyntax declaration, StatementSyntax statement)
            => FixedStatement(declaration, statement);

        [Recursive]
        public ForStatementSyntax GenForStatementSyntax(StatementSyntax statement)
            => ForStatement(statement);

        [Recursive]
        public ForStatementSyntax GenForStatementSyntax(
            VariableDeclarationSyntax declaration,
            List<ExpressionSyntax> initializers,
            ExpressionSyntax condition,
            List<ExpressionSyntax> incrementors,
            StatementSyntax statement
            )
        {
            return ForStatement(
                declaration,
                SeparatedList(initializers),
                condition,
                SeparatedList(incrementors),
                statement);
        }

        [Recursive]
        public GotoStatementSyntax GenGotoStatement(ExpressionSyntax expression)
            => GotoStatement(SyntaxKind.GotoStatement, expression);
        
        [Recursive]
        public GotoStatementSyntax GenCasedGotoStatement(ExpressionSyntax expression)
            => GotoStatement(SyntaxKind.GotoStatement, Token(SyntaxKind.CaseKeyword), expression);
            
        [Recursive]
        public GotoStatementSyntax GenDefaultGotoStatement()
            => GotoStatement(SyntaxKind.GotoStatement, Token(SyntaxKind.DefaultKeyword), null);
        [Recursive]
        public IfStatementSyntax GenIfStatement(ExpressionSyntax expression, StatementSyntax statement)
            => IfStatement(expression, statement);
        [Recursive]
        public IfStatementSyntax GenIfStatement(ExpressionSyntax expression, StatementSyntax statement, ElseClauseSyntax elseClause)
            => IfStatement(expression, statement, elseClause);

        [Recursive]
        public LabeledStatementSyntax GenLabeledStatement(string identifier, StatementSyntax statement)
            => LabeledStatement(identifier, statement);

        [Recursive]
        public LocalDeclarationStatementSyntax GenLocalDeclarationStatement(VariableDeclarationSyntax declaration)
            => LocalDeclarationStatement(declaration);

        [Recursive]
        public LocalDeclarationStatementSyntax GenModifiedLocalDeclarationStatement(VariableDeclarationSyntax declaration)
            => LocalDeclarationStatement(GenModifiers(), declaration);

        [Recursive]
        public LockStatementSyntax GenLockStatement(ExpressionSyntax expression, StatementSyntax statement)
            => LockStatement(expression, statement);

        [Recursive]
        public ReturnStatementSyntax GenReturnStatement(ExpressionSyntax expression)
            => ReturnStatement(expression);
        
        [Recursive]
        public SwitchStatementSyntax GenSwitchStatement(ExpressionSyntax expression)
            => SwitchStatement(expression);
        
        [Recursive]
        public SwitchStatementSyntax GenSwitchStatement(ExpressionSyntax expression, List<SwitchSectionSyntax> sections)
            => SwitchStatement(expression, sections.ToSyntaxList());

        [Recursive]
        public ThrowStatementSyntax GenThrowStatement(ExpressionSyntax expression)
            => ThrowStatement(expression);

        [Recursive]
        public TryStatementSyntax GenTryStatement(List<CatchClauseSyntax> catches)
            => TryStatement(catches.ToSyntaxList());

        [Recursive]
        public TryStatementSyntax GenTryStatement(BlockSyntax block, List<CatchClauseSyntax> catches, FinallyClauseSyntax finallyClause)
            => TryStatement(block, catches.ToSyntaxList(), finallyClause);

        public CatchClauseSyntax GenCatchClause()
            => CatchClause();

        public CatchClauseSyntax GenCatchClause(CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
            => CatchClause(declaration, filter, block);

        public CatchDeclarationSyntax GenCatchDeclaration(TypeSyntax type)
            => CatchDeclaration(type);

        public CatchDeclarationSyntax GenCatchDeclaration(TypeSyntax type, string identifier)
            => CatchDeclaration(type, Identifier(identifier));

        public FinallyClauseSyntax GenFinallyClause(BlockSyntax block)
            => FinallyClause(block);

        [Recursive]
        public CatchFilterClauseSyntax GenCatchFilterClause(ExpressionSyntax filterExpression)
            => CatchFilterClause(filterExpression);

        [Recursive]
        public UnsafeStatementSyntax GenUnsafeStatement(BlockSyntax block)
            => UnsafeStatement(block);

        [Recursive]
        public UsingStatementSyntax GenUsingStatement(StatementSyntax statement)
            => UsingStatement(statement);

        [Recursive]
        public UsingStatementSyntax GenUsingStatement(VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
            => UsingStatement(declaration, expression, statement);

        [Recursive]
        public WhileStatementSyntax GenWhileStatement(ExpressionSyntax expression, StatementSyntax statement)
            => WhileStatement(expression, statement);

        [Recursive]
        public YieldStatementSyntax GenYieldBreakStatement(ExpressionSyntax expression)
            => YieldStatement(SyntaxKind.YieldBreakStatement, expression);

        [Recursive]
        public YieldStatementSyntax GenYieldReturnStatement(ExpressionSyntax expression)
            => YieldStatement(SyntaxKind.YieldReturnStatement, expression);

        public LocalFunctionStatementSyntax GenLocalFunctionStatement(TypeSyntax returnType, string identifier)
            => LocalFunctionStatement(returnType, identifier);

        [Recursive]
        public ElseClauseSyntax GenElseClause(StatementSyntax statement)
            => ElseClause(statement);

        public SwitchSectionSyntax GenSwitchSection()
            => SwitchSection();

        [Recursive]
        public SwitchSectionSyntax GenSwitchSection(List<SwitchLabelSyntax> labels, List<StatementSyntax> statements)
            => SwitchSection(labels.ToSyntaxList(), statements.ToSyntaxList());

        [Recursive]
        public SwitchLabelSyntax GenSwitchLabel(ExpressionSyntax expression)
            => CaseSwitchLabel(expression);

        public SwitchLabelSyntax GenSwitchLabel(PatternSyntax pattern)
            => CasePatternSwitchLabel(pattern, Token(SyntaxKind.ColonToken));

        public SwitchLabelSyntax GenSwitchLabel()
            => DefaultSwitchLabel();

        [Recursive]
        public PatternSyntax GenPattern(ExpressionSyntax expression)
            => ConstantPattern(expression);

        public PatternSyntax GenPattern(TypeSyntax type, VariableDesignationSyntax designation)
            => DeclarationPattern(type, designation);

        public VariableDesignationSyntax GenVariableDesignation()
            => DiscardDesignation();

        public VariableDesignationSyntax GenVariableDesignation(List<VariableDesignationSyntax> designations)
            => ParenthesizedVariableDesignation(SeparatedList(designations));

        public VariableDesignationSyntax GenVariableDesignation(string identifier)
            => SingleVariableDesignation(Identifier(identifier));
    }
}
