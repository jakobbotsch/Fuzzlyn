// Generated by Fuzzlyn v1.1 on 2018-09-30 16:18:13
// Seed: 5532365103077209480
// Reduced from 320.2 KiB to 0.4 KiB in 00:08:22
// Debug: Outputs 0
// Release: Outputs 439
public class Program
{
    static short[] s_5;
    static short s_8;
    public static void Main()
    {
        s_5 = new short[]{-1};
        M17(s_8);
    }

    static void M17(short arg0)
    {
        arg0 = s_5[0];
        var vr3 = arg0 / 149;
        System.Console.WriteLine(vr3);
        try
        {
            s_5 = new short[]{-1};
        }
        finally
        {
            arg0 /= 1;
        }
    }
}