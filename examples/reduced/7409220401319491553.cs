// Generated by Fuzzlyn v1.2 on 2021-08-17 03:51:16
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 7409220401319491553
// Reduced from 101.9 KiB to 0.3 KiB in 00:04:49
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static int s_1;
    public static void Main()
    {
        long vr6 = 1 + (long)(2035208206786669042UL % (1 + M16()));
        System.Console.WriteLine(vr6);
    }

    static ulong M16()
    {
        int var0 = s_1;
        for (int var1 = 0; var1 < 0; var1++)
        {
        }

        return 0;
    }
}
