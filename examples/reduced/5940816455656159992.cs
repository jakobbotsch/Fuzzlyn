// Generated by Fuzzlyn v1.1 on 2018-11-22 22:11:11
// Seed: 5940816455656159992
// Reduced from 195.9 KiB to 1.2 KiB in 00:06:19
// Debug: Outputs 0
// Release: Outputs 16
public class Program
{
    static short s_3;
    static byte[][][][][] s_4;
    static int s_35;
    static bool s_59;
    static short[][] s_93 = new short[][]{new short[]{1}};
    public static void Main()
    {
        bool vr4 = s_93[0][0]-- >= s_35;
        bool vr5 = s_93[0][0]-- >= s_35;
        var vr6 = s_93[0][0];
        M77(vr6);
    }

    static void M77(short arg0)
    {
        bool var0 = default(bool);
        uint var7 = default(uint);
        int var9 = default(int);
        try
        {
            System.Console.WriteLine(var7);
        }
        finally
        {
            if (var0)
            {
                var vr3 = new byte[, ]{{0, 0, 0, 0, 1, 0, 1, 0, 0, 0}};
                M84();
                byte[][][] var8 = s_4[0][0];
                System.Console.WriteLine(var8[0][0][0]);
                System.Console.WriteLine(var9);
            }

            var vr2 = new byte[, ]{{1}};
            var0 = s_59;
        }

        arg0 = arg0--;
        arg0 = s_3;
        arg0 /= -4991;
        System.Console.WriteLine(arg0);
    }

    static ushort M84()
    {
        return default(ushort);
    }
}