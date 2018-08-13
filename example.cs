public class Program
{
    static Fuzzlyn.Execution.IRuntime s_rt;
    static uint[] s_1 = new uint[]{4294967294U, 3434206712U, 77466795U, 2U, 4294967295U, 3865374495U, 3380499559U, 1U, 0U, 3170159126U};
    static sbyte[] s_2 = new sbyte[]{1, -10, 127, -90, 1, -38, -57, -78, 0, -127};
    static ushort[] s_3 = new ushort[]{65534, 48759, 52191, 26642, 65535, 29351};
    static int[][] s_4 = new int[][]{new int[]{0, 0, 1}, new int[]{-10}, new int[]{-2147483647}, new int[]{2018912628}, new int[]{2147483647, -1111039485}, new int[]{-108403293, -220823461}};
    static ulong s_5 = 1UL;
    static long s_6 = 9223372036854775806L;
    static bool s_7 = true;
    static short s_8 = -32767;
    static sbyte[] s_9 = new sbyte[]{-128, 125, -114, 0, 126, 0, 126, 2, 0};
    static ulong[] s_10 = new ulong[]{15691583844157379322UL};
    static long s_11 = -9223372036854775808L;
    static int s_12 = 0;
    static uint[, ] s_13 = new uint[, ]{{2940745711U, 2U, 0U, 2806973544U, 646660452U, 1U, 224910540U}, {1396646511U, 2113080014U, 2638558188U, 4294967295U, 1U, 4000773105U, 131751513U}, {1U, 10U, 428575728U, 1U, 1714057555U, 4029293351U, 1604924186U}, {2933174561U, 81542878U, 4294967295U, 1373992250U, 4011059407U, 4294967295U, 79693535U}, {3701430385U, 3502192981U, 1U, 61638541U, 4294967294U, 1221868582U, 1U}};
    static long[] s_14 = new long[]{-10L, 9223372036854775806L, 6393774577775434276L, 9223372036854775806L, 1L};
    static byte s_15 = 1;
    static sbyte[, ] s_16 = new sbyte[, ]{{-10, -2, -128, 127, -80, 10, 24, 93, -128, -24}, {1, 31, 126, 80, -127, -119, 120, -127, 126, 1}, {126, -48, 10, 1, 40, 126, 0, -68, 126, -80}, {84, 0, 127, 31, 17, 127, 0, 0, 127, -128}, {0, 122, -55, 8, -10, 21, 10, 1, -66, 10}, {-57, 1, -128, -45, 41, 10, -1, -128, -127, 17}, {-10, 127, 35, 99, -10, 127, 1, 0, -2, 58}};
    static ushort s_17 = 52333;
    static byte s_18 = 1;
    static short s_19 = 1;
    static byte[] s_20 = new byte[]{143, 16, 1, 172, 109};
    static int s_21 = 2147483646;
    static uint[][] s_22 = new uint[][]{new uint[]{1U, 0U, 132292939U, 1541317924U}, new uint[]{0U, 0U, 2809163855U, 10U}, new uint[]{4294967294U, 488755429U, 3997249837U, 2U, 1U, 552332944U, 3630054909U, 1U, 0U}, new uint[]{1U}, new uint[]{4294967294U}, new uint[]{0U, 2730515622U, 1611871695U}, new uint[]{3394635652U, 4156053135U, 271345531U, 0U, 4294967295U, 0U, 4294967294U, 0U}, new uint[]{0, 3967018014U}, new uint[]{2U, 4294967294U, 4294967294U}};
    static bool[] s_23 = new bool[]{false, false, true, true, false, false, false, false};
    static short s_24 = 1;
    static byte s_25 = 1;
    static sbyte s_26 = 0;
    static long s_27 = -5276488358949898293L;
    static short s_28 = -24188;
    static long s_29 = 9223372036854775807L;
    static short[] s_30 = new short[]{32767, -21208, 9259, -16568, 1};
    static int s_31 = 10;
    static int[,, ] s_32 = new int[,, ]{{{1684627006, 567893548, -2097384269, 0, 0}, {10, 0, 0, 1775731947, 1179149299}, {398773950, 1, 1652481477, 0, -2147483647}, {1, -10, -736803111, 1, -10}}, {{-570687049, 1134633264, -94567418, 343631916, 0}, {1, -1, 10, 1715804116, 0}, {-2, 2147483646, 2147483646, 1025674361, -1719933266}, {1254318462, 1, 2147483647, -125811244, -2}}, {{-2147483648, -2147483648, 1731959151, -1818984964, -523843289}, {-175117629, -2147483647, -986102983, 2147483647, 829123963}, {1, 125348452, 371577867, -2, 936763915}, {2147483647, -1452486305, -10, 747197395, 0}}};
    static uint s_33 = 0U;
    static byte s_34 = 30;
    static long s_35 = -1L;
    static bool[][] s_36 = new bool[][]{new bool[]{true}, new bool[]{false, true}, new bool[]{false}, new bool[]{false}, new bool[]{true}};
    static int s_37 = 2;
    static int[][] s_38 = new int[][]{new int[]{-2147483648, 919655894}};
    static bool s_39 = true;
    static long[][, ] s_40 = new long[][, ]{new long[, ]{{-7992415552364922197L, 2L, -7233700375066217485L, 9223372036854775807L, -2L, 10L}, {6351545876477010827L, -5713031588102511822L, -4171116239725220506L, -9223372036854775807L, -3636488955248992161L, 5092030502199159774L}, {-6974194491934292512L, -4334206874623886496L, -8228578365445497022L, -5942857179925102965L, -2507977566635680942L, -9223372036854775808L}, {-7405046352208497299L, 1L, 0L, 9223372036854775807L, 6896096164173769362L, -3146720505347792329L}}};
    static ulong s_41 = 14379028663109837625UL;
    static short s_42 = -26110;
    static uint s_43 = 0U;
    static long s_44 = 9223372036854775807L;
    static byte[, ][][][, ] s_45 = new byte[, ][][][, ]{{new byte[][][, ]{new byte[][, ]{new byte[, ]{{187, 9, 4, 2, 0, 0, 1, 251, 28, 70}}, new byte[, ]{{0, 0, 34, 254, 1, 182, 0, 170, 238, 10}}, new byte[, ]{{0, 219, 2, 1, 0, 179, 1, 0, 2, 254}}, new byte[, ]{{86}}, new byte[, ]{{207, 1, 113, 197, 0, 112, 187, 1, 221, 0}}}, new byte[][, ]{new byte[, ]{{223, 238, 1, 0, 1, 0, 1, 232, 206, 10}}, new byte[, ]{{4, 1, 168, 255, 1, 254, 0, 131, 1, 219}}, new byte[, ]{{38, 1, 8, 28, 123, 254, 0, 201, 221, 209}}, new byte[, ]{{15, 1, 255, 10, 57, 186, 160, 54, 0, 250}}, new byte[, ]{{1, 1, 123, 1, 111, 1, 254, 255, 186, 1}}, new byte[, ]{{0, 0, 1, 1, 249, 0, 196, 0, 1, 241}}}, new byte[][, ]{new byte[, ]{{78, 142, 1, 0, 234, 32, 222, 57, 255, 56}}, new byte[, ]{{38, 84, 254, 120, 255, 1, 254, 101, 1, 1}}, new byte[, ]{{45, 1, 251, 2, 255, 252, 172, 1, 254, 21}}, new byte[, ]{{24, 113, 0, 163, 140, 0, 1, 191, 67, 134}}, new byte[, ]{{255, 188, 173, 234, 255, 1, 104, 18, 1, 52}}, new byte[, ]{{25, 0, 0, 65, 71, 38, 248, 3, 83, 234}}, new byte[, ]{{188, 0, 1, 179, 2, 255, 0, 0, 152, 86}}}}}};
    static bool s_46 = true;
    static ulong s_47 = 7534743659986054350UL;
    static bool s_48 = false;
    static byte s_49 = 167;
    static ushort[][] s_50 = new ushort[][]{new ushort[]{1, 51866, 0, 0, 6697}, new ushort[]{15151, 11331}, new ushort[]{19272, 65535, 12068, 65534, 57184, 65534, 0, 10}, new ushort[]{0, 0, 8816, 65535, 43228, 1, 11154}, new ushort[]{42405, 0, 1}, new ushort[]{0, 2636, 38370, 22989}, new ushort[]{46234, 0, 1, 0, 65535, 24283, 50920, 60970, 0}};
    static ulong s_51 = 0UL;
    static byte[] s_52 = new byte[]{210, 254, 1, 222, 0, 181};
    static bool[,, ] s_53 = new bool[,, ]{{{false, false, true, true, true}, {true, true, true, false, false}, {false, false, false, true, false}, {true, false, true, true, false}, {false, false, false, false, false}, {true, false, true, true, true}}, {{false, false, true, true, false}, {true, false, false, false, false}, {false, false, true, false, true}, {false, false, false, true, true}, {false, false, false, true, true}, {true, false, false, false, false}}, {{false, false, false, false, true}, {true, false, false, false, true}, {true, true, false, true, true}, {true, false, false, false, false}, {false, true, false, true, true}, {false, false, false, false, false}}, {{true, false, false, true, true}, {true, true, false, false, true}, {true, false, true, true, false}, {true, true, false, false, false}, {false, false, false, false, true}, {false, true, false, true, true}}, {{false, false, false, true, false}, {false, true, true, false, true}, {false, false, true, false, true}, {true, true, false, true, true}, {false, false, false, true, false}, {true, true, true, false, true}}, {{true, false, true, false, false}, {false, false, true, false, false}, {false, true, false, true, true}, {false, false, true, false, false}, {true, true, false, true, true}, {false, true, false, false, true}}, {{true, false, false, false, true}, {false, false, false, true, true}, {true, false, false, false, false}, {true, true, true, true, false}, {true, true, false, true, false}, {true, true, true, true, true}}, {{false, true, false, false, false}, {true, true, false, false, true}, {true, false, false, false, true}, {false, true, true, false, false}, {true, false, true, false, false}, {true, false, false, false, true}}, {{false, false, true, false, false}, {false, false, true, false, false}, {true, true, true, false, false}, {true, false, true, true, false}, {true, true, false, false, false}, {false, false, true, false, true}}, {{false, false, false, true, true}, {true, true, false, true, false}, {false, true, false, false, true}, {true, true, false, false, false}, {true, false, true, true, true}, {false, true, false, false, false}}};
    static short[] s_54 = new short[]{0};
    static bool[] s_55 = new bool[]{false};
    static sbyte s_56 = 51;
    static ulong s_57 = 18446744073709551615UL;
    static byte s_58 = 0;
    static long[] s_59 = new long[]{-2L};
    static byte s_60 = 183;
    static bool s_61 = true;
    static byte s_62 = 221;
    static ushort s_63 = 10;
    static byte[] s_64 = new byte[]{137};
    static byte s_65 = 115;
    static ushort s_66 = 1;
    static uint s_67 = 1U;
    static uint s_68 = 1U;
    static sbyte s_69 = 1;
    static ulong s_70 = 9931359137184661811UL;
    static ushort s_71 = 47833;
    static sbyte[] s_72 = new sbyte[]{0, -89};
    static long s_73 = 4433513382959200365L;
    static short s_74 = 32766;
    static short[] s_75 = new short[]{0, -16223, 12414, 1, -23240, -22783, 29677, 2};
    static ushort s_76 = 21018;
    static long s_77 = -10L;
    static sbyte[] s_78 = new sbyte[]{-10, -127};
    static long s_79 = -7240599102791735317L;
    static byte s_80 = 176;
    static uint s_81 = 1575519253U;
    static ushort s_82 = 65534;
    static int[, ] s_83 = new int[, ]{{-91266160, -155858831, -844708500, 1, 2, 1372204320, -560255575, 1289851176, -10, -1258794678}, {427412787, 1, 0, 1184100929, 2081490509, 1654702316, 65075659, -2, 0, 2147483646}, {-1708551524, 10, 0, 0, -1589220109, 1, 2147483647, -2147483648, 114863842, 0}, {-2147483648, -1525232497, -1458931372, 2147483646, 668276572, -1271018063, 2147483646, 1, 37147830, 1}, {2147483647, 1736784398, 2147483647, 1, -2147483648, -604620144, 1728231666, -2147483647, 1, -2147483647}, {-2147483648, 316503196, -2, -1382137729, -2147483648, 1, -1226197826, 2011857211, 60883710, 1191878440}, {413403784, -648566239, 2147483647, 2147483647, 1293590376, 1, 2, -2147483647, -2147483648, -2147483648}};
    static long[] s_84 = new long[]{0L};
    static ushort[] s_85 = new ushort[]{10, 33956, 5112};
    static ulong[][][][] s_86 = new ulong[][][][]{new ulong[][][]{new ulong[][]{new ulong[]{9473705828934257457UL, 11681380003416049046UL, 2UL}}, new ulong[][]{new ulong[]{18446744073709551614UL}}, new ulong[][]{new ulong[]{848190615324469612UL, 2925960094248593586UL}, new ulong[]{10756069151345801327UL}, new ulong[]{1UL, 7048877750559284168UL}}, new ulong[][]{new ulong[]{18446744073709551615UL, 1UL}, new ulong[]{639116403792133790UL, 17351212131294472002UL}}, new ulong[][]{new ulong[]{1UL, 1UL}, new ulong[]{1UL}}, new ulong[][]{new ulong[]{0UL, 1835784703590513921UL}}}, new ulong[][][]{new ulong[][]{new ulong[]{18446744073709551614UL}, new ulong[]{7255834492879790079UL}, new ulong[]{1UL, 10549269101587010861UL}}, new ulong[][]{new ulong[]{1UL, 6937854609209454588UL, 18446744073709551615UL}, new ulong[]{9775876505139152478UL, 13995507868386262262UL, 5472050023709486820UL}, new ulong[]{17595033409978435908UL}}, new ulong[][]{new ulong[]{1UL, 10766519060016844268UL, 4439822146110601278UL}}, new ulong[][]{new ulong[]{7253602645301238740UL, 0UL, 1UL}}, new ulong[][]{new ulong[]{8971264753139576801UL}}, new ulong[][]{new ulong[]{0UL, 18446744073709551614UL}, new ulong[]{18241470660611436813UL, 10821715400680997689UL, 5284737582520043660UL}, new ulong[]{0UL, 1UL, 752503329734952533UL}}}};
    static bool s_87 = true;
    static ushort[] s_88 = new ushort[]{63268, 11840, 58711, 18436, 1, 45518, 1, 16752, 0};
    static bool s_89 = true;
    static ulong s_90 = 1UL;
    static long s_91 = -10L;
    static ushort s_92 = 10;
    static int s_93 = 1817748138;
    static bool s_94 = false;
    static long s_95 = -6719449326620905016L;
    static sbyte s_96 = -102;
    static short[] s_97 = new short[]{-9562};
    static ulong[, ] s_98 = new ulong[, ]{{16441191778846550313UL, 18446744073709551615UL}, {5189913475039122932UL, 8279421832472410543UL}, {17000380444521325886UL, 1UL}};
    static sbyte s_99 = -127;
    static ulong[] s_100 = new ulong[]{18446744073709551615UL, 0UL, 3672647257117954802UL, 18446744073709551614UL, 1UL, 0UL, 3933871564268361431UL, 947428867697683813UL};
    static bool[, ] s_101 = new bool[, ]{{false, true, true, true, true, false, false, true, false}, {true, false, false, false, false, false, false, false, false}, {true, false, false, true, false, false, true, false, false}};
    static short s_102 = -20649;
    static byte s_103 = 1;
    static bool s_104 = true;
    static short s_105 = 0;
    static int[][] s_106 = new int[][]{new int[]{0}, new int[]{1203970180, 1, 2147483647, -2147483647, 964382849, -1671108894}, new int[]{2147483646, 731406230}};
    static uint s_107 = 1519880997U;
    static int s_108 = -1626333939;
    static byte s_109 = 0;
    static byte s_110 = 68;
    static ulong s_111 = 8400943349787705141UL;
    static short s_112 = -21694;
    static short s_113 = 1;
    static byte s_114 = 236;
    static short[] s_115 = new short[]{-32768, 19759, 32766, -31188, 0, 0, -2};
    static long s_116 = 5852970427657993206L;
    static sbyte[][] s_117 = new sbyte[][]{new sbyte[]{-127, -128, 0, -10, 126, -1, -23}, new sbyte[]{1, -2, -128, -93}, new sbyte[]{98, -7, 0, 1, 127, -41, 51}, new sbyte[]{103, 127, -123, 126, 110, 126}, new sbyte[]{0, 67, 4, -50}, new sbyte[]{-21, -101, -71, 0, 91}};
    static bool s_118 = true;
    static short s_119 = -25405;
    static byte[][] s_120 = new byte[][]{new byte[]{78, 0}, new byte[]{52}, new byte[]{87}, new byte[]{21, 1}, new byte[]{0, 255}, new byte[]{0, 10}, new byte[]{81, 88}, new byte[]{1, 194}};
    static short[][][] s_121 = new short[][][]{new short[][]{new short[]{-10279, -10, 0, -5705}}, new short[][]{new short[]{-13814}}, new short[][]{new short[]{23066, -32767}}, new short[][]{new short[]{1688, -28885, 0, 24573}}, new short[][]{new short[]{28217, -18541, -5536, -23457}}, new short[][]{new short[]{1, -1628, -2398}}, new short[][]{new short[]{-32768, -29446, -31858}}, new short[][]{new short[]{19793}}, new short[][]{new short[]{-18737, -2, 32766, 1}}, new short[][]{new short[]{32767, -6990, 1}}};
    static bool s_122 = false;
    static byte s_123 = 1;
    static uint s_124 = 4294967294U;
    public static void Main(Fuzzlyn.Execution.IRuntime rt)
    {
        s_rt = rt;
        M0();
    }

    static void M0()
    {
        byte var0 = (byte)(-1592527126 | (byte)(1 % (sbyte)((sbyte)(1 - (int)(0UL * (uint)(0 % (ushort)((ushort)(0 % (int)((int)(M1(2, 880340794U, (byte)(490360998U - (long)((ulong)(s_113++ - (int)M72()) + (byte)M36())), 10L, ref s_117, ref s_9[0], ref s_26, 37, ref s_60, s_47, ref s_78, (byte)M81(), s_77++) + (sbyte)M3((ushort)M18(), ref s_70, (short)(s_108 / (long)(-6951955190531076674L | 1)), -2057119971763330565L)) | 1)) | 1)))) | 1)));
    }

    static uint M1(byte arg0, uint arg1, byte arg2, long arg3, ref sbyte[][] arg4, ref sbyte arg5, ref sbyte arg6, sbyte arg7, ref byte arg8, ulong arg9, ref sbyte[] arg10, byte arg11, long arg12)
    {
        M2(ref s_22);
        return 185228006U;
    }

    static ulong M2(ref uint[][] arg0)
    {
        M11(arg0[0]);
        return (ulong)(s_109 ^ s_69);
    }

    static int M3(ushort arg0, ref ulong arg1, short arg2, long arg3)
    {
        return default(int);
    }

    static bool M4(ref ulong[, ] arg0)
    {
        return default(bool);
    }

    static ref sbyte[] M5(ushort arg0, long arg1, long[] arg2)
    {
        return ref s_2;
    }

    static bool M6(bool arg0, ushort arg1, long arg2, ref ushort arg3, sbyte arg4, byte arg5)
    {
        return default(bool);
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
        return M47((long)(arg0[0] / (ulong)(0UL | 1)));
    }

    static byte M12(sbyte[, ] arg0, byte arg1, short arg2)
    {
        try
        {
            sbyte[] var2 = new sbyte[]{126, 12, 70, 17, 94, -127, -128, -76, 127};
        }
        finally
        {
            sbyte var3 = arg0[0, 0];
            sbyte var4 = arg0[0, 0];
            s_rt.Checksum("c_28", var4);
            M13(arg1, ref s_1[0], new int[]{450864205, -533136863, 547591822, 1, 1799500906, 2147483646});
            s_1[0] = (uint)M13(arg1, ref s_1[0], s_4[0]);
            s_1[0] = 0U;
        }

        s_7 = -21984 >= arg1++;
        if (!s_7)
        {
            arg2 = (short)(51431005U | M13(arg1, ref s_1[0], new int[]{751191425, 1, -2147483647, -2003659586, 0, -595842771}));
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

    static bool M14()
    {
        return default(bool);
    }

    static bool[] M15()
    {
        return default(bool[]);
    }

    static ulong M16()
    {
        return default(ulong);
    }

    static short M17(sbyte arg0, ref uint arg1, short arg2, ulong arg3, int arg4, sbyte arg5)
    {
        return default(short);
    }

    static sbyte M18()
    {
        return default(sbyte);
    }

    static ulong[] M19(ref uint arg0, short arg1, uint[] arg2)
    {
        return default(ulong[]);
    }

    static ref uint[, ] M20()
    {
        return ref s_13;
    }

    static sbyte M21(int arg0)
    {
        return default(sbyte);
    }

    static byte M22()
    {
        return default(byte);
    }

    static byte M23(byte arg0, int arg1, sbyte arg2, ref sbyte[, ] arg3, long arg4, ushort arg5, sbyte arg6, uint arg7, ushort[] arg8)
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

    static int M27()
    {
        return default(int);
    }

    static ref short M28(long arg0)
    {
        return ref s_8;
    }

    static uint M29()
    {
        return default(uint);
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

    static ref long M33(ref int[][] arg0)
    {
        return ref s_6;
    }

    static ref int[,, ] M34(long[][] arg0)
    {
        return ref s_32;
    }

    static sbyte M35(ushort[][] arg0, ref ulong arg1, ref long arg2)
    {
        return default(sbyte);
    }

    static uint M36()
    {
        return default(uint);
    }

    static bool M37(ref long arg0, short[, ][][, ] arg1, sbyte arg2, sbyte arg3)
    {
        return default(bool);
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

    static bool M41(short arg0)
    {
        return default(bool);
    }

    static ref uint M42()
    {
        return ref s_33;
    }

    static byte M43(ref short arg0)
    {
        return default(byte);
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

    static short[] M48(byte arg0)
    {
        return default(short[]);
    }

    static sbyte M49(ushort[] arg0, int arg1, short arg2)
    {
        return default(sbyte);
    }

    static ushort[, ] M50(ref int arg0)
    {
        return default(ushort[, ]);
    }

    static short M51(int arg0, uint arg1, ref bool arg2, int arg3, uint arg4)
    {
        return default(short);
    }

    static short M52(long arg0, ushort arg1)
    {
        return default(short);
    }

    static ulong M53(ref uint arg0)
    {
        return default(ulong);
    }

    static int M54(ref int arg0, ref long arg1)
    {
        return default(int);
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

    static ushort[] M58(sbyte[] arg0)
    {
        return default(ushort[]);
    }

    static byte M59(ushort[][] arg0, ref ulong[, ] arg1, ref ulong arg2)
    {
        return default(byte);
    }

    static int[] M60(ushort arg0)
    {
        return default(int[]);
    }

    static ulong M61()
    {
        return default(ulong);
    }

    static bool M62()
    {
        return default(bool);
    }

    static ref long M63()
    {
        return ref s_35;
    }

    static short M64()
    {
        return default(short);
    }

    static long[][] M65(sbyte arg0)
    {
        return default(long[][]);
    }

    static bool M66()
    {
        return default(bool);
    }

    static ulong M67(ushort[,, ] arg0)
    {
        return default(ulong);
    }

    static sbyte[] M68(sbyte arg0)
    {
        return default(sbyte[]);
    }

    static ref bool M69(sbyte arg0, int arg1, ulong arg2, ref sbyte arg3, byte arg4, bool arg5, long arg6)
    {
        return ref s_46;
    }

    static long M70()
    {
        return default(long);
    }

    static ushort[][] M71()
    {
        return default(ushort[][]);
    }

    static ushort M72()
    {
        return default(ushort);
    }

    static ref ulong[] M73(int arg0, sbyte arg1, ref sbyte arg2, bool arg3, long arg4, uint arg5)
    {
        return ref s_10;
    }

    static sbyte M74(ref sbyte arg0)
    {
        return default(sbyte);
    }

    static bool M75(ref bool[] arg0, bool arg1, bool arg2, ref int arg3, bool[] arg4)
    {
        return default(bool);
    }

    static ref int M76(ref ushort arg0, sbyte[] arg1, bool arg2)
    {
        return ref s_4[0][0];
    }

    static long M77(ulong[][] arg0, sbyte arg1, ushort[] arg2)
    {
        return default(long);
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

    static ref uint M82()
    {
        return ref s_33;
    }

    static uint M83()
    {
        return default(uint);
    }

    static int M84()
    {
        return default(int);
    }

    static bool M85(ref ulong arg0)
    {
        return default(bool);
    }

    static short[][] M86(int arg0, ushort arg1, ulong arg2, long arg3, ref long arg4, sbyte[] arg5, uint arg6)
    {
        return default(short[][]);
    }

    static uint M87()
    {
        return default(uint);
    }

    static uint M88()
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

    static short M91()
    {
        return default(short);
    }

    static uint M92(byte arg0)
    {
        return default(uint);
    }

    static short M93()
    {
        return default(short);
    }

    static byte M94()
    {
        return default(byte);
    }

    static long M95()
    {
        return default(long);
    }

    static int M96(short arg0)
    {
        return default(int);
    }

    static int M97()
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

    static short M100()
    {
        return default(short);
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