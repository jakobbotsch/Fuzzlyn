// Generated by Fuzzlyn v1.1 on 2018-09-14 07:32:00
// Seed: 2703779015775885041
// Reduced from 221.1 KiB to 1.0 KiB in 00:05:03
// Debug: Outputs 0
// Release: Outputs -14796
public class Program
{
    static short s_4;
    static sbyte[] s_29 = new sbyte[]{0};
    static short[, ][] s_61 = new short[, ][]{{new short[]{0}}};
    static long[][][] s_84 = new long[][][]{new long[][]{new long[]{0}}};
    static byte[, ][] s_96 = new byte[, ][]{{new byte[]{0}}};
    public static void Main()
    {
        M24(-1);
    }

    static void M24(short arg0)
    {
        sbyte var2 = default(sbyte);
        try
        {
            byte var1 = s_96[0, 0][0];
        }
        finally
        {
            var vr2 = new sbyte[, ]{{1}};
            var vr1 = M58();
            var vr3 = s_29[0];
            bool vr0 = M86();
            s_61[0, 0] = new short[]{1};
            long vr11 = s_84[0][0][0];
            arg0 = s_4;
            arg0 %= -25370;
            System.Console.WriteLine(var2);
        }

        System.Console.WriteLine(arg0);
    }

    static long M58()
    {
        return default(long);
    }

    static bool M86()
    {
        return default(bool);
    }
}
