// Generated by Fuzzlyn v1.2 on 2021-08-07 06:38:10
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 10652801136737959435
// Reduced from 125.5 KiB to 0.2 KiB in 00:06:03
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static uint[] s_1 = new uint[]{0};
    public static void Main()
    {
        ulong vr2 = (11396960594095825787UL % (s_1[0] + 1)) + 1;
        System.Console.WriteLine(vr2);
    }
}