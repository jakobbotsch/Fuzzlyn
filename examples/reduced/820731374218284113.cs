// Generated by Fuzzlyn v1.2 on 2021-07-05 00:08:19
// Seed: 820731374218284113
// Reduced from 69.1 KiB to 0.6 KiB in 00:00:27
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public bool F0;
    public short F1;
    public short F2;
    public ulong F3;
    public S0(short f1): this()
    {
        F1 = f1;
    }
}

struct S2
{
    public S0 F0;
    public uint F2;
    public S2(S0 f0): this()
    {
        F0 = f0;
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
        S3 vr0 = new S3(new S2(new S0(1)));
        System.Console.WriteLine(vr0.F0.F0.F0);
        System.Console.WriteLine(vr0.F0.F0.F1);
    }
}
