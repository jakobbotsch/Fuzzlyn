// Generated by Fuzzlyn v1.1 on 2021-06-12 00:42:21
// Seed: 11991615925769861084
// Reduced from 166.4 KiB to 0.8 KiB in 00:10:53
// Crashes the runtime
public class Program
{
    static ushort[][] s_23 = new ushort[][]{new ushort[]{0}};
    static short s_32;
    static short s_33;
    static int s_45;
    public static void Main()
    {
        ushort[] vr4 = s_23[0];
        ushort vr6 = M45();
    }

    static ushort M45()
    {
        short var0;
        try
        {
            var0 = s_32;
        }
        finally
        {
            var0 = s_33;
            int var1 = s_45;
            ulong vr8 = default(ulong);
            var0 = (short)((sbyte)vr8 - var0);
            try
            {
                M46();
            }
            finally
            {
                M46();
            }

            System.Console.WriteLine(var1);
        }

        return 0;
    }

    static ulong M46()
    {
        return default(ulong);
    }
}