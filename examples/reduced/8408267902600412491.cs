// Generated by Fuzzlyn v1.2 on 2021-07-05 02:42:01
// Seed: 8408267902600412491
// Reduced from 27.5 KiB to 0.6 KiB in 00:00:19
// Debug: Outputs False
// Release: Outputs True
struct S0
{
    public bool F0;
    public int F2;
    public uint F4;
    public short F5;
    public S0(short f5): this()
    {
        F5 = f5;
    }
}

struct S2
{
    public byte F1;
    public S0 F2;
    public S2(byte f1, S0 f2): this()
    {
        F1 = f1;
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
        S3 vr0 = new S3(new S2(1, new S0(-1)));
        System.Console.WriteLine(vr0.F0.F1);
        System.Console.WriteLine(vr0.F0.F2.F0);
    }
}
