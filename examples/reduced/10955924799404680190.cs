// Generated by Fuzzlyn v1.2 on 2021-08-10 10:59:59
// Run on .NET 6.0.0-dev on X64 Windows
// Seed: 10955924799404680190
// Reduced from 107.1 KiB to 0.6 KiB in 00:00:38
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public sbyte F0;
    public sbyte F1;
    public int F7;
    public byte F9;
    public S0(sbyte f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public S0 F0;
    public S0 F3;
    public S1(S0 f0): this()
    {
        F0 = f0;
    }
}

struct S2
{
    public S1 F0;
    public S2(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S2 vr7 = new S2(new S1(new S0(1)));
        System.Console.WriteLine(vr7.F0.F0.F0);
        System.Console.WriteLine(vr7.F0.F0.F1);
    }
}
