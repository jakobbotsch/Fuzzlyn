// Generated by Fuzzlyn v1.2 on 2021-08-09 01:51:14
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 8187014995035231727
// Reduced from 11.2 KiB to 0.2 KiB in 00:00:33
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static byte[] s_3 = new byte[]{1};
    public static void Main()
    {
        short vr3 = (sbyte)(1 + (9374648426012372084UL % s_3[0]));
        System.Console.WriteLine(vr3);
    }
}
