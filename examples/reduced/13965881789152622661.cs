// Generated by Fuzzlyn v1.1 on 2018-11-10 06:26:12
// Seed: 13965881789152622661
// Reduced from 7.6 KiB to 0.3 KiB in 00:00:08
// Debug: Outputs 0
// Release: Outputs -44
public class Program
{
    static sbyte s_1;
    public static void Main()
    {
        M2(-1);
    }

    static void M2(sbyte arg1)
    {
        try
        {
            s_1 = arg1;
        }
        finally
        {
            arg1++;
            arg1 %= (-118 | 1);
        }

        System.Console.WriteLine(arg1);
    }
}