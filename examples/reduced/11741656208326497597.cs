// Generated by Fuzzlyn v1.2 on 2021-08-05 20:19:28
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 11741656208326497597
// Reduced from 6.7 KiB to 0.4 KiB in 00:00:20
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static bool s_1;
    public static void Main()
    {
        int vr0 = 1 + (904581292 % (M2(0, 0) | 1));
        System.Console.WriteLine(vr0);
    }

    static short M2(uint arg0, short arg1)
    {
        arg0 = arg0;
        for (int var0 = 0; var0 < 1; var0++)
        {
            s_1 = true;
        }

        return arg1;
    }
}
