// Generated by Fuzzlyn v1.2 on 2021-08-13 06:44:18
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 3410623918019978721
// Reduced from 43.2 KiB to 0.3 KiB in 00:01:11
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static ushort s_2;
    static long s_6;
    public static void Main()
    {
        s_6 = (1 + (786633763U % (uint)(1 ^ M5())));
        System.Console.WriteLine(s_6);
    }

    static ushort M5()
    {
        int var0 = (int)((s_2 | 0) % 3161382460U);
        return 0;
    }
}
