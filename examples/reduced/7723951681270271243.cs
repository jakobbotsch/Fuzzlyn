// Generated by Fuzzlyn v1.2 on 2021-07-05 08:39:49
// Seed: 7723951681270271243
// Reduced from 111.3 KiB to 0.6 KiB in 00:00:38
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
}

struct S0
{
    public byte F1;
    public byte F2;
    public byte F3;
    public C0 F4;
    public S0(byte f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
}

struct S2
{
    public S0 F0;
    public S1 F1;
    public S2(S0 f0): this()
    {
        F0 = f0;
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
        S4 vr0 = new S4(new S2(new S0(1)));
        System.Console.WriteLine(vr0.F0.F0.F1);
        System.Console.WriteLine(vr0.F0.F0.F3);
    }
}