// Generated by Fuzzlyn v1.2 on 2021-08-16 13:43:42
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 7698428822723836987
// Reduced from 20.5 KiB to 0.5 KiB in 00:00:55
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        var vr2 = new byte[]{1};
        int vr3 = (byte)(1 + (8196964043604673096L % (M1(vr2) | 1)));
        System.Console.WriteLine(vr3);
    }

    static uint M1(byte[] arg3)
    {
        var vr1 = (sbyte)M3();
        M2(vr1, ref arg3[0]);
        return 0;
    }

    static void M2(sbyte arg1, ref byte arg4)
    {
    }

    static byte M3()
    {
        return default(byte);
    }
}