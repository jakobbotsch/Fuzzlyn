// Generated by Fuzzlyn v1.1 on 2018-11-20 21:43:51
// Seed: 8714309122440523742
// Reduced from 128.7 KiB to 0.6 KiB in 00:01:36
// Debug: Outputs 0
// Release: Outputs 3
public class Program
{
    static bool s_1;
    static sbyte s_2;
    static short s_36 = -1;
    public static void Main()
    {
        s_1 = true;
        M10(0);
    }

    static void M10(short arg0)
    {
        if (s_1)
        {
            arg0 = s_36;
            long var0 = arg0 / 32766;
            System.Console.WriteLine(var0);
        }
        else
        {
            try
            {
                arg0 = s_2;
            }
            finally
            {
                --arg0;
            }
        }
    }
}
