// Generated by Fuzzlyn v1.2 on 2021-08-11 03:50:25
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12040784894779594246
// Reduced from 29.4 KiB to 0.2 KiB in 00:00:43
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static int s_22;
    public static void Main()
    {
        uint vr1 = (uint)s_22;
        sbyte vr0 = (sbyte)((17037509133481796096UL % (vr1 | 1)) + 1);
        System.Console.WriteLine(vr0);
    }
}