// Generated by Fuzzlyn v1.2 on 2021-08-09 01:11:06
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16843019657415533810
// Reduced from 131.3 KiB to 0.3 KiB in 00:04:29
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static uint[, ] s_43 = new uint[, ]{{1}};
    public static void Main()
    {
        var vr3 = new short[]{0};
        vr3[0] = (short)((13850135127670718030UL % s_43[0, 0]) + 1);
        System.Console.WriteLine(vr3[0]);
    }
}
