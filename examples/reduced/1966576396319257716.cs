// Generated by Fuzzlyn v1.2 on 2021-07-21 19:16:17
// Seed: 1966576396319257716
// Reduced from 75.0 KiB to 0.6 KiB in 00:00:37
// Debug: Outputs False
// Release: Outputs True
struct S0
{
    public bool F0;
    public uint F1;
    public int F3;
    public uint F4;
    public S0(uint f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public ushort F0;
    public S0 F1;
    public S1(ushort f0, S0 f1): this()
    {
        F0 = f0;
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
        S2 vr0 = new S2(new S1(1, new S0(0)));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F1.F0);
    }
}