// Generated by Fuzzlyn v1.2 on 2021-08-17 00:46:25
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9059673482739458962
// Reduced from 6.2 KiB to 0.2 KiB in 00:00:30
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static int s_1;
    public static void Main()
    {
        short vr1 = (short)((4317948018809132043UL % (uint)(s_1 | 1)) + 1);
        System.Console.WriteLine(vr1);
    }
}