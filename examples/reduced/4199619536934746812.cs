// Generated by Fuzzlyn v1.1 on 2018-09-07 15:02:33
// Seed: 4199619536934746812
// Reduced from 179.2 KiB to 0.8 KiB in 00:03:04
// Debug: Outputs -1
// Release: Outputs 5
public class Program
{
    static long[, ] s_1;
    static int[,, ] s_3 = new int[,, ]{{{-1}}};
    static byte s_5;
    static byte[][] s_11 = new byte[][]{new byte[]{1}};
    public static void Main()
    {
        M14(0);
    }

    static void M14(sbyte arg0)
    {
        try
        {
            s_1 = new long[, ]{{0}};
        }
        finally
        {
            byte vr14 = s_11[0][0];
            int vr16 = s_3[0, 0, 0];
            arg0 = (sbyte)vr16;
            var vr2 = s_3[0, 0, 0];
            arg0 %= 10;
            var vr3 = s_11[0][0]++;
            M19(ref s_11[0][0], vr3);
            var vr4 = s_5--;
            M19(ref s_11[0][0], vr4);
        }

        System.Console.WriteLine(arg0);
    }

    static int M19(ref byte arg0, byte arg2)
    {
        return s_3[0, 0, 0];
    }
}
