// Generated by Fuzzlyn v1.2 on 2021-08-12 13:53:08
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9497546706185186449
// Reduced from 21.0 KiB to 0.2 KiB in 00:00:42
// Debug: Outputs 4294967295
// Release: Outputs 0
public class Program
{
    static sbyte s_2;
    public static void Main()
    {
        uint vr1 = (uint)(1186519573 % (s_2 | 1)) - 1;
        System.Console.WriteLine(vr1);
    }
}
