// Generated by Fuzzlyn v1.4 on 2021-08-24 10:30:03
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 506180735280653047
// Reduced from 505.1 KiB to 0.8 KiB in 00:15:43
// Debug: Outputs False
// Release: Outputs True
public struct S0
{
    public sbyte F0;
    public int F2;
    public bool F3;
    public uint F4;
    public S0(int f2): this()
    {
        F2 = f2;
    }
}

public struct S1
{
    public uint F1;
    public bool F2;
    public S0 F6;
    public S1(uint f1, S0 f6): this()
    {
        F1 = f1;
        F6 = f6;
    }
}

public struct S2
{
    public S1 F0;
    public S2(S1 f0): this()
    {
        F0 = f0;
    }
}

public struct S3
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
        S3 vr5 = new S3(new S2(new S1(1, new S0(0))));
        System.Console.WriteLine(vr5.F0.F0.F1);
        System.Console.WriteLine(vr5.F0.F0.F2);
    }
}
