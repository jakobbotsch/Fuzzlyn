// Generated by Fuzzlyn v1.1 on 2018-09-05 22:30:34
// Seed: 14462288519811438559
// Reduced from 55.8 KiB to 0.8 KiB in 00:00:46
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
class C0
{
    public uint F2;
    public ulong F3;
    public C0(uint f2)
    {
        F2 = f2;
    }
}

class C1
{
    public C0 F5;
    public C1(C0 f5)
    {
        F5 = f5;
    }
}

struct S0
{
    public short F1;
}

struct S1
{
    public C1 F1;
    public C0 F2;
    public S1(C1 f1): this()
    {
        F1 = f1;
    }
}

public class Program
{
    static S0 s_3;
    public static void Main()
    {
        var vr2 = new S1[]{new S1(new C1(new C0(0)))};
        bool vr3 = 1 < M1(vr2);
    }

    static short M1(S1[] arg1)
    {
        arg1[0].F2 = new C0(0);
        if (0 >= (arg1[0].F2.F3 ^ arg1[0].F2.F2))
        {
            arg1[0].F1.F5.F2 = arg1[0].F2.F2;
        }

        return s_3.F1;
    }
}