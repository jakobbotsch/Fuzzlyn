// Generated by Fuzzlyn v1.1 on 2018-11-12 02:55:07
// Seed: 1851301748548974048
// Reduced from 276.6 KiB to 0.6 KiB in 00:04:51
// Debug: Outputs 0
// Release: Outputs -4
public class Program
{
    static short[] s_78 = new short[]{0};
    static short[] s_98 = new short[]{0};
    static ulong[][] s_132;
    public static void Main()
    {
        M108(-1);
    }

    static void M108(short arg0)
    {
        try
        {
            short vr0 = s_78[0];
        }
        finally
        {
            s_132 = new ulong[][]{new ulong[]{0}, new ulong[]{1}, new ulong[]{0}, new ulong[]{0}, new ulong[]{1}, new ulong[]{0}, new ulong[]{0}};
            arg0 = s_98[0];
            arg0 %= -32767;
        }

        System.Console.WriteLine(arg0);
    }
}