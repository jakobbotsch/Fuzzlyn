// Generated by Fuzzlyn v1.1 on 2018-11-12 14:12:03
// Seed: 7358117823964788628
// Reduced from 39.3 KiB to 0.9 KiB in 00:01:01
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public uint F0;
    public int F1;
    public sbyte F2;
    public int F3;
    public ushort F5;
    public S0(uint f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public byte F5;
    public S0 F9;
}

struct S2
{
    public bool F0;
    public S1 F2;
}

struct S3
{
    public S2 F4;
}

class C2
{
    public S3 F0;
}

public class Program
{
    static C2 s_2 = new C2();
    public static void Main()
    {
        var vr6 = new S0(1);
        S2 vr11 = default(S2);
        var vr8 = new S3[]{new S3()};
        bool vr9 = M2(vr6, M3(vr11, vr8)) >= s_2.F0.F4.F2.F5;
    }

    static byte M2(S0 arg0, S3 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        ulong vr12 = default(ulong);
        return (byte)vr12;
    }

    static ref S3 M3(S2 arg0, S3[] arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref arg1[0];
    }
}
