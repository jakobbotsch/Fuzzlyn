// Generated by Fuzzlyn v1.1 on 2018-11-22 03:59:42
// Seed: 8855759856116747168
// Reduced from 24.4 KiB to 0.6 KiB in 00:00:46
// Debug: Outputs 0
// Release: Outputs 31
public class Program
{
    static ushort s_3;
    public static void Main()
    {
        sbyte vr12 = default(sbyte);
        var vr10 = M10(vr12);
    }

    static uint M10(sbyte arg0)
    {
        try
        {
            s_3 = 0;
        }
        finally
        {
            ++arg0;
        }

        arg0 = (sbyte)(50161 % (M13(254) | 1));
        sbyte var6 = arg0;
        var6 %= (sbyte)(var6 | 1);
        System.Console.WriteLine(var6);
        return 0;
    }

    static uint M13(uint arg0)
    {
        bool vr0 = s_3 < s_3++;
        return arg0;
    }
}
