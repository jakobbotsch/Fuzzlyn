// Generated by Fuzzlyn v1.2 on 2021-07-07 00:13:13
// Seed: 3320674469085401464
// Reduced from 60.1 KiB to 0.6 KiB in 00:00:32
// Debug: Outputs 0
// Release: Outputs 1
struct S1
{
    public int F0;
    public ulong F1;
    public ushort F3;
    public ushort F4;
    public S1(ushort f3): this()
    {
        F3 = f3;
    }
}

struct S2
{
    public byte F0;
    public S1 F1;
    public S2(S1 f1): this()
    {
        F1 = f1;
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
        S3 vr0 = new S3(new S2(new S1(1)));
        System.Console.WriteLine(vr0.F0.F1.F3);
        System.Console.WriteLine(vr0.F0.F1.F4);
    }
}