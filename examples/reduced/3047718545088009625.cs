// Generated by Fuzzlyn v1.2 on 2021-08-11 06:04:43
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 3047718545088009625
// Reduced from 296.9 KiB to 0.3 KiB in 00:14:40
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public byte F2;
    public S0(byte f2): this()
    {
        F2 = f2;
    }
}

public class Program
{
    static S0 s_56 = new S0(1);
    public static void Main()
    {
        var vr12 = 1 + (2837619572U % s_56.F2);
        System.Console.WriteLine(vr12);
    }
}