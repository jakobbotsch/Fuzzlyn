// Generated by Fuzzlyn v1.2 on 2021-07-03 17:14:43
// Seed: 9968647983116627421
// Reduced from 122.1 KiB to 0.6 KiB in 00:00:50
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public ushort F3;
    public sbyte F6;
}

struct S1
{
    public S0 F0;
    public sbyte F5;
    public S1(sbyte f5): this()
    {
        F5 = f5;
    }
}

struct S2
{
    public short F2;
    public S1 F3;
    public S2(short f2, S1 f3): this()
    {
        F2 = f2;
        F3 = f3;
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
        S3 vr0 = new S3(new S2(1, new S1(0)));
        System.Console.WriteLine(vr0.F0.F2);
        System.Console.WriteLine(vr0.F0.F3.F0.F3);
    }
}