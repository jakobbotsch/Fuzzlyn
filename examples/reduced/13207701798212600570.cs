// Generated by Fuzzlyn v1.1 on 2018-10-04 14:19:54
// Seed: 13207701798212600570
// Reduced from 84.3 KiB to 0.6 KiB in 00:01:07
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
    public short F0;
    public C0(short f0)
    {
        F0 = f0;
    }
}

struct S0
{
    public C0 F0;
    public byte F4;
    public int F5;
    public int F6;
    public S0(C0 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S0[] s_2 = new S0[]{new S0(new C0(0))};
    public static void Main()
    {
        var vr8 = new S0(new C0(0));
        S0 vr13 = new S0(new C0(1));
        var vr10 = new S0(new C0(0));
        M10(vr8, M10(vr13, vr10));
    }

    static ref S0 M10(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0.F0);
        return ref s_2[0];
    }
}
