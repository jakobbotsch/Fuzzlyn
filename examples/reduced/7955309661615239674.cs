// Generated by Fuzzlyn v1.1 on 2018-11-15 11:36:40
// Seed: 7955309661615239674
// Reduced from 35.3 KiB to 0.8 KiB in 00:01:24
// Debug: Outputs 0
// Release: Outputs -15
public class Program
{
    static short[] s_1 = new short[]{0};
    static byte[] s_8;
    public static void Main()
    {
        var vr11 = M2(-1);
    }

    static byte[] M2(sbyte arg0)
    {
        if (s_1[0] == 0)
        {
            arg0 = (sbyte)M5();
            arg0 /= 28;
        }
        else
        {
            try
            {
                s_1 = new short[]{0};
            }
            finally
            {
                ushort[][] var1 = new ushort[][]{new ushort[]{0}, new ushort[]{1}, new ushort[]{0, 0, 0, 0, 0, 1, 0, 0}, new ushort[]{1}, new ushort[]{1}, new ushort[]{0}};
            }
        }

        System.Console.WriteLine(arg0);
        return s_8;
    }

    static long M5()
    {
        byte[] var0 = new byte[]{135};
        return 0;
    }
}
