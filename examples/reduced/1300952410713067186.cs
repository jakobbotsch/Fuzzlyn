// Generated by Fuzzlyn v1.2 on 2021-07-05 20:56:52
// Seed: 1300952410713067186
// Reduced from 88.2 KiB to 0.6 KiB in 00:01:04
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public int F0;
    public short F3;
    public bool F4;
    public int F5;
    public S0(int f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public bool F2;
    public int F4;
    public S0 F5;
    public S1(int f4, S0 f5): this()
    {
        F4 = f4;
        F5 = f5;
    }
}

struct S3
{
    public S1 F0;
    public S3(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S3 vr3 = new S3(new S1(1, new S0(0)));
        System.Console.WriteLine(vr3.F0.F2);
        System.Console.WriteLine(vr3.F0.F4);
    }
}
