// Generated by Fuzzlyn v1.2 on 2021-08-16 12:31:58
// Run on .NET 6.0.0-dev on X64 Windows
// Seed: 1713921816167933000
// Reduced from 15.3 KiB to 0.5 KiB in 00:00:18
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public bool F0;
}

struct S2
{
    public int F1;
    public sbyte F2;
    public sbyte F3;
    public S0 F4;
    public S2(sbyte f2): this()
    {
        F2 = f2;
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
        S4 vr0 = new S4(new S2(1));
        System.Console.WriteLine(vr0.F0.F2);
        System.Console.WriteLine(vr0.F0.F3);
    }
}
