// Generated by Fuzzlyn v1.2 on 2021-08-10 08:54:14
// Run on .NET 6.0.0-dev on X64 Windows
// Seed: 6432197072902080187
// Reduced from 22.5 KiB to 0.6 KiB in 00:00:16
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public ulong F0;
    public uint F1;
    public ulong F2;
    public short F4;
    public S0(short f4): this()
    {
        F4 = f4;
    }
}

struct S1
{
    public ulong F0;
    public S0 F1;
    public S0 F3;
    public S1(ulong f0, S0 f3): this()
    {
        F0 = f0;
        F3 = f3;
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
        S2 vr0 = new S2(new S1(1, new S0(1)));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F1.F0);
    }
}