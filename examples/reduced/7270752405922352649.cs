// Generated by Fuzzlyn v1.1 on 2018-09-06 11:34:20
// Seed: 7270752405922352649
// Reduced from 392.0 KiB to 0.7 KiB in 00:05:37
// Debug: Prints 0 line(s)
// Release: Prints 1 line(s)
struct S0
{
    public bool F1;
}

public class Program
{
    static S0[][] s_65 = new S0[][]{new S0[]{new S0()}};
    public static void Main()
    {
        S0[] vr1 = s_65[0];
        M90(ref vr1[0]);
    }

    static void M90(ref S0 arg2)
    {
        ulong var0 = default(ulong);
        try
        {
            if (arg2.F1)
            {
                try
                {
                    return;
                }
                finally
                {
                    System.Console.WriteLine(var0);
                }
            }
            else
            {
                return;
            }
        }
        finally
        {
            bool vr0 = arg2.F1;
        }
    }
}