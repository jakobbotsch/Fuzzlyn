// Generated by Fuzzlyn v1.2 on 2021-08-16 12:32:02
// Run on .NET 6.0.0-dev on X64 Windows
// Seed: 17883188181817993970
// Reduced from 70.4 KiB to 0.5 KiB in 00:00:41
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public ushort F0;
}

struct S1
{
    public sbyte F0;
    public sbyte F1;
    public long F2;
    public S0 F5;
    public S1(sbyte f1): this()
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
        S2 vr0 = new S2(new S1(1));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F1);
    }
}