// Generated by Fuzzlyn v1.2 on 2021-08-05 18:15:37
// Run on Arm64 Linux, .NET 6.0.0-preview.6.21352.12
// Seed: 153570980909371310
// Reduced from 48.4 KiB to 0.3 KiB in 00:01:22
// Debug: Outputs 0
// Release: Outputs -1
public class Program
{
    static short s_1;
    static ulong[] s_3 = new ulong[]{0};
    static byte s_4;
    public static void Main()
    {
        s_4 = 1;
        if (s_3[0] == ((uint)(345731951 % s_4) + 1))
        {
            short vr6 = s_1--;
        }

        System.Console.WriteLine(s_1);
    }
}