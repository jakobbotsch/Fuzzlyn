// Generated by Fuzzlyn v1.3 on 2021-08-22 12:31:23
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16843214936605262996
// Reduced from 150.6 KiB to 0.5 KiB in 00:03:34
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public uint F4;
}

struct S1
{
    public S0 F2;
}

struct S2
{
    public bool F0;
    public int F1;
    public S1 F3;
    public S2(int f1, S1 f3): this()
    {
        F1 = f1;
        F3 = f3;
    }
}

struct S4
{
    public S2 F0;
    public S4(S2 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S4 vr0 = new S4(new S2(1, new S1()));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F1);
    }
}
