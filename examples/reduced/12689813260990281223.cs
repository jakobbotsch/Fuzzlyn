// Generated by Fuzzlyn v1.2 on 2021-08-08 15:08:45
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12689813260990281223
// Reduced from 52.5 KiB to 0.2 KiB in 00:00:56
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static uint s_10;
    public static void Main()
    {
        int vr0 = 1 + (-760645182 % ((short)s_10 | 1));
        System.Console.WriteLine(vr0);
    }
}