// Generated by Fuzzlyn v1.2 on 2021-07-04 07:33:12
// Seed: 8987705271059538724
// Reduced from 126.7 KiB to 0.6 KiB in 00:00:33
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public int F0;
    public ulong F1;
    public int F3;
    public ushort F7;
    public S0(ulong f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public sbyte F0;
    public S0 F2;
    public S1(sbyte f0, S0 f2): this()
    {
        F0 = f0;
        F2 = f2;
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
        System.Console.WriteLine(vr0.F0.F2.F0);
    }
}
