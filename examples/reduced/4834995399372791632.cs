// Generated by Fuzzlyn v1.2 on 2021-08-15 06:13:00
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 4834995399372791632
// Reduced from 11.9 KiB to 0.2 KiB in 00:00:43
// Debug: Outputs 4294967295
// Release: Outputs 0
public class Program
{
    static uint s_1;
    public static void Main()
    {
        var vr4 = (3332876178U % (1 + s_1)) - 1;
        System.Console.WriteLine(vr4);
    }
}
