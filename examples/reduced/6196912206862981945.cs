// Generated by Fuzzlyn v1.2 on 2021-08-15 18:21:19
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 6196912206862981945
// Reduced from 134.1 KiB to 0.2 KiB in 00:05:19
// Debug: Outputs -1
// Release: Outputs 0
public class Program
{
    static short s_2;
    static long s_9;
    public static void Main()
    {
        s_9 = ((1870963345 % (s_2 | 1)) - 1);
        System.Console.WriteLine(s_9);
    }
}