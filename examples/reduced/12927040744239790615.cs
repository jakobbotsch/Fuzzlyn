// Generated by Fuzzlyn v1.2 on 2021-08-04 18:54:14
// Seed: 12927040744239790615
// Reduced from 28.7 KiB to 0.6 KiB in 00:01:07
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public ulong F0;
    public int F1;
    public byte F2;
    public uint F3;
    public S0(byte f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public S0 F0;
    public S0 F1;
    public S1(S0 f0): this()
    {
        F0 = f0;
    }
}

struct S4
{
    public S1 F0;
    public S4(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S4 vr0 = new S4(new S1(new S0(1)));
        System.Console.WriteLine(vr0.F0.F0.F2);
        System.Console.WriteLine(vr0.F0.F0.F3);
    }
}