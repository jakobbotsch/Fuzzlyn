// Generated by Fuzzlyn v1.1 on 2018-09-12 03:21:52
// Seed: 789416209151314616
// Reduced from 269.3 KiB to 0.5 KiB in 00:05:41
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    static short s_2;
    static short s_12;
    static ulong s_22 = 1;
    public static void Main()
    {
        s_12 = -1;
        M40(s_2, 0);
        System.Console.WriteLine(s_22);
    }

    static void M40(short arg0, sbyte arg1)
    {
        try
        {
            arg0 = s_12;
            arg1 = (sbyte)(arg0 / 4498);
        }
        finally
        {
            s_22 %= ((ulong)arg1 | 1);
            arg0--;
        }
    }
}
