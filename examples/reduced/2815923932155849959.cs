// Generated by Fuzzlyn v1.1 on 2018-09-04 22:20:48
// Seed: 2815923932155849959
// Reduced from 242.8 KiB to 0.7 KiB in 00:03:43
// Debug: Prints 1 line(s)
// Release: Prints 2 line(s)
public class Program
{
    static byte s_4;
    static short[][, ][] s_14 = new short[][, ][]{new short[, ][]{{new short[]{0}}}};
    public static void Main()
    {
        M16(1);
    }

    static void M16(short arg0)
    {
        arg0 = (short)(M17(ref s_14) + 0);
        short var1 = arg0;
        if (var1 >= s_4)
        {
            System.Console.WriteLine(0U);
        }

        try
        {
            var1 = var1--;
        }
        finally
        {
            var1 = arg0;
        }
    }

    static sbyte M17(ref short[][, ][] arg0)
    {
        System.Console.WriteLine(arg0[0][0, 0][0]);
        return -1;
    }
}