public class Program
{
    static Fuzzlyn.Execution.IRuntime s_rt;
    static uint[] s_1 = new uint[]{0};
    static int[][] s_4 = new int[][]{new int[]{0}};
    static bool s_7;
    static sbyte[, ] s_16 = new sbyte[, ]{{0}};
    static short s_19;
    static byte s_25;
    public static void Main(Fuzzlyn.Execution.IRuntime rt)
    {
        s_rt = rt;
        M12(s_16, s_25, s_19);
        uint vr5 = 0;
        byte vr4 = (byte)vr5;
    }

    static void M0()
    {
        M2();
        uint vr6 = 0;
        byte var0 = (byte)vr6;
    }

    static uint M1()
    {
        M2();
        return 0;
    }

    static void M2()
    {
        M11();
    }

    static void M11()
    {
        M12(s_16, s_25, s_19);
    }

    static void M12(sbyte[, ] arg0, byte arg1, short arg2)
    {
        try
        {
            sbyte[] var2 = new sbyte[]{0};
        }
        finally
        {
            sbyte var3 = arg0[0, 0];
            sbyte var4 = arg0[0, 0];
            s_rt.Checksum("c_28", var4);
            var vr2 = new int[]{450864205};
            M13(ref s_1[0], vr2);
            var vr3 = s_4[0];
            s_1[0] = (uint)M13(ref s_1[0], vr3);
        }

        if (!s_7)
        {
            var vr1 = new int[]{0};
            arg2 = (short)(51431005U | M13(ref s_1[0], vr1));
            short var8 = arg2;
            if (arg0[0, 0] > var8)
            {
                uint var11 = s_1[0];
                s_rt.Checksum("c_40", var11);
            }
        }

        s_rt.Checksum("c_264", arg2);
    }

    static int M13(ref uint arg1, int[] arg2)
    {
        return arg2[0];
    }
}