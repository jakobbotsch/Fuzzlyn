// Generated by Fuzzlyn v1.1 on 2018-09-26 14:28:23
// Seed: 3890976659656784834
// Reduced from 106.7 KiB to 0.6 KiB in 00:01:33
// Debug: Outputs -2147483647
// Release: Outputs 2147483393
public class Program
{
    static bool s_42;
    public static void Main()
    {
        uint vr9 = default(uint);
        var vr6 = (sbyte)vr9;
        bool vr7 = M2(vr6);
    }

    static bool M2(sbyte arg0)
    {
        arg0 = (sbyte)(0 - M4(0));
        var vr2 = 2147483647 * arg0;
        System.Console.WriteLine(vr2);
        try
        {
            var vr4 = new short[]{0};
        }
        finally
        {
            arg0 >>= 0;
        }

        return s_42;
    }

    static long M4(uint arg0)
    {
        System.Console.WriteLine(arg0);
        return 1;
    }
}
