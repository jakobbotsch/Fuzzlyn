// Generated by Fuzzlyn v1.2 on 2021-08-10 18:24:49
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 14752420195929182690
// Reduced from 37.3 KiB to 0.2 KiB in 00:01:06
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static long[] s_2 = new long[]{0};
    public static void Main()
    {
        int vr0 = 1;
        vr0 += (int)(3336116088U % ((uint)s_2[0] | 1));
        System.Console.WriteLine(vr0);
    }
}