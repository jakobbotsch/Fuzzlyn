// Generated by Fuzzlyn v1.2 on 2021-08-08 03:25:25
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 152370716858301318
// Reduced from 10.1 KiB to 0.2 KiB in 00:00:32
// Debug: Outputs -1
// Release: Outputs 0
class C0
{
    public short F3;
}

public class Program
{
    public static void Main()
    {
        C0[] vr1 = new C0[]{new C0()};
        var vr2 = (1092474643344563732L % (vr1[0].F3 | 1)) - 1;
        System.Console.WriteLine(vr2);
    }
}
