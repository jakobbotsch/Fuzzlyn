// Generated by Fuzzlyn v1.2 on 2021-07-03 17:24:26
// Seed: 7942836627444969388
// Reduced from 647.5 KiB to 0.6 KiB in 00:08:19
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public ushort F0;
    public bool F1;
    public int F3;
    public bool F5;
    public S0(int f3): this()
    {
        F3 = f3;
    }
}

struct S2
{
    public uint F0;
    public int F5;
    public S0 F6;
    public S2(uint f0, S0 f6): this()
    {
        F0 = f0;
        F6 = f6;
    }
}

struct S4
{
    public S2 F0;
    public S4(S2 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static ulong s_74;
    public static void Main()
    {
        S4 vr0 = new S4(new S2(1, new S0(1)));
        s_74 >>= vr0.F0.F5;
        System.Console.WriteLine(vr0.F0.F0);
    }
}
