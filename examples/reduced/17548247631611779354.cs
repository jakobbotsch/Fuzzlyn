// Generated by Fuzzlyn v1.2 on 2021-08-13 03:51:37
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 17548247631611779354
// Reduced from 14.7 KiB to 0.2 KiB in 00:00:33
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static ulong s_1;
    public static void Main()
    {
        short vr0 = 1;
        s_1 = (ulong)(1 + (4354022161641057936L % vr0));
        System.Console.WriteLine(s_1);
    }
}