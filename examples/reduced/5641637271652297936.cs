// Generated by Fuzzlyn v1.1 on 2018-09-30 08:19:59
// Seed: 5641637271652297936
// Reduced from 324.8 KiB to 1.0 KiB in 00:14:43
// Debug: Prints 4 line(s)
// Release: Prints 3 line(s)
struct S0
{
    public ushort F3;
    public ushort F4;
}

public class Program
{
    static short[] s_32 = new short[]{-1};
    static uint s_34;
    static S0 s_52;
    public static void Main()
    {
        var vr7 = s_32[0];
        var vr8 = new S0();
        M83(vr7, vr8);
    }

    static void M83(short arg0, S0 arg2)
    {
        try
        {
            s_52.F4 = arg2.F3;
        }
        finally
        {
            arg0 = (short)M91();
            short var3 = arg0;
            if (s_34 <= var3)
            {
                byte vr13 = default(byte);
                System.Console.WriteLine(vr13);
            }

            short[][, ] var4 = new short[][, ]{new short[, ]{{1}}};
            System.Console.WriteLine(var4[0][0, 0]);
        }

        System.Console.WriteLine(arg0);
    }

    static long M91()
    {
        byte var0 = default(byte);
        System.Console.WriteLine(var0);
        return 0;
    }
}