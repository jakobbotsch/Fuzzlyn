// Generated by Fuzzlyn v1.1 on 2018-12-06 22:01:53
// Seed: 4822555403361454597
// Reduced from 314.2 KiB to 0.8 KiB in 00:09:38
// Debug: Outputs 1
// Release: Outputs 0
class C0
{
}

struct S0
{
    public ushort F0;
    public C0 F5;
    public C0 F6;
    public S0(ushort f0): this()
    {
        F0 = f0;
    }
}

struct S2
{
    public sbyte F0;
    public ulong F1;
    public bool F3;
}

class C1
{
    public S2 F6;
    public S0 F7;
    public C1(S0 f7)
    {
        F7 = f7;
    }
}

public class Program
{
    static S2 s_7;
    static C1 s_13 = new C1(new S0(1));
    static C1[] s_25 = new C1[]{new C1(new S0(1))};
    public static void Main()
    {
        var vr9 = s_13.F7;
        var vr10 = new S0(0);
        var vr11 = s_25[0].F6;
        M53(vr9, M53(vr10, vr11));
    }

    static ref S2 M53(S0 arg0, S2 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_7;
    }
}
