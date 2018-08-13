public class Program
{
    static Fuzzlyn.Execution.IRuntime s_rt;
    static uint[] s_1 = new uint[]{0};
    static sbyte[] s_2;
    static ushort[] s_3 = new ushort[]{0};
    static int[][] s_4 = new int[][]{new int[]{0}};
    static long s_6;
    static bool s_7 = true;
    static short s_8 = 0;
    static sbyte[] s_9 = new sbyte[]{0};
    static ulong[] s_10;
    static long s_11 = 0;
    static int s_12 = 0;
    static uint[, ] s_13;
    static long[] s_14 = new long[]{0};
    static byte s_15 = 0;
    static sbyte[, ] s_16 = new sbyte[, ]{{0}};
    static byte s_18 = 0;
    static short s_19 = 0;
    static byte[] s_20 = new byte[]{0};
    static int s_21 = 0;
    static uint[][] s_22 = new uint[][]{new uint[]{0}};
    static byte s_25 = 1;
    static sbyte s_26;
    static long s_29;
    static uint s_33;
    static byte s_34 = 0;
    static long s_35 = 0;
    static long[][, ] s_40 = new long[][, ]{new long[, ]{{0}}};
    static ulong s_41 = 0;
    static uint s_43 = 0;
    static long s_44 = 0;
    static byte[, ][][][, ] s_45 = new byte[, ][][][, ]{{new byte[][][, ]{new byte[][, ]{new byte[, ]{{0}}}}}};
    static bool s_46 = true;
    static ulong s_47 = 0;
    static bool s_48 = false;
    static byte s_49 = 0;
    static ulong s_51 = 0;
    static byte[] s_52 = new byte[]{0};
    static short[] s_54 = new short[]{0};
    static sbyte s_56 = 0;
    static ulong s_57 = 0;
    static byte s_60;
    static ushort s_63 = 0;
    static ushort s_66 = 0;
    static uint s_68 = 0;
    static sbyte s_69 = 0;
    static ulong s_70 = 0;
    static ushort s_71 = 0;
    static long s_73 = 0;
    static short[] s_75 = new short[]{0};
    static ushort s_76 = 0;
    static long s_77 = 0;
    static sbyte[] s_78 = new sbyte[]{0};
    static long s_79 = 0;
    static byte s_80 = 0;
    static uint s_81 = 0;
    static ushort s_82 = 0;
    static long[] s_84 = new long[]{0};
    static ushort[] s_85 = new ushort[]{0};
    static ulong[][][][] s_86 = new ulong[][][][]{new ulong[][][]{new ulong[][]{new ulong[]{0}}}};
    static ushort[] s_88 = new ushort[]{0};
    static bool s_89 = true;
    static ulong s_90 = 0;
    static long s_91 = 0;
    static ushort s_92 = 0;
    static int s_93 = 0;
    static bool s_94 = false;
    static short[] s_97 = new short[]{0};
    static ulong[] s_100 = new ulong[]{0};
    static bool[, ] s_101 = new bool[, ]{{false}};
    static short s_102 = 0;
    static int[][] s_106 = new int[][]{new int[]{0}};
    static int s_108 = 0;
    static byte s_110 = 0;
    static ulong s_111 = 0;
    static short s_112 = 0;
    static short s_113 = 0;
    static short[] s_115 = new short[]{0};
    static sbyte[][] s_117 = new sbyte[][]{new sbyte[]{0}};
    static bool s_118 = true;
    static short[][][] s_121 = new short[][][]{new short[][]{new short[]{0}}};
    static byte s_123 = 0;
    static uint s_124 = 0;
    public static void Main(Fuzzlyn.Execution.IRuntime rt)
    {
        s_rt = rt;
        M0();
    }

    static void M0()
    {
        byte var0 = (byte)M1(0, 0, (byte)s_113, 0, ref s_117, ref s_9[0], ref s_26, 0, ref s_60, s_47, ref s_78, (byte)M81(), s_77);
    }

    static uint M1(byte arg0, uint arg1, byte arg2, long arg3, ref sbyte[][] arg4, ref sbyte arg5, ref sbyte arg6, sbyte arg7, ref byte arg8, ulong arg9, ref sbyte[] arg10, byte arg11, long arg12)
    {
        M2(ref s_22);
        return 0;
    }

    static void M2(ref uint[][] arg0)
    {
        M11(arg0[0]);
        return;
    }

    static ref sbyte[] M5(ushort arg0, long arg1, long[] arg2)
    {
        return ref s_2;
    }

    static int M7()
    {
        return default(int);
    }

    static uint M8()
    {
        return default(uint);
    }

    static long M9(ref bool arg0)
    {
        return default(long);
    }

    static long M10(ref ulong arg0, ref bool arg1)
    {
        return default(long);
    }

    static long M11(uint[] arg0)
    {
        M12(s_16, s_25, s_19);
        return M47(arg0[0]);
    }

    static byte M12(sbyte[, ] arg0, byte arg1, short arg2)
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
            M13(arg1, ref s_1[0], new int[]{450864205});
            s_1[0] = (uint)M13(arg1, ref s_1[0], s_4[0]);
            s_1[0] = 0;
        }

        s_7 = 0 >= arg1;
        if (!s_7)
        {
            arg2 = (short)(51431005U | M13(arg1, ref s_1[0], new int[]{0}));
            short var8 = arg2;
            if (arg0[0, 0] > var8)
            {
                uint var11 = s_1[0];
                s_rt.Checksum("c_40", var11);
            }
        }

        s_rt.Checksum("c_264", arg2);
        return arg1;
    }

    static int M13(byte arg0, ref uint arg1, int[] arg2)
    {
        return arg2[0];
    }

    static short M17(sbyte arg0, ref uint arg1, short arg2, ulong arg3, int arg4, sbyte arg5)
    {
        return default(short);
    }

    static sbyte M18()
    {
        return default(sbyte);
    }

    static byte M22()
    {
        return default(byte);
    }

    static int M24()
    {
        return default(int);
    }

    static int[][] M25()
    {
        return default(int[][]);
    }

    static bool M26()
    {
        return default(bool);
    }

    static ref short M28(long arg0)
    {
        return ref s_8;
    }

    static long M30()
    {
        return default(long);
    }

    static uint[] M31()
    {
        return default(uint[]);
    }

    static sbyte M32(byte arg0)
    {
        return default(sbyte);
    }

    static uint M36()
    {
        return default(uint);
    }

    static bool M38(sbyte arg0, ushort arg1)
    {
        return default(bool);
    }

    static uint[] M39(ulong arg0, uint arg1)
    {
        return default(uint[]);
    }

    static long[] M40(short arg0, int arg1)
    {
        return default(long[]);
    }

    static ref uint M42()
    {
        return ref s_33;
    }

    static sbyte M44()
    {
        return default(sbyte);
    }

    static ulong M45(int arg0, ushort arg1, ref bool arg2, int arg3, short[] arg4)
    {
        return default(ulong);
    }

    static short M46(ushort[] arg0, uint arg1)
    {
        return default(short);
    }

    static ref long M47(long arg0)
    {
        return ref s_29;
    }

    static ushort[, ] M50(ref int arg0)
    {
        return default(ushort[, ]);
    }

    static short M52(long arg0, ushort arg1)
    {
        return default(short);
    }

    static ushort[][] M55(uint[] arg0, sbyte arg1)
    {
        return default(ushort[][]);
    }

    static ref ulong M56()
    {
        return ref s_10[0];
    }

    static long[] M57(long arg0)
    {
        return default(long[]);
    }

    static byte M59(ushort[][] arg0, ref ulong[, ] arg1, ref ulong arg2)
    {
        return default(byte);
    }

    static int[] M60(ushort arg0)
    {
        return default(int[]);
    }

    static bool M62()
    {
        return default(bool);
    }

    static ref long M63()
    {
        return ref s_35;
    }

    static long[][] M65(sbyte arg0)
    {
        return default(long[][]);
    }

    static ulong M67(ushort[,, ] arg0)
    {
        return default(ulong);
    }

    static ushort[][] M71()
    {
        return default(ushort[][]);
    }

    static ushort M72()
    {
        return default(ushort);
    }

    static ref int M76(ref ushort arg0, sbyte[] arg1, bool arg2)
    {
        return ref s_4[0][0];
    }

    static ulong[][] M78()
    {
        return default(ulong[][]);
    }

    static int M79(sbyte arg0, sbyte[] arg1, uint arg2)
    {
        return default(int);
    }

    static bool M80(long arg0)
    {
        return default(bool);
    }

    static long M81()
    {
        return default(long);
    }

    static uint M83()
    {
        return default(uint);
    }

    static int M84()
    {
        return default(int);
    }

    static uint M87()
    {
        return default(uint);
    }

    static ulong M89(int arg0)
    {
        return default(ulong);
    }

    static bool[] M90(ref uint arg0, ref sbyte arg1, ref ushort arg2)
    {
        return default(bool[]);
    }

    static uint M92(byte arg0)
    {
        return default(uint);
    }

    static short M93()
    {
        return default(short);
    }

    static long M95()
    {
        return default(long);
    }

    static int M96(short arg0)
    {
        return default(int);
    }

    static ulong[] M98()
    {
        return default(ulong[]);
    }

    static long M99(long arg0)
    {
        return default(long);
    }

    static uint M101()
    {
        return default(uint);
    }

    static ref uint M102()
    {
        return ref s_1[0];
    }
}