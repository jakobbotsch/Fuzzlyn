// Generated by Fuzzlyn v1.1 on 2018-10-03 08:36:55
// Seed: 12313578835390713670
// Reduced from 201.3 KiB to 0.7 KiB in 00:03:34
// Debug: Prints 0 line(s)
// Release: Prints 1 line(s)
public class Program
{
    static uint[] s_6;
    static uint[][] s_39 = new uint[][]{new uint[]{0}};
    static bool s_58;
    public static void Main()
    {
        M63();
    }

    static void M63()
    {
        try
        {
            if (s_58)
            {
                try
                {
                    return;
                }
                finally
                {
                    long vr6 = default(long);
                    System.Console.WriteLine(vr6);
                }
            }
            else
            {
                return;
            }
        }
        finally
        {
            s_39[0] = s_6;
        }
    }
}
