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

    static void M2()
    {
        ref sbyte[, ] vr47 = ref s_16;
        ref byte vr48 = ref s_25;
        ref short vr49 = ref s_19;
        try
        {
            sbyte[] vr50 = new sbyte[]{0};
        }
        finally
        {
            sbyte vr51 = vr47[0, 0];
            sbyte vr52 = vr47[0, 0];
            s_rt.Checksum("c_28", vr52);
            var vr53 = new int[]{450864205};
            ref uint vr58 = ref s_1[0];
            ref int[] vr59 = ref vr53;
            int vr60 = vr59[0];
            var vr54 = s_4[0];
            ref uint vr61 = ref s_1[0];
            ref int[] vr62 = ref vr54;
            int vr63 = vr62[0];
            s_1[0] = (uint)vr63;
        }

        if (!s_7)
        {
            var vr55 = new int[]{0};
            ref uint vr64 = ref s_1[0];
            ref int[] vr65 = ref vr55;
            int vr66 = vr65[0];
            vr49 = (short)(51431005U | vr66);
            short vr56 = vr49;
            if (vr47[0, 0] > vr56)
            {
                uint vr57 = s_1[0];
                s_rt.Checksum("c_40", vr57);
            }
        }

        s_rt.Checksum("c_264", vr49);
    }

    static void M11()
    {
        ref sbyte[, ] vr67 = ref s_16;
        ref byte vr68 = ref s_25;
        ref short vr69 = ref s_19;
        try
        {
            sbyte[] vr70 = new sbyte[]{0};
        }
        finally
        {
            sbyte vr71 = vr67[0, 0];
            sbyte vr72 = vr67[0, 0];
            s_rt.Checksum("c_28", vr72);
            var vr73 = new int[]{450864205};
            ref uint vr78 = ref s_1[0];
            ref int[] vr79 = ref vr73;
            int vr80 = vr79[0];
            var vr74 = s_4[0];
            ref uint vr81 = ref s_1[0];
            ref int[] vr82 = ref vr74;
            int vr83 = vr82[0];
            s_1[0] = (uint)vr83;
        }

        if (!s_7)
        {
            var vr75 = new int[]{0};
            ref uint vr84 = ref s_1[0];
            ref int[] vr85 = ref vr75;
            int vr86 = vr85[0];
            vr69 = (short)(51431005U | vr86);
            short vr76 = vr69;
            if (vr67[0, 0] > vr76)
            {
                uint vr77 = s_1[0];
                s_rt.Checksum("c_40", vr77);
            }
        }

        s_rt.Checksum("c_264", vr69);
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
            ref uint vr87 = ref s_1[0];
            ref int[] vr88 = ref vr2;
            int vr89 = vr88[0];
            var vr3 = s_4[0];
            s_1[0] = (uint)M13(ref s_1[0], vr3);
        }

        if (!s_7)
        {
            var vr1 = new int[]{0};
            ref uint vr90 = ref s_1[0];
            ref int[] vr91 = ref vr1;
            int vr92 = vr91[0];
            arg2 = (short)(51431005U | vr92);
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