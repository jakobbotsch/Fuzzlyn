// Generated by Fuzzlyn v1.1 on 2018-12-01 06:37:24
// Seed: 13309504745193715438
// Reduced from 356.4 KiB to 0.8 KiB in 00:17:18
// Debug: Outputs -1
// Release: Outputs -95566
public class Program
{
    static short s_21;
    static byte s_26;
    static ulong[] s_31 = new ulong[]{0};
    static ulong s_38;
    public static void Main()
    {
        var vr27 = M46(1, s_21);
    }

    static ref byte M46(ushort arg0, short arg2)
    {
        int var1 = default(int);
        var vr20 = arg2--;
        var vr23 = arg2 % -31855;
        System.Console.WriteLine(vr23);
        try
        {
            System.Console.WriteLine(var1);
        }
        finally
        {
            try
            {
                s_31[0] = s_38;
            }
            finally
            {
                System.Console.WriteLine(arg0);
            }
        }

        System.Console.WriteLine(arg2);
        return ref s_26;
    }
}
