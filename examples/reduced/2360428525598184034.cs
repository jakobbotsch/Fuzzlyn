// Generated by Fuzzlyn v1.2 on 2021-07-03 18:55:56
// Seed: 2360428525598184034
// Reduced from 50.8 KiB to 0.6 KiB in 00:00:25
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public uint F2;
    public short F4;
    public bool F5;
    public short F6;
    public S0(short f6): this()
    {
        F6 = f6;
    }
}

struct S2
{
    public bool F0;
    public sbyte F2;
    public S0 F4;
    public S2(bool f0, S0 f4): this()
    {
        F0 = f0;
        F4 = f4;
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
    public static void Main()
    {
        S4 vr0 = new S4(new S2(true, new S0(0)));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F2);
    }
}
