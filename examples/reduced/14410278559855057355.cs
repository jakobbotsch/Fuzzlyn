// Generated by Fuzzlyn v1.2 on 2021-08-11 21:04:53
// Run on .NET 6.0.0-dev on Arm Linux
// Seed: 14410278559855057355
// Reduced from 63.8 KiB to 0.6 KiB in 00:02:05
// Debug: Outputs True
// Release: Outputs False
struct S0
{
    public bool F0;
    public short F1;
    public bool F2;
    public ulong F3;
    public S0(bool f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public S0 F0;
    public S0 F1;
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
        S2 vr3 = new S2(new S1(new S0(true)));
        System.Console.WriteLine(vr3.F0.F0.F1);
        System.Console.WriteLine(vr3.F0.F0.F2);
    }
}
