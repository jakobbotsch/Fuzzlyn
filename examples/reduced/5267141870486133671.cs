// Generated by Fuzzlyn v1.3 on 2021-08-22 22:46:17
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 5267141870486133671
// Reduced from 50.0 KiB to 0.8 KiB in 00:01:13
// Debug: Outputs True
// Release: Outputs False
struct S0
{
    public bool F1;
    public int F2;
    public uint F3;
    public bool F6;
    public S0(int f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public bool F1;
    public sbyte F5;
    public S0 F6;
    public S1(bool f1, S0 f6): this()
    {
        F1 = f1;
        F6 = f6;
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
    internal static S1 s_2;
    public static void Main()
    {
        var vr2 = new sbyte[]{0};
        bool vr3 = M3(vr2) != s_2.F5;
    }

    internal static sbyte M3(sbyte[] arg0)
    {
        S2 var0 = new S2(new S1(true, new S0(0)));
        var0.F0.F6.F2 = var0.F0.F6.F2;
        System.Console.WriteLine(var0.F0.F1);
        return arg0[0];
    }
}