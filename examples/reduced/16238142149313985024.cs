// Generated by Fuzzlyn v1.1 on 2018-11-30 09:02:31
// Seed: 16238142149313985024
// Reduced from 179.1 KiB to 0.8 KiB in 00:03:36
// Debug: Outputs 0
// Release: Outputs -7
struct S0
{
    public long F1;
    public sbyte F2;
}

public class Program
{
    static ushort[][][][] s_16 = new ushort[][][][]{new ushort[][][]{new ushort[][]{new ushort[]{0}}}};
    static S0 s_25;
    static S0 s_32;
    static short[] s_36 = new short[]{-1};
    public static void Main()
    {
        M43(0);
    }

    static void M43(short arg0)
    {
        try
        {
            s_16[0][0][0] = new ushort[]{1};
        }
        finally
        {
            arg0 = s_36[0];
            arg0 /= -13683;
            var vr1 = new byte[]{1};
            var vr2 = s_25.F2++;
            var vr3 = s_25.F1++;
            byte[] vr6 = vr1;
            sbyte vr7 = vr2;
            sbyte var0 = s_32.F2;
            System.Console.WriteLine(var0);
        }

        System.Console.WriteLine(arg0);
    }
}