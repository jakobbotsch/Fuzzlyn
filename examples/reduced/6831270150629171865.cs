// Generated by Fuzzlyn v1.2 on 2021-08-12 07:25:29
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 6831270150629171865
// Reduced from 15.5 KiB to 0.6 KiB in 00:00:43
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public long F0;
    public sbyte F1;
    public bool F2;
    public int F3;
    public S0(sbyte f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public S0 F1;
    public uint F2;
    public S1(S0 f1): this()
    {
        F1 = f1;
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
        S2 vr0 = new S2(new S1(new S0(1)));
        System.Console.WriteLine(vr0.F0.F1.F1);
        System.Console.WriteLine(vr0.F0.F1.F3);
    }
}