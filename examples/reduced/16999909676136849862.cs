// Generated by Fuzzlyn v1.2 on 2021-08-06 13:31:37
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16999909676136849862
// Reduced from 630.8 KiB to 0.4 KiB in 00:31:41
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
    public uint F0;
    public C0(uint f0)
    {
        F0 = f0;
    }
}

struct S0
{
    public C0 F2;
    public S0(C0 f2): this()
    {
        F2 = f2;
    }
}

public class Program
{
    public static void Main()
    {
        var vr10 = new S0[]{new S0(new C0(1))};
        vr10[0].F2.F0 *= 0;
        var vr12 = vr10[0].F2.F0;
        System.Console.WriteLine(vr12);
    }
}
