// Generated by Fuzzlyn v1.1 on 2018-08-07 16:49:46
// Seed: 14760460929992869643
// Reduced from 108.0 KiB to 0.8 KiB
// Debug: Outputs 13397152771135084132
// Release: Outputs 0
class C0
{
    public long F0;
}

struct S0
{
    public ulong F2;
    public ushort F4;
    public C0 F6;
    public S0(ulong f2, C0 f6): this()
    {
        F2 = f2;
        F6 = f6;
    }
}

public class Program
{
    static S0 s_1 = new S0(0, new C0());
    static byte s_3;
    public static void Main()
    {
        var vr15 = new S0(0, new C0());
        var vr16 = new S0(13397152771135084132UL, new C0());
        s_1.F6.F0 = M6(vr16, M4(vr15));
    }

    static ref S0 M4(S0 arg0)
    {
        bool[] var2 = new bool[]{false};
        System.Console.WriteLine(var2[0]);
        return ref s_1;
    }

    static uint M6(S0 arg0, S0 arg1)
    {
        var vr5 = arg0.F2;
        System.Console.WriteLine(vr5);
        ref byte vr18 = ref s_3;
        return vr18;
    }
}
