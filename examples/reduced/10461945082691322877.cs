// Generated by Fuzzlyn v1.3 on 2021-08-21 21:58:38
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 10461945082691322877
// Reduced from 198.8 KiB to 0.6 KiB in 00:03:46
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public bool F3;
    public ulong F5;
    public uint F7;
    public bool F8;
    public S0(bool f3): this()
    {
        F3 = f3;
    }
}

struct S1
{
    public byte F0;
    public ushort F1;
    public S0 F5;
    public S1(ushort f1, S0 f5): this()
    {
        F1 = f1;
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
        S3 vr0 = new S3(new S1(1, new S0(true)));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F1);
    }
}