using Fuzzlyn.Methods;
using Fuzzlyn.ProbabilityDistributions;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace Fuzzlyn;

internal class FuzzlynOptions
{
    public ulong? Seed { get; set; }
    public int NumPrograms { get; set; } = 1;
    public TimeSpan? TimeToRun { get; set; }
    public string OutputEventsTo { get; set; }
    public string Host { get; set; }
    public int Parallelism { get; set; } = 1;
    public bool Output { get; set; } = false;
    public bool EnableChecksumming { get; set; } = true;
    public bool Reduce { get; set; } = false;
    public bool Stats { get; set; } = false;
    // Probability that we pick a class when generating a new type. Otherwise we make a struct.
    public double MakeClassProb { get; set; } = 0.5;
    // Probability that a field of an aggregate type gets a primitive type vs a aggregate type.
    public double PrimitiveFieldProb { get; set; } = 0.8;
    public int ProgramMinStatements { get; set; } = 50;
    /// <summary>
    /// Max total number of calls for a single function in the program.
    /// Without such a cap we can easily generate programs that will run until the end of the
    /// universe, for example M0 calling M1 twice, M1 calling M2 twice, M2 calling M3 twice,
    /// etc. until some big N.
    /// </summary>
    public long SingleFunctionMaxTotalCalls { get; set; } = 100_000_000;
    public double AssignToNewVarProb { get; set; } = 0.4;
    public double NewVarIsLocalProb { get; set; } = 0.8;
    public double CompoundAssignmentProb { get; set; } = 0.1;
    public double IncDecAssignmentStatementProb { get; set; } = 0.1;

    public ProbabilityDistribution NumAggregateTypesDist { get; set; } = new GeometricDistribution(0.4);
    public ProbabilityDistribution NumStructFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
    public ProbabilityDistribution NumClassFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
    public ProbabilityDistribution NumInterfaceTypesDist { get; set; } = new GeometricDistribution(0.5);
    // For each interface this distribution indicates how many more types to implement it on.
    // Should always give at least 1.
    public ProbabilityDistribution NumImplementorsDist { get; set; } = new GeometricDistribution(0.5, 1);
    public ProbabilityDistribution MakeArrayCountDist { get; set; } = new GeometricDistribution(0.8);
    public ProbabilityDistribution ArrayRankDist { get; set; } = new GeometricDistribution(0.9, 1);
    public ProbabilityDistribution BlockStatementCountDist { get; set; } = new GeometricDistribution(0.30, 1);
    public ProbabilityDistribution StatementTypeDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)StatementKind.Assignment] = 0.57,
            [(int)StatementKind.If] = 0.14,
            [(int)StatementKind.Block] = 0.1,
            [(int)StatementKind.Call] = 0.1,
            [(int)StatementKind.TryFinally] = 0.05,
            [(int)StatementKind.Loop] = 0.03,
            [(int)StatementKind.Return] = 0.01,
        });
    public ProbabilityDistribution ExpressionTypeDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)ExpressionKind.MemberAccess] = 0.46,
            [(int)ExpressionKind.Literal] = 0.21,
            [(int)ExpressionKind.Call] = 0.11,
            [(int)ExpressionKind.NewObject] = 0.02,
            [(int)ExpressionKind.Unary] = 0.05,
            [(int)ExpressionKind.Binary] = 0.05,
            [(int)ExpressionKind.Increment] = 0.05,
            [(int)ExpressionKind.Decrement] = 0.05,
        });

    // Controls how the level of nesting rejects generating recursive statements (blocks, ifs, calls).
    // https://www.desmos.com/calculator/lxqwr6if6d
    public HillEquationParameters StatementRejection { get; set; } = new HillEquationParameters(2.4, 2);
    public HillEquationParameters ExpressionRejection { get; set; } = new HillEquationParameters(3.5, 3);
    // Probability that we will attempt to generate a new method when trying to generate a call.
    public double GenNewFunctionProb { get; set; } = 0.07;
    // Controls the probability of generating a new function as a function of the total number of current
    // functions we have.
    // https://www.desmos.com/calculator/iaqs1fgrok
    public HillEquationParameters FuncGenRejection { get; set; } = new HillEquationParameters(2, 100);

    public double PickLiteralFromTableProb { get; set; } = 0.5;
    public ProbabilityDistribution LiteralDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [int.MinValue] = 0.1,
            [int.MinValue + 1] = 0.1,
            [-10] = 0.04,
            [-2] = 0.04,
            [-1] = 0.04,
            [0] = 0.2,
            [1] = 0.2,
            [2] = 0.04,
            [10] = 0.04,
            [int.MaxValue - 1] = 0.1,
            [int.MaxValue] = 0.1,
        });

    public ProbabilityDistribution UnaryIntegralDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)SyntaxKind.UnaryPlusExpression] = 0.02,
            [(int)SyntaxKind.UnaryMinusExpression] = 0.49,
            [(int)SyntaxKind.BitwiseNotExpression] = 0.49,
        });

    public ProbabilityDistribution BinaryIntegralDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)SyntaxKind.AddExpression] = 0.1,
            [(int)SyntaxKind.SubtractExpression] = 0.1,
            [(int)SyntaxKind.DivideExpression] = 0.1,
            [(int)SyntaxKind.MultiplyExpression] = 0.1,
            [(int)SyntaxKind.ModuloExpression] = 0.1,
            [(int)SyntaxKind.LeftShiftExpression] = 0.1,
            [(int)SyntaxKind.RightShiftExpression] = 0.1,
            [(int)SyntaxKind.BitwiseAndExpression] = 0.1,
            [(int)SyntaxKind.BitwiseOrExpression] = 0.1,
            [(int)SyntaxKind.ExclusiveOrExpression] = 0.1,
        });

    public ProbabilityDistribution BinaryBoolDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)SyntaxKind.EqualsExpression] = 0.15,
            [(int)SyntaxKind.NotEqualsExpression] = 0.15,
            [(int)SyntaxKind.LogicalAndExpression] = 0.12,
            [(int)SyntaxKind.LogicalOrExpression] = 0.12,
            [(int)SyntaxKind.LessThanOrEqualExpression] = 0.10,
            [(int)SyntaxKind.LessThanExpression] = 0.10,
            [(int)SyntaxKind.GreaterThanOrEqualExpression] = 0.10,
            [(int)SyntaxKind.GreaterThanExpression] = 0.10,
            [(int)SyntaxKind.ExclusiveOrExpression] = 0.02,
            [(int)SyntaxKind.BitwiseAndExpression] = 0.02,
            [(int)SyntaxKind.BitwiseOrExpression] = 0.02,
        });

    public int MaxArrayTotalSize { get; set; } = 300;
    public int MaxArrayLengthPerDimension { get; set; } = 10;
    public ProbabilityDistribution FuncKindDist { get; set; }
        = new TableDistribution(new Dictionary<int, double>
        {
            [(int)FuncKind.StaticMethod] = 0.47,
            [(int)FuncKind.InstanceMethod] = 0.47,
            [(int)FuncKind.InterfaceMethod] = 0.06,
        });
    public ProbabilityDistribution MethodParameterCountDist { get; set; } = new GeometricDistribution(0.4);
    public double ParameterIsByRefProb { get; set; } = 0.25;
    public double LocalIsByRefProb { get; set; } = 0.10;
    public double ReturnTypeIsByRefProb { get; set; } = 0.20;
    /// <summary>Probably to generate a ref-reassign when assigning a by-ref value.</summary>
    public double AssignGenRefReassignProb { get; set; } = 0.25;

    /// <summary>Probability that we select a local over a static when generating a member access expression.</summary>
    public double MemberAccessSelectLocalProb { get; set; } = 0.8;
    /// <summary>Table to use when selecting an existing lvalue.</summary>
    public ProbabilityDistribution ExistingLValueDist { get; set; }
     = new TableDistribution(new Dictionary<int, double>
     {
         [(int)LValueKind.Local] = 0.7,
         [(int)LValueKind.Static] = 0.2,
         [(int)LValueKind.RefReturningCall] = 0.1,
     });
}

internal enum LValueKind
{
    Local,
    Static,
    RefReturningCall,
}

internal class HillEquationParameters
{
    public HillEquationParameters(double n, double h)
    {
        N = n;
        H = h;
    }

    public double N { get; }
    public double H { get; }

    // This equation is taken from https://math.stackexchange.com/a/2324218.
    // Properties:
    // For level = 0, the probability is 0%
    // For level = h, the probability is 50%
    // The probability is always increasing
    // The probability is never 1.
    public double Compute(double level)
    {
        double @base = H / level;
        return 1 / (Math.Pow(@base, N) + 1);
    }

    // 'samples' the hill equation to determine whether some functionality
    // should be probabilistically rejected at this level.
    // This function returns true if rand < 1 / ((h/level)^n + 1).
    public bool Reject(double level, Rng rng)
    {
        return rng.NextDouble() < Compute(level);
    }
}
