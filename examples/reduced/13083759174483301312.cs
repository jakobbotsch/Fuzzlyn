// Generated by Fuzzlyn v1.2 on 2021-08-10 22:57:52
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 13083759174483301312
// Reduced from 21.8 KiB to 0.2 KiB in 00:00:57
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static uint s_1 = 1;
    public static void Main()
    {
        ulong vr2 = (ulong)(1 + (5989415210585353277L % s_1));
        System.Console.WriteLine(vr2);
    }
}
