// Generated by Fuzzlyn v1.1 on 2018-12-12 21:19:39
// Seed: 11102086366240306332
// Reduced from 248.4 KiB to 0.5 KiB in 00:05:08
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public short F0;
    public ulong F2;
    public long F3;
    public S0(short f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S0 s_28;
    static S0[][] s_29 = new S0[][]{new S0[]{new S0(1)}};
    static S0 s_45;
    public static void Main()
    {
        S0 vr8 = s_29[0][0];
        M105(vr8, M105(s_28, s_45));
    }

    static ref S0 M105(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_29[0][0];
    }
}