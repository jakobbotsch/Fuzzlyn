// Generated by Fuzzlyn v1.1 on 2018-09-07 20:08:22
// Seed: 11837965281667710192
// Reduced from 216.1 KiB to 1.1 KiB in 00:06:02
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static ushort[] s_4 = new ushort[]{1};
    static short[] s_16 = new short[]{0};
    static long s_62 = 1;
    public static void Main()
    {
        s_4[0] &= (ushort)M80(0);
        System.Console.WriteLine(s_4[0]);
    }

    static short M80(short arg0)
    {
        if (s_62 == s_16[0])
        {
            try
            {
                System.Console.WriteLine(435908236U);
            }
            finally
            {
                long[] var9 = new long[]{1};
                short var10 = 0;
                bool var11 = false;
                long var12 = var9[0]++;
                ulong[] var13 = new ulong[]{0, 1, 0, 0, 0, 0, 0, 0};
                System.Console.WriteLine(var10);
                System.Console.WriteLine(var11);
            }
        }
        else
        {
            arg0 = (short)M81(0);
            arg0 %= (-32768 | 1);
        }

        return arg0;
    }

    static uint M81(int arg0)
    {
        System.Console.WriteLine(arg0);
        return 1897710951U;
    }
}
