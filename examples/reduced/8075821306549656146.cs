// Generated by Fuzzlyn v1.2 on 2021-08-09 05:33:24
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 8075821306549656146
// Reduced from 174.5 KiB to 0.2 KiB in 00:04:06
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static long s_1 = 1;
    public static void Main()
    {
        byte vr0 = (byte)(1 + (3475766517U % s_1));
        System.Console.WriteLine(vr0);
    }
}