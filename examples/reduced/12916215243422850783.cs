// Generated by Fuzzlyn v1.2 on 2021-07-03 16:53:44
// Seed: 12916215243422850783
// Reduced from 20.9 KiB to 0.6 KiB in 00:00:14
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public short F0;
    public ulong F1;
    public uint F2;
    public uint F4;
    public S0(ulong f1): this()
    {
        F1 = f1;
    }
}

struct S1
{
    public byte F1;
    public ushort F2;
    public S0 F8;
    public S1(byte f1, S0 f8): this()
    {
        F1 = f1;
        F8 = f8;
    }
}

struct S2
{
    public S1 F0;
    public S2(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S2 vr0 = new S2(new S1(1, new S0(0)));
        System.Console.WriteLine(vr0.F0.F1);
        System.Console.WriteLine(vr0.F0.F2);
    }
}
