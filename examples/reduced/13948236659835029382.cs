// Generated by Fuzzlyn v1.2 on 2021-08-17 21:14:17
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 13948236659835029382
// Reduced from 23.8 KiB to 0.5 KiB in 00:00:58
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
}

struct S0
{
    public C0 F0;
}

struct S1
{
    public int F0;
    public S0 F1;
    public sbyte F2;
    public short F3;
    public S1(int f0): this()
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
        S2 vr0 = new S2(new S1(1));
        System.Console.WriteLine(vr0.F0.F0);
        System.Console.WriteLine(vr0.F0.F2);
    }
}
