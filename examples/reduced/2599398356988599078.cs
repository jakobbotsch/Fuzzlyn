// Generated by Fuzzlyn v1.1 on 2018-10-02 03:25:00
// Seed: 2599398356988599078
// Reduced from 79.2 KiB to 0.6 KiB in 00:01:08
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
struct S0
{
    public long F1;
}

class C0
{
    public S0 F0;
    public int F2;
    public bool F3;
}

public class Program
{
    static C0 s_1 = new C0();
    static bool[] s_25;
    public static void Main()
    {
        M5(ref s_25);
    }

    static void M5(ref bool[] arg0)
    {
        var vr0 = new C0[]{new C0()};
        var vr1 = s_1.F0.F1 - (0 & s_1.F2);
        var vr2 = s_1.F2;
        M6(vr0, vr1);
    }

    static bool M6(C0[] arg1, long arg2)
    {
        ulong[] var0 = new ulong[]{0, 0, 1, 0, 0, 0, 0};
        return arg1[0].F3;
    }
}
