// Generated by Fuzzlyn v1.3 on 2021-08-23 04:13:31
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12478420912793094560
// Reduced from 544.2 KiB to 0.6 KiB in 00:12:02
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public byte F0;
    public uint F2;
    public int F6;
    public short F7;
    public S0(uint f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public S0 F0;
    public short F3;
    public S1(S0 f0): this()
    {
        F0 = f0;
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
        S2 vr0 = new S2(new S1(new S0(1)));
        System.Console.WriteLine(vr0.F0.F0.F0);
        System.Console.WriteLine(vr0.F0.F0.F2);
    }
}