// Generated by Fuzzlyn v1.2 on 2021-08-06 01:26:17
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 10156874734631613038
// Reduced from 113.4 KiB to 0.3 KiB in 00:04:06
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    static uint[] s_44 = new uint[]{1};
    static uint s_52;
    public static void Main()
    {
        for (int vr7 = 0; vr7 < 2; vr7++)
        {
            s_52 = s_44[0];
            s_44[0] &= 0;
        }

        System.Console.WriteLine(s_52);
    }
}