// Generated by Fuzzlyn v1.2 on 2021-08-05 18:23:27
// Run on Arm64 Linux, .NET 6.0.0-preview.6.21352.12
// Seed: 11570437162079104654
// Reduced from 64.6 KiB to 0.3 KiB in 00:01:50
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    public static void Main()
    {
        var vr5 = new uint[]{1};
        for (int vr8 = 0; vr8 < 2; vr8++)
        {
            vr5[0] = 0;
            vr5[0] = vr5[0];
        }

        System.Console.WriteLine(vr5[0]);
    }
}
