// Generated by Fuzzlyn v1.2 on 2021-07-05 02:04:46
// Seed: 14904070790975154429
// Reduced from 72.3 KiB to 0.5 KiB in 00:00:19
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public ushort F0;
}

struct S1
{
    public ushort F0;
    public sbyte F3;
    public ushort F4;
    public S0 F5;
    public S1(ushort f0): this()
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
        vr0.F0.F4 = vr0.F0.F5.F0;
        System.Console.WriteLine(vr0.F0.F0);
    }
}
