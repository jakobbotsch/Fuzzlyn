// Generated by Fuzzlyn v1.1 on 2018-11-20 10:23:54
// Seed: 4539368566890289288
// Reduced from 369.8 KiB to 1.4 KiB in 00:05:21
// Debug: Outputs 0
// Release: Outputs 1596438138
struct S0
{
    public short F1;
    public short F3;
    public int F6;
}

struct S1
{
    public S0 F0;
    public bool F1;
    public S0 F2;
    public S0 F3;
    public sbyte F5;
}

struct S2
{
    public uint F0;
    public int F1;
    public ushort F2;
    public int F4;
    public sbyte F5;
    public byte F6;
}

public class Program
{
    static S1 s_1;
    static S2 s_3;
    static S1 s_23;
    static S0 s_27;
    static S1 s_54;
    static S1 s_57;
    static S2[][] s_64 = new S2[][]{new S2[]{new S2()}};
    static S2 s_73;
    static S2[] s_90 = new S2[]{new S2()};
    public static void Main()
    {
        var vr6 = s_54.F2.F3;
        var vr7 = new uint[]{0};
        sbyte vr11 = s_1.F5;
        var vr9 = M13(vr6, ref s_27.F6, 0, vr7, vr11, ref s_90[0].F6, ref s_23.F3.F1);
        var vr13 = new uint[]{0};
        var vr14 = vr9.F5;
        M8(s_73, M13(0, ref vr9.F4, 0, vr13, vr14, ref s_64[0][0].F6, ref s_57.F0.F3));
    }

    static void M8(S2 arg0, S2 arg1)
    {
        System.Console.WriteLine(arg0.F0);
    }

    static ref S2 M13(short arg0, ref int arg1, ushort arg2, uint[] arg3, sbyte arg4, ref byte arg5, ref short arg6)
    {
        if (s_1.F1)
        {
            arg1 = arg1;
        }
        else
        {
            return ref s_3;
        }

        return ref s_3;
    }
}