// Generated by Fuzzlyn v1.1 on 2018-08-28 05:40:04
// Seed: 3738462916353647676
// Reduced from 76.5 KiB to 0.7 KiB in 00:01:30
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public short F0;
    public short F1;
    public byte F4;
    public uint F5;
    public long F6;
    public S0(short f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S0 s_5;
    static uint[] s_7 = new uint[]{0};
    static long[] s_10;
    public static void Main()
    {
        var vr7 = new S0(1);
        S0 vr12 = default(S0);
        M4(s_5, M17(M4(vr7, vr12)));
    }

    static ref long[] M4(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_10;
    }

    static ref S0 M17(long[] arg0)
    {
        bool vr0 = s_7[0]-- == 0;
        return ref s_5;
    }
}