// Generated by Fuzzlyn v1.1 on 2018-09-05 05:46:22
// Seed: 3139391673536815992
// Reduced from 264.7 KiB to 1.3 KiB in 00:12:37
// Debug: Throws 'System.OverflowException'
// Release: Throws 'System.OverflowException'
class C0
{
    public short F2;
    public short F3;
    public C0(short f3)
    {
        F3 = f3;
    }
}

struct S0
{
    public ushort F0;
    public sbyte F1;
    public C0 F2;
    public int F4;
    public short F6;
    public S0(ushort f0): this()
    {
        F0 = f0;
    }
}

class C1
{
    public S0 F3;
}

struct S1
{
    public S0 F3;
    public S1(S0 f3): this()
    {
        F3 = f3;
    }
}

class C2
{
    public C0 F1;
    public C2(C0 f1)
    {
        F1 = f1;
    }
}

public class Program
{
    static S1 s_1 = new S1(new S0(1));
    static C2[] s_3 = new C2[]{new C2(new C0(1))};
    static S1 s_7;
    static C2 s_14;
    public static void Main()
    {
        s_14 = new C2(new C0(-6));
        s_3[0].F1.F2 = s_14.F1.F3++;
        s_3[0].F1.F2 = s_14.F1.F3++;
        s_3[0].F1.F2 = s_14.F1.F3++;
        C1[,, ] vr12 = new C1[,, ]{{{new C1()}}};
        var vr13 = s_1.F3;
        var vr14 = vr12[0, 0, 0].F3;
        var vr15 = new S1(new S0(0));
        M26(vr13, M26(vr14, vr15));
        s_3[0].F1.F2 = s_14.F1.F3++;
        s_3[0].F1.F2 = (short)(-9223372036854775808L % (s_14.F1.F3 | 1));
    }

    static ref S1 M26(S0 arg0, S1 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_7;
    }
}