// Generated by Fuzzlyn v1.2 on 2021-08-07 07:34:22
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9051797481244003720
// Reduced from 236.6 KiB to 0.2 KiB in 00:07:43
// Debug: Outputs -1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        M3(0);
    }

    static void M3(ulong arg1)
    {
        long var0 = (sbyte)((14152181696602362111UL % (arg1 | 1)) - 1);
        System.Console.WriteLine(var0);
    }
}