// Generated by Fuzzlyn v1.1 on 2018-11-27 09:33:18
// Seed: 14726925039796902946
// Reduced from 143.5 KiB to 1.3 KiB in 00:05:10
// Debug: Outputs 0
// Release: Outputs 2
struct S0
{
    public long F3;
}

struct S1
{
    public sbyte F0;
    public S1(sbyte f0): this()
    {
        F0 = f0;
    }
}

class C0
{
}

class C1
{
    public S0 F0;
}

class C2
{
    public short F5;
}

struct S2
{
    public long F7;
    public C1 F8;
    public S2(C1 f8): this()
    {
        F8 = f8;
    }
}

struct S3
{
    public sbyte F0;
}

struct S4
{
    public long F1;
}

public class Program
{
    static C2 s_4 = new C2();
    static S4 s_6;
    static S4 s_14;
    static S2 s_23 = new S2(new C1());
    static C0[] s_29 = new C0[]{new C0()};
    static S2 s_33;
    public static void Main()
    {
        s_4.F5 ^= -2417;
        short vr3 = s_4.F5;
        var vr2 = (sbyte)vr3;
        M5(vr2);
    }

    static void M5(sbyte arg0)
    {
        arg0 += arg0;
        arg0 /= -128;
        try
        {
            long var9 = s_23.F8.F0.F3;
        }
        finally
        {
            var vr1 = s_29[0];
            S3 var10 = M11(s_6, new S1(0));
            s_14.F1 = 0;
            long var11 = s_33.F7;
            System.Console.WriteLine(var10.F0);
            System.Console.WriteLine(var11);
        }

        System.Console.WriteLine(arg0);
    }

    static S3 M11(S4 arg2, S1 arg3)
    {
        return default(S3);
    }
}