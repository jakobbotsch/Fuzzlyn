// Generated by Fuzzlyn v1.2 on 2021-08-14 20:50:57
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 4416876652049450083
// Reduced from 336.8 KiB to 0.6 KiB in 00:12:27
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public long F0;
    public short F1;
    public byte F5;
    public bool F9;
    public S0(short f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public ushort F3;
    public S0 F5;
    public S1(S0 f5): this()
    {
        F5 = f5;
    }
}

struct S3
{
    public S1 F0;
    public S3(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S3 vr3 = new S3(new S1(new S0(1)));
        System.Console.WriteLine(vr3.F0.F5.F1);
        System.Console.WriteLine(vr3.F0.F5.F5);
    }
}