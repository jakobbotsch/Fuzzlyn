// Generated by Fuzzlyn v1.2 on 2021-08-06 22:22:11
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 2162510488036367961
// Reduced from 35.5 KiB to 0.3 KiB in 00:01:15
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public int F1;
    public S0(int f1): this()
    {
        F1 = f1;
    }
}

public class Program
{
    static S0 s_7 = new S0(1);
    public static void Main()
    {
        s_7.F1 &= 0;
        System.Console.WriteLine(s_7.F1);
    }
}
