// Generated by Fuzzlyn v1.2 on 2021-07-05 09:29:17
// Seed: 12411176586024457617
// Reduced from 13.7 KiB to 0.6 KiB in 00:00:14
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public byte F0;
    public S0(byte f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public S0 F3;
    public S1(S0 f3): this()
    {
        F3 = f3;
    }
}

struct S2
{
    public uint F0;
    public S1 F2;
    public S2(S1 f2): this()
    {
        F2 = f2;
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
        S3 vr0 = new S3(new S2(new S1(new S0(1))));
        vr0.F0.F2.F3.F0 = vr0.F0.F2.F3.F0;
        System.Console.WriteLine(vr0.F0.F0);
    }
}
