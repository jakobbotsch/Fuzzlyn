// Generated by Fuzzlyn v1.2 on 2021-08-08 06:26:58
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 18212205704989601417
// Reduced from 17.8 KiB to 0.3 KiB in 00:00:40
// Debug: Outputs 3647
// Release: Outputs 3646
class C1
{
    public int F3;
}

public class Program
{
    static C1[, ] s_1 = new C1[, ]{{new C1()}};
    public static void Main()
    {
        short vr0 = (short)(1 + (2941578339827650110UL % (ulong)(-1 ^ s_1[0, 0].F3)));
        System.Console.WriteLine(vr0);
    }
}