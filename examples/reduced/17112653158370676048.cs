// Generated by Fuzzlyn v1.2 on 2021-08-07 22:25:07
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 17112653158370676048
// Reduced from 240.2 KiB to 0.3 KiB in 00:07:20
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public byte F4;
    public S0(byte f4): this()
    {
        F4 = f4;
    }
}

public class Program
{
    public static void Main()
    {
        var vr1 = new S0[]{new S0(1)};
        ulong vr3 = 1 + (16098004120865148102UL % vr1[0].F4);
        System.Console.WriteLine(vr3);
    }
}