using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    partial class RandomSyntaxGenerator
    {
        public BlockSyntax GenBlock(
            List<StatementSyntax> statements)
            => Block(statements.ToSyntaxList());

        public BreakStatementSyntax GenBreak()
            => BreakStatement();

        public CheckedStatementSyntax GenCheckedStatement(BlockSyntax block)
            => CheckedStatement(SyntaxKind.CheckedStatement, block);
        
        public ForEachStatementSyntax GenForEachStatementSyntax(
            TypeSyntax type,
            string identifier,
            ExpressionSyntax expression,
            StatementSyntax statement)
            => ForEachStatement(type, identifier, expression, statement);

        public ForEachVariableStatementSyntax GenForEachVariableStatementSyntax(
            ExpressionSyntax variable,
            ExpressionSyntax expression,
            StatementSyntax statement)
            => ForEachVariableStatement(variable, expression, statement);

        public ContinueStatementSyntax GenContinueStatement()
            => ContinueStatement();

        public DoStatementSyntax GenDoStatement(StatementSyntax statement, ExpressionSyntax condition)
            => DoStatement(statement, condition);

        public EmptyStatementSyntax GenEmptyStatement()
            => EmptyStatement();

        public ExpressionStatementSyntax GenExpressionStatement(ExpressionSyntax expression)
            => ExpressionStatement(expression);

        public FixedStatementSyntax GenFixedStatement(VariableDeclarationSyntax declaration, StatementSyntax statement)
            => FixedStatement(declaration, statement);

        public ForStatementSyntax GenForStatementSyntax(StatementSyntax statement)
            => ForStatement(statement);

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

        public GotoStatementSyntax GenGotoStatement(ExpressionSyntax expression)
            => GotoStatement(SyntaxKind.GotoStatement, expression);
        
        public GotoStatementSyntax GenCasedGotoStatement(ExpressionSyntax expression)
            => GotoStatement(SyntaxKind.GotoStatement, Token(SyntaxKind.CaseKeyword), expression);
            
        public GotoStatementSyntax GenDefaultGotoStatement()
            => GotoStatement(SyntaxKind.GotoStatement, Token(SyntaxKind.DefaultKeyword), null);
        public IfStatementSyntax GenIfStatement(ExpressionSyntax expression, StatementSyntax statement)
            => IfStatement(expression, statement);
        public IfStatementSyntax GenIfStatement(ExpressionSyntax expression, StatementSyntax statement, ElseClauseSyntax elseClause)
            => IfStatement(expression, statement, elseClause);

        public LabeledStatementSyntax GenLabeledStatement(string identifier, StatementSyntax statement)
            => LabeledStatement(identifier, statement);

        public LocalDeclarationStatementSyntax GenLocalDeclarationStatement(VariableDeclarationSyntax declaration)
            => LocalDeclarationStatement(declaration);

        public LocalDeclarationStatementSyntax GenModifiedLocalDeclarationStatement(VariableDeclarationSyntax declaration)
            => LocalDeclarationStatement(GenModifiers(), declaration);

        public LockStatementSyntax GenLockStatement(ExpressionSyntax expression, StatementSyntax statement)
            => LockStatement(expression, statement);

        public ReturnStatementSyntax GenReturnStatement(ExpressionSyntax expression)
            => ReturnStatement(expression);
        
        public SwitchStatementSyntax GenSwitchStatement(ExpressionSyntax expression)
            => SwitchStatement(expression);
        
        public SwitchStatementSyntax GenSwitchStatement(ExpressionSyntax expression, List<SwitchSectionSyntax> sections)
            => SwitchStatement(expression, sections.ToSyntaxList());

        public ThrowStatementSyntax GenThrowStatement(ExpressionSyntax expression)
            => ThrowStatement(expression);

        public TryStatementSyntax GenTryStatement(List<CatchClauseSyntax> catches)
            => TryStatement(catches.ToSyntaxList());

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

        public CatchFilterClauseSyntax GenCatchFilterClause(ExpressionSyntax filterExpression)
            => CatchFilterClause(filterExpression);

        public UnsafeStatementSyntax GenUnsafeStatement(BlockSyntax block)
            => UnsafeStatement(block);

        public UsingStatementSyntax GenUsingStatement(StatementSyntax statement)
            => UsingStatement(statement);

        public UsingStatementSyntax GenUsingStatement(VariableDeclarationSyntax declaration, ExpressionSyntax expression, StatementSyntax statement)
            => UsingStatement(declaration, expression, statement);

        public WhileStatementSyntax GenWhileStatement(ExpressionSyntax expression, StatementSyntax statement)
            => WhileStatement(expression, statement);

        public YieldStatementSyntax GenYieldBreakStatement(ExpressionSyntax expression)
            => YieldStatement(SyntaxKind.YieldBreakStatement, expression);

        public YieldStatementSyntax GenYieldReturnStatement(ExpressionSyntax expression)
            => YieldStatement(SyntaxKind.YieldReturnStatement, expression);

        public LocalFunctionStatementSyntax GenLocalFunctionStatement(TypeSyntax returnType, string identifier)
            => LocalFunctionStatement(returnType, identifier);

        public ElseClauseSyntax GenElseClause(StatementSyntax statement)
            => ElseClause(statement);

        public SwitchSectionSyntax GenSwitchSection()
            => SwitchSection();

        public SwitchSectionSyntax GenSwitchSection(List<SwitchLabelSyntax> labels, List<StatementSyntax> statements)
            => SwitchSection(labels.ToSyntaxList(), statements.ToSyntaxList());

        public SwitchLabelSyntax GenSwitchLabel(ExpressionSyntax expression)
            => CaseSwitchLabel(expression);

        public SwitchLabelSyntax GenSwitchLabel(PatternSyntax pattern)
            => CasePatternSwitchLabel(pattern, Token(SyntaxKind.ColonToken));

        public SwitchLabelSyntax GenSwitchLabel()
            => DefaultSwitchLabel();

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
