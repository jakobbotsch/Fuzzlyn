// Generated by Fuzzlyn v1.2 on 2021-08-14 07:31:54
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 10170563209862976001
// Reduced from 111.5 KiB to 0.3 KiB in 00:03:57
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static long s_1;
    public static void Main()
    {
        s_1 = 1 + (-1676898205 % (M6(true) | 1));
        System.Console.WriteLine(s_1);
    }

    static byte M6(bool arg0)
    {
        System.Console.WriteLine(arg0);
        return 0;
    }
}