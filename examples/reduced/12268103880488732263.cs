// Generated by Fuzzlyn v1.2 on 2021-08-16 08:43:34
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12268103880488732263
// Reduced from 34.5 KiB to 0.4 KiB in 00:00:50
// Debug: Outputs -1
// Release: Outputs 0
public class Program
{
    static ushort s_5;
    public static void Main()
    {
        short vr0 = (short)((-6797289403781994535L % (M2() | 1)) - 1);
        System.Console.WriteLine(vr0);
    }

    static ushort M2()
    {
        M3();
        ushort var2 = s_5;
        return M5();
    }

    static void M3()
    {
    }

    static byte M5()
    {
        return default(byte);
    }
}
