// Generated by Fuzzlyn v1.3 on 2021-08-20 21:15:28
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12734459574578953193
// Reduced from 190.9 KiB to 0.6 KiB in 00:05:55
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public short F0;
    public int F1;
    public uint F2;
    public ushort F3;
    public S0(uint f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public sbyte F0;
    public short F1;
    public S0 F8;
    public S1(short f1, S0 f8): this()
    {
        F1 = f1;
        F8 = f8;
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
        S2 vr2 = new S2(new S1(1, new S0(0)));
        System.Console.WriteLine(vr2.F0.F0);
        System.Console.WriteLine(vr2.F0.F1);
    }
}