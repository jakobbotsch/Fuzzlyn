// Generated by Fuzzlyn v1.2 on 2021-07-06 00:55:45
// Seed: 2980424644467410330
// Reduced from 207.2 KiB to 0.6 KiB in 00:01:46
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public int F1;
    public short F2;
    public sbyte F3;
    public uint F5;
    public S0(short f2): this()
    {
        F2 = f2;
    }
}

struct S2
{
    public uint F1;
    public int F3;
    public S0 F7;
    public S2(uint f1, S0 f7): this()
    {
        F1 = f1;
        F7 = f7;
    }
}

struct S3
{
    public S2 F0;
    public S3(S2 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S3 vr3 = new S3(new S2(1, new S0(0)));
        System.Console.WriteLine(vr3.F0.F1);
        System.Console.WriteLine(vr3.F0.F3);
    }
}