// Generated by Fuzzlyn v1.2 on 2021-08-06 09:45:32
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16850457371111640322
// Reduced from 288.1 KiB to 0.6 KiB in 00:10:33
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public uint F2;
    public S0(uint f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public S0 F6;
    public S1(S0 f6): this()
    {
        F6 = f6;
    }
}

class C0
{
    public S1 F0;
    public C0(S1 f0)
    {
        F0 = f0;
    }
}

class C1
{
    public C0 F0;
    public C1(C0 f0)
    {
        F0 = f0;
    }
}

public class Program
{
    static C1[][] s_32 = new C1[][]{new C1[]{new C1(new C0(new S1(new S0(1))))}};
    public static void Main()
    {
        s_32[0][0].F0.F0.F6.F2 &= 0;
        S1 vr1 = s_32[0][0].F0.F0;
        System.Console.WriteLine(vr1.F6.F2);
    }
}