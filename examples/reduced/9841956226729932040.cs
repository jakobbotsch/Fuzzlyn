// Generated by Fuzzlyn v1.2 on 2021-08-14 10:42:54
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9841956226729932040
// Reduced from 157.7 KiB to 0.2 KiB in 00:05:42
// Debug: Outputs 255
// Release: Outputs 0
public class Program
{
    static uint s_11;
    public static void Main()
    {
        byte vr6 = (byte)((790779561 % (s_11 | 1)) - 1);
        System.Console.WriteLine(vr6);
    }
}
