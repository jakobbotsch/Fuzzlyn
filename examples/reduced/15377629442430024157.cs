// Generated by Fuzzlyn v1.1 on 2018-11-27 11:48:59
// Seed: 15377629442430024157
// Reduced from 113.8 KiB to 0.9 KiB in 00:01:07
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public long F0;
}

struct S2
{
    public byte F0;
    public ulong F2;
    public S0 F3;
    public S0 F8;
    public S2(ulong f2): this()
    {
        F2 = f2;
    }
}

struct S3
{
    public int F0;
    public bool F2;
    public uint F3;
    public int F5;
    public sbyte F6;
}

public class Program
{
    static S3 s_2;
    static uint[][] s_3 = new uint[][]{new uint[]{0}};
    public static void Main()
    {
        var vr6 = new S3();
        var vr7 = new S2(1);
        var vr8 = M3(vr6, M4(M1(vr7)));
    }

    static uint M1(S2 arg0)
    {
        arg0.F3.F0 = arg0.F8.F0;
        return s_3[0][0];
    }

    static byte M3(S3 arg0, S3 arg1)
    {
        var vr0 = arg0.F3;
        System.Console.WriteLine(vr0);
        return 1;
    }

    static ref S3 M4(uint arg0)
    {
        System.Console.WriteLine(arg0);
        return ref s_2;
    }
}