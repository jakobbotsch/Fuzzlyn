// Generated by Fuzzlyn v1.1 on 2018-12-16 17:29:01
// Seed: 14841158161095174101
// Reduced from 284.3 KiB to 0.7 KiB in 00:07:20
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public bool F0;
    public S0(bool f0): this()
    {
    }
}

struct S1
{
    public short F0;
    public long F1;
    public S0 F4;
    public S1(short f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S1 s_6 = new S1(1);
    public static void Main()
    {
        S1 vr7 = default(S1);
        var vr6 = new S1[]{new S1(0)};
        M31(vr7, M19(s_6, vr6));
    }

    static ref S1 M19(S1 arg3, S1[] arg4)
    {
        if (arg4[0].F4.F0)
        {
            S0 var0 = new S0(false);
            return ref arg4[0];
        }

        return ref arg4[0];
    }

    static void M31(S1 arg0, S1 arg1)
    {
        System.Console.WriteLine(arg0.F0);
    }
}