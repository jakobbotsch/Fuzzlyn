// Generated by Fuzzlyn v1.1 on 2018-09-27 17:19:57
// Seed: 1774530793774778055
// Reduced from 333.4 KiB to 0.9 KiB in 00:07:50
// Debug: Outputs 0
// Release: Outputs 1
class C1
{
    public sbyte F0;
    public C1(sbyte f0)
    {
        F0 = f0;
    }
}

struct S0
{
    public C1 F0;
    public long F1;
    public sbyte F3;
    public S0(C1 f0): this()
    {
        F0 = f0;
    }
}

class C2
{
    public S0 F2;
    public C2(S0 f2)
    {
        F2 = f2;
    }
}

public class Program
{
    static C2 s_17 = new C2(new S0(new C1(1)));
    static S0[, ] s_19 = new S0[, ]{{new S0(new C1(0))}};
    public static void Main()
    {
        var vr11 = new S0(new C1(0));
        var vr12 = s_17.F2;
        M33(vr11, M43(0, vr12));
    }

    static void M33(S0 arg0, S0 arg1)
    {
        arg1.F0.F0 = arg0.F0.F0;
        System.Console.WriteLine(arg1.F0.F0);
    }

    static ref S0 M43(byte arg0, S0 arg2)
    {
        System.Console.WriteLine(arg0);
        return ref s_19[0, 0];
    }
}
