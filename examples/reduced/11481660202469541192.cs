// Generated by Fuzzlyn v1.1 on 2018-11-27 04:00:05
// Seed: 11481660202469541192
// Reduced from 209.1 KiB to 0.5 KiB in 00:05:15
// Debug: Outputs 0
// Release: Outputs -2
public class Program
{
    static int s_1;
    static sbyte s_2;
    public static void Main()
    {
        s_2 = -1;
        M70(0);
    }

    static void M70(sbyte arg1)
    {
        try
        {
            M71();
        }
        finally
        {
            arg1 %= (sbyte)(s_1 | 1);
            arg1 = s_2;
            arg1 /= -128;
            System.Console.WriteLine(arg1);
        }
    }

    static bool M71()
    {
        return default(bool);
    }
}
