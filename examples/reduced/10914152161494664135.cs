// Generated by Fuzzlyn v1.2 on 2021-07-04 05:34:59
// Seed: 10914152161494664135
// Reduced from 196.5 KiB to 0.6 KiB in 00:01:08
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public long F0;
    public long F1;
    public ulong F4;
    public uint F7;
    public S0(long f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public S0 F0;
    public short F1;
    public S1(S0 f0): this()
    {
        F0 = f0;
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
        S3 vr0 = new S3(new S1(new S0(1)));
        System.Console.WriteLine(vr0.F0.F0.F0);
        System.Console.WriteLine(vr0.F0.F0.F1);
    }
}
