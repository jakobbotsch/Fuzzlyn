// Generated by Fuzzlyn v1.2 on 2021-07-03 18:00:58
// Seed: 3217011808778606476
// Reduced from 346.7 KiB to 0.6 KiB in 00:02:40
// Debug: Outputs False
// Release: Outputs True
struct S0
{
    public uint F0;
    public sbyte F1;
    public uint F6;
    public ushort F7;
    public S0(sbyte f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public ushort F0;
    public bool F1;
    public S0 F8;
    public S1(ushort f0, S0 f8): this()
    {
        F0 = f0;
        F8 = f8;
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
        S3 vr2 = new S3(new S1(1, new S0(0)));
        System.Console.WriteLine(vr2.F0.F0);
        System.Console.WriteLine(vr2.F0.F1);
    }
}
