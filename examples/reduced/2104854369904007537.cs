// Generated by Fuzzlyn v1.1 on 2018-12-14 08:26:14
// Seed: 2104854369904007537
// Reduced from 325.1 KiB to 1.2 KiB in 00:07:21
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public bool F0;
    public bool F2;
    public short F3;
    public ulong F4;
    public byte F6;
    public S0(ulong f4, byte f6, uint f7): this()
    {
    }
}

struct S1
{
    public long F4;
    public S0 F5;
    public S0 F6;
    public S1(long f4): this()
    {
        F4 = f4;
    }
}

struct S2
{
    public bool F0;
    public long F1;
    public int F2;
}

public class Program
{
    static S2 s_6;
    static S0 s_13;
    static S0[] s_22 = new S0[]{new S0(0, 0, 0)};
    public static void Main()
    {
        var vr9 = new S1(1);
        var vr10 = new S1(0);
        var vr11 = s_13.F6;
        var vr12 = s_22[0];
        sbyte vr13 = (sbyte)M62(vr9, vr10, M64(vr11, vr12));
        System.Console.WriteLine(vr13);
    }

    static long M62(S1 arg0, S1 arg1, S2 arg2)
    {
        arg1.F6.F3 = 10;
        if (arg2.F0)
        {
            arg1.F5.F0 = arg1.F6.F2;
            arg0.F6 = new S0(0, 155, 2508992936U);
            bool vr0 = arg1.F6.F0;
            bool vr4 = vr0;
        }

        return arg0.F4;
    }

    static ref S2 M64(byte arg0, S0 arg6)
    {
        System.Console.WriteLine(arg0);
        return ref s_6;
    }
}
