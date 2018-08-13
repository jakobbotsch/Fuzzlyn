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
    static uint[][] s_22 = new uint[][]{new uint[]{1U, 0U, 132292939U, 1541317924U}, new uint[]{0U, 0U, 2809163855U, 10U}, new uint[]{4294967294U, 488755429U, 3997249837U, 2U, 1U, 552332944U, 3630054909U, 1U, 0U}, new uint[]{1U}, new uint[]{4294967294U, 1U}, new uint[]{0U, 2730515622U, 1611871695U}, new uint[]{3394635652U, 4156053135U, 271345531U, 0U, 4294967295U, 0U, 4294967294U, 0U}, new uint[]{1605839441U, 3967018014U}, new uint[]{2U, 4294967294U, 4294967294U}};
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
    static byte[, ][][][, ] s_45 = new byte[, ][][][, ]{{new byte[][][, ]{new byte[][, ]{new byte[, ]{{187, 9, 4, 2, 0, 0, 1, 251, 28, 70}}, new byte[, ]{{0, 0, 34, 254, 1, 182, 0, 170, 238, 10}}, new byte[, ]{{0, 219, 2, 1, 0, 179, 1, 0, 2, 254}}, new byte[, ]{{86, 8, 0, 255, 229, 118, 1, 0, 147, 1}}, new byte[, ]{{207, 1, 113, 197, 0, 112, 187, 1, 221, 0}}}, new byte[][, ]{new byte[, ]{{223, 238, 1, 0, 1, 0, 1, 232, 206, 10}}, new byte[, ]{{4, 1, 168, 255, 1, 254, 0, 131, 1, 219}}, new byte[, ]{{38, 1, 8, 28, 123, 254, 0, 201, 221, 209}}, new byte[, ]{{15, 1, 255, 10, 57, 186, 160, 54, 0, 250}}, new byte[, ]{{1, 1, 123, 1, 111, 1, 254, 255, 186, 1}}, new byte[, ]{{0, 0, 1, 1, 249, 0, 196, 0, 1, 241}}}, new byte[][, ]{new byte[, ]{{78, 142, 1, 0, 234, 32, 222, 57, 255, 56}}, new byte[, ]{{38, 84, 254, 120, 255, 1, 254, 101, 1, 1}}, new byte[, ]{{45, 1, 251, 2, 255, 252, 172, 1, 254, 21}}, new byte[, ]{{24, 113, 0, 163, 140, 0, 1, 191, 67, 134}}, new byte[, ]{{255, 188, 173, 234, 255, 1, 104, 18, 1, 52}}, new byte[, ]{{25, 0, 0, 65, 71, 38, 248, 3, 83, 234}}, new byte[, ]{{188, 0, 1, 179, 2, 255, 0, 24, 152, 86}}}}}};
    static bool s_46 = true;
    static ulong s_47 = 7534743659986054350UL;
    static bool s_48 = false;
    static byte s_49 = 167;
    static ushort[][] s_50 = new ushort[][]{new ushort[]{1, 51866, 0, 0, 6697}, new ushort[]{15151, 11331}, new ushort[]{19272, 65535, 12068, 65534, 57184, 65534, 0, 10}, new ushort[]{0, 0, 8816, 65535, 43228, 1, 11154}, new ushort[]{42405, 0, 1}, new ushort[]{0, 2636, 38370, 22989}, new ushort[]{46234, 0, 1, 0, 65535, 24283, 50920, 60970, 0}};
    static ulong s_51 = 0UL;
    static byte[] s_52 = new byte[]{210, 254, 1, 222, 0, 181};
    static bool[,, ] s_53 = new bool[,, ]{{{false, false, true, true, true}, {true, true, true, false, false}, {false, false, false, true, false}, {true, false, true, true, false}, {false, false, false, false, false}, {true, false, true, true, true}}, {{false, false, true, true, false}, {true, false, false, false, false}, {false, false, true, false, true}, {false, false, false, true, true}, {false, false, false, true, true}, {true, false, false, false, false}}, {{false, false, false, false, true}, {true, false, false, false, true}, {true, true, false, true, true}, {true, false, false, false, false}, {false, true, false, true, true}, {false, false, false, false, false}}, {{true, false, false, true, true}, {true, true, false, false, true}, {true, false, true, true, false}, {true, true, false, false, false}, {false, false, false, false, true}, {false, true, false, true, true}}, {{false, false, false, true, false}, {false, true, true, false, true}, {false, false, true, false, true}, {true, true, false, true, true}, {false, false, false, true, false}, {true, true, true, false, true}}, {{true, false, true, false, false}, {false, false, true, false, false}, {false, true, false, true, true}, {false, false, true, false, false}, {true, true, false, true, true}, {false, true, false, false, true}}, {{true, false, false, false, true}, {false, false, false, true, true}, {true, false, false, false, false}, {true, true, true, true, false}, {true, true, false, true, false}, {true, true, true, true, true}}, {{false, true, false, false, false}, {true, true, false, false, true}, {true, false, false, false, true}, {false, true, true, false, false}, {true, false, true, false, false}, {true, false, false, false, true}}, {{false, false, true, false, false}, {false, false, true, false, false}, {true, true, true, false, false}, {true, false, true, true, false}, {true, true, false, false, false}, {false, false, true, false, true}}, {{false, false, false, true, true}, {true, true, false, true, false}, {false, true, false, false, true}, {true, true, false, false, false}, {true, false, true, true, true}, {false, true, false, false, false}}};
    static short[] s_54 = new short[]{32767, 0, -27973};
    static bool[] s_55 = new bool[]{false, true, false, true, false, true, true, true, true};
    static sbyte s_56 = 51;
    static ulong s_57 = 18446744073709551615UL;
    static byte s_58 = 0;
    static long[] s_59 = new long[]{-2L};
    static byte s_60 = 183;
    static bool s_61 = true;
    static byte s_62 = 221;
    static ushort s_63 = 10;
    static byte[] s_64 = new byte[]{137, 202, 14, 163, 238, 211, 21, 0, 1};
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
    static int[, ] s_83 = new int[, ]{{-91266160, -155858831, -844708500, 1, 2, 1372204320, -560255575, 1289851176, -10, -1258794678}, {427412787, 1, 0, 1184100929, 2081490509, 1654702316, 65075659, -2, 0, 2147483646}, {-1708551524, 10, 0, 0, -1589220109, 1, 2147483647, -2147483648, 114863842, 535664270}, {-2147483648, -1525232497, -1458931372, 2147483646, 668276572, -1271018063, 2147483646, 1, 37147830, 1}, {2147483647, 1736784398, 2147483647, 1, -2147483648, -604620144, 1728231666, -2147483647, 1, -2147483647}, {-2147483648, 316503196, -2, -1382137729, -2147483648, 1, -1226197826, 2011857211, 60883710, 1191878440}, {413403784, -648566239, 2147483647, 2147483647, 1293590376, 1, 2, -2147483647, -2147483648, -2147483648}};
    static long[] s_84 = new long[]{0L};
    static ushort[] s_85 = new ushort[]{10, 33956, 5112};
    static ulong[][][][] s_86 = new ulong[][][][]{new ulong[][][]{new ulong[][]{new ulong[]{9473705828934257457UL, 11681380003416049046UL, 2UL}}, new ulong[][]{new ulong[]{18446744073709551614UL}, new ulong[]{12480991256520289877UL, 17132974197241911813UL}, new ulong[]{4517508128294569059UL, 11444679574451264727UL}}, new ulong[][]{new ulong[]{848190615324469612UL, 2925960094248593586UL}, new ulong[]{10756069151345801327UL}, new ulong[]{1UL, 7048877750559284168UL}}, new ulong[][]{new ulong[]{18446744073709551615UL, 1UL}, new ulong[]{639116403792133790UL, 17351212131294472002UL}}, new ulong[][]{new ulong[]{1UL, 1UL}, new ulong[]{1UL}}, new ulong[][]{new ulong[]{0UL, 1835784703590513921UL}}}, new ulong[][][]{new ulong[][]{new ulong[]{18446744073709551614UL}, new ulong[]{7255834492879790079UL}, new ulong[]{1UL, 10549269101587010861UL}}, new ulong[][]{new ulong[]{1UL, 6937854609209454588UL, 18446744073709551615UL}, new ulong[]{9775876505139152478UL, 13995507868386262262UL, 5472050023709486820UL}, new ulong[]{17595033409978435908UL}}, new ulong[][]{new ulong[]{1UL, 10766519060016844268UL, 4439822146110601278UL}}, new ulong[][]{new ulong[]{7253602645301238740UL, 0UL, 1UL}}, new ulong[][]{new ulong[]{8971264753139576801UL}, new ulong[]{3258859164436283474UL, 18446744073709551614UL, 6573904701021239622UL}}, new ulong[][]{new ulong[]{0UL, 18446744073709551614UL}, new ulong[]{18241470660611436813UL, 10821715400680997689UL, 5284737582520043660UL}, new ulong[]{0UL, 1UL, 752503329734952533UL}}}};
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
    static short[] s_97 = new short[]{-9562, -32571, -32768, 32766, 32767, -23652, 15244, -16287, -2};
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
        byte var0 = default(byte);
        ushort[, ] var2 = default(ushort[, ]);
        ushort[] var5 = default(ushort[]);
        byte var6 = default(byte);
        short var7 = default(short);
        uint var8 = default(uint);
        byte var9 = default(byte);
        uint var10 = default(uint);
        var0 = (byte)(-1592527126 | (byte)(1 % (sbyte)((sbyte)(1 - (int)(0UL * (uint)(0 % (ushort)((ushort)(0 % (int)((int)(M1(2, 880340794U, (byte)(490360998U - (long)((ulong)(s_113++ - (int)M72()) + (byte)M36())), 10L, ref s_117, ref s_9[0], ref s_26, 37, ref s_60, s_47, ref s_78, (byte)M81(), s_77++) + (sbyte)M3((ushort)M18(), ref s_70, (short)(s_108 / (long)(-6951955190531076674L | 1)), -2057119971763330565L)) | 1)) | 1)))) | 1)));
    }

    static uint M1(byte arg0, uint arg1, byte arg2, long arg3, ref sbyte[][] arg4, ref sbyte arg5, ref sbyte arg6, sbyte arg7, ref byte arg8, ulong arg9, ref sbyte[] arg10, byte arg11, long arg12)
    {
        s_1 = new uint[]{4294967294U, 3086801155U, 0U, 2U};
        arg1 = arg1;
        arg9 = M2(ref s_22);
        s_rt.Checksum("c_631", arg0);
        s_rt.Checksum("c_632", arg1);
        s_rt.Checksum("c_633", arg2);
        s_rt.Checksum("c_634", arg3);
        s_rt.Checksum("c_635", arg4[0][0]);
        s_rt.Checksum("c_636", arg5);
        s_rt.Checksum("c_637", arg6);
        s_rt.Checksum("c_638", arg7);
        s_rt.Checksum("c_639", arg8);
        s_rt.Checksum("c_640", arg9);
        s_rt.Checksum("c_641", arg10[0]);
        s_rt.Checksum("c_642", arg11);
        s_rt.Checksum("c_643", arg12);
        return 185228006U;
    }

    static ulong M2(ref uint[][] arg0)
    {
        arg0 = ref arg0;
        {
            M3(0, ref s_70, s_74--, (long)(-127 | (byte)((int)M64() + M11(arg0[0]))));
        }

        s_rt.Checksum("c_630", arg0[0][0]);
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
        long var0 = default(long);
        arg0 = arg0;
        if (255 < (short)(arg0++ ^ (92 | (short)(arg1 * 69))))
        {
            var0 = arg2[0]++;
            arg2[0] = var0;
            s_rt.Checksum("c_0", var0);
        }

        arg0 = arg0++;
        arg2 = arg2;
        arg1 = 9223372036854775807L;
        s_rt.Checksum("c_1", arg0);
        s_rt.Checksum("c_2", arg1);
        s_rt.Checksum("c_3", arg2[0]);
        return ref s_2;
    }

    static bool M6(bool arg0, ushort arg1, long arg2, ref ushort arg3, sbyte arg4, byte arg5)
    {
        return default(bool);
    }

    static int M7()
    {
        M8();
        s_2 = s_2;
        s_2 = new sbyte[]{109, 53, -128, 61, -71, -10, 10};
        return (int)M8();
    }

    static uint M8()
    {
        ++s_1[0];
        return s_1[0];
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
        arg0 = new uint[]{1497508146U, 1U, 866623142U, 1969664608U, 3022816759U, 4060504330U, 1535333176U, 0U};
        try
        {
            M12(s_16, s_25--, s_19);
        }
        finally
        {
            ref short var0 = ref s_30[0];
            s_rt.Checksum("c_265", var0);
        }

        arg0[0] = arg0[0];
        s_rt.Checksum("c_266", arg0[0]);
        return M47((long)(arg0[0] / (ulong)(0UL | 1)));
    }

    static byte M12(sbyte[, ] arg0, byte arg1, short arg2)
    {
        sbyte var0 = default(sbyte);
        short var1 = default(short);
        sbyte[] var2 = default(sbyte[]);
        sbyte var3 = default(sbyte);
        long var5 = default(long);
        ushort var6 = default(ushort);
        int var7 = default(int);
        short var8 = default(short);
        byte var9 = default(byte);
        int var10 = default(int);
        uint var11 = default(uint);
        byte var12 = default(byte);
        bool var13 = default(bool);
        uint var14 = default(uint);
        ulong var15 = default(ulong);
        ulong var17 = default(ulong);
        bool var18 = default(bool);
        {
            try
            {
                try
                {
                    if (s_4[0][0] != 1)
                    {
                        var0 = arg0[0, 0];
                        arg0[0, 0] = s_2[0];
                        arg0 = arg0;
                        var1 = -32767;
                        arg0[0, 0] = -1;
                        var2 = new sbyte[]{126, 12, 70, 17, 94, -127, -128, -76, 127};
                        s_rt.Checksum("c_24", var0);
                        s_rt.Checksum("c_25", var1);
                        s_rt.Checksum("c_26", var2[0]);
                    }
                }
                finally
                {
                    arg2 = 1;
                    arg1 = arg1;
                    s_3 = s_3;
                    {
                        {
                            arg1 = arg1++;
                            var3 = arg0[0, 0];
                            var3 = arg0[0, 0];
                            ref sbyte var4 = ref arg0[0, 0];
                            s_rt.Checksum("c_27", var3);
                            s_rt.Checksum("c_28", var4);
                        }

                        M13(arg1, ref s_1[0], new int[]{450864205, -533136863, 547591822, 1, 1799500906, 2147483646});
                        s_5 = (ulong)M13(231, ref s_1[0], new int[]{-1, 1, 1, 0, -2147483648, 0, 1, -5574577});
                        s_1[0] = (uint)M13(arg1, ref s_1[0], s_4[0]);
                    }

                    s_1[0] = 0U;
                    arg0[0, 0] = arg0[0, 0];
                }
            }
            finally
            {
                s_1[0] = s_1[0]++;
                arg1 = arg1;
            }

            s_7 = -21984 >= arg1++;
        }

        arg1 = (byte)M13(arg1, ref s_1[0], s_4[0]);
        try
        {
            arg0[0, 0] = arg0[0, 0]--;
        }
        finally
        {
            var5 = (long)(1 / (short)((short)M13(arg1--, ref s_1[0], new int[]{2044349693, -2147483647, 1, -2147483648, -10, 1350566660, -71041840, 0, 450850015}) | 1));
            s_rt.Checksum("c_35", var5);
        }

        if (s_7)
        {
            var6 = (ushort)M13(arg1, ref s_1[0], new int[]{-2147483647, -1502575172, -85402593, -2071209967, -2000332689, 767121773, 1418662593, -2147483647, -1162568171, -2147483648});
            s_rt.Checksum("c_36", var6);
        }
        else
        {
            var7 = 2147483647;
            arg2 = (short)(51431005U | (short)M13(arg1, ref s_1[0], new int[]{751191425, 1, -2147483647, -2003659586, 0, -595842771}));
            M13(arg1, ref s_1[0], new int[]{-1111734879, 2147483646, 923341667, 82077352, 0, 2147483646});
            arg1 = 10;
            var8 = arg2;
            arg0 = new sbyte[, ]{{0, 48, -128}, {126, -104, -4}, {-1, 10, 126}, {-95, 1, -64}, {-90, 20, 127}, {-67, 126, -62}, {65, -128, 1}, {-1, 0, 0}, {92, 31, 127}};
            if (arg0[0, 0] > var8)
            {
                if (s_7)
                {
                    arg0[0, 0] = arg0[0, 0];
                    arg1 >>= var7;
                    M13(arg1, ref s_1[0], new int[]{2147483646, 1535561362, -423977565, 84686650, -2027895321, -766944526, -2147483648});
                }
                else
                {
                    s_6 = s_6++;
                    arg2 = arg2;
                    s_7 = false != (true || s_7);
                    M13((byte)(-9223372036854775808L * arg1), ref s_1[0], new int[]{-533454929, 1226706085, -2058640502, 219790284});
                    var9 = 154;
                    var9 /= (byte)(224 | 1);
                    var10 = var7;
                    var10 = var10;
                    s_rt.Checksum("c_37", var9);
                    s_rt.Checksum("c_38", var10);
                }

                var11 = s_1[0];
                if ((ulong)M13(arg1, ref s_1[0], new int[]{469759899}) <= var11++)
                {
                    s_8 = var8;
                    M13(1, ref s_1[0], s_4[0]);
                    var12 = 26;
                    var8 = var8;
                    s_4[0][0] = 1;
                    M13(arg1, ref s_1[0], s_4[0]);
                    arg2 = 14599;
                    s_rt.Checksum("c_39", var12);
                }

                s_rt.Checksum("c_40", var11);
            }
            else
            {
                s_9 = new sbyte[]{127, -19};
                var7 = 1181877853;
            }

            if (M14())
            {
                var13 = s_36[0][0];
                arg2 = var8;
                arg0[0, 0] += arg0[0, 0];
                var14 = s_13[0, 0]++ + s_34++;
                if (M26())
                {
                    M42();
                    s_44 = 0L;
                    var8 = -2;
                    var15 = s_5;
                    arg0 = arg0;
                    ref byte[, ][][][, ] var16 = ref s_45;
                    {
                        var17 = (ulong)M27();
                        var18 = var13;
                        M27();
                        s_rt.Checksum("c_254", var17);
                        s_rt.Checksum("c_255", var18);
                    }

                    s_rt.Checksum("c_256", var15);
                    s_rt.Checksum("c_257", var16[0, 0][0][0][0, 0]);
                }

                s_rt.Checksum("c_258", var13);
                s_rt.Checksum("c_259", var14);
            }

            s_45[0, 0] = s_45[0, 0];
            s_rt.Checksum("c_260", var7);
            s_rt.Checksum("c_261", var8);
        }

        s_rt.Checksum("c_262", arg0[0, 0]);
        s_rt.Checksum("c_263", arg1);
        s_rt.Checksum("c_264", arg2);
        return arg1;
    }

    static int M13(byte arg0, ref uint arg1, int[] arg2)
    {
        int var2 = default(int);
        ref long var0 = ref s_6;
        if (arg0 > -406484741)
        {
            if (true && (25431 < (short)(1U * arg1)))
            {
                ref bool var1 = ref s_7;
                s_rt.Checksum("c_29", var1);
            }
            else
            {
                s_3 = new ushort[]{65535};
                if (true || (true || s_7))
                {
                    if (s_7)
                    {
                        arg1 = arg1;
                        var2 = arg2[0];
                        s_rt.Checksum("c_30", var2);
                    }
                }

                var0 = -7078116301479133748L;
            }
        }

        s_rt.Checksum("c_31", arg0);
        s_rt.Checksum("c_32", arg1);
        s_rt.Checksum("c_33", arg2[0]);
        s_rt.Checksum("c_34", var0);
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
        s_4 = new int[][]{new int[]{2, -105973443, 585475956}, new int[]{1657642978, -1707822875}, new int[]{1}, new int[]{-2147483648}, new int[]{-1475196895, 1828142884}, new int[]{2147483647}, new int[]{924938583}, new int[]{-1726076589, 1, 1}, new int[]{-1202747345, 268588811}, new int[]{-2, -137555522, -73775037}};
        s_rt.Checksum("c_44", arg0);
        s_rt.Checksum("c_45", arg1);
        s_rt.Checksum("c_46", arg2);
        s_rt.Checksum("c_47", arg3);
        s_rt.Checksum("c_48", arg4);
        s_rt.Checksum("c_49", arg5);
        return 1;
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
        bool var0 = default(bool);
        s_3 = new ushort[]{48756, 0, 1, 22371, 0, 3289, 1};
        {
            M21(s_4[0][0]++);
            s_3 = new ushort[]{26922, 3046, 38349, 25841, 28119, 0, 57341};
        }

        var0 = false;
        s_12 = (int)M21(1550332660);
        var0 = s_7;
        s_rt.Checksum("c_52", var0);
        return ref s_13;
    }

    static sbyte M21(int arg0)
    {
        if (s_7)
        {
            arg0 = arg0;
            arg0 = 928119432;
        }

        M22();
        arg0 = 2147483646;
        s_11 = (long)M22();
        if ((byte)(91 - arg0--) >= M22())
        {
            arg0 = arg0++;
            arg0 = -2147483647;
            M22();
        }

        s_rt.Checksum("c_51", arg0);
        return (sbyte)M22();
    }

    static byte M22()
    {
        s_5 = s_10[0];
        return 1;
    }

    static byte M23(byte arg0, int arg1, sbyte arg2, ref sbyte[, ] arg3, long arg4, ushort arg5, sbyte arg6, uint arg7, ushort[] arg8)
    {
        return default(byte);
    }

    static int M24()
    {
        s_10 = s_10;
        return s_4[0][0];
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
        ref sbyte var0 = ref s_9[0];
        if (var0 != s_11)
        {
            var0 = ref var0;
        }
        else
        {
            var0 = var0--;
            var0 = ref var0;
        }

        s_rt.Checksum("c_87", var0);
        return s_4[0][0]--;
    }

    static ref short M28(long arg0)
    {
        ushort[] var0 = default(ushort[]);
        int[] var1 = default(int[]);
        byte var3 = default(byte);
        long var4 = default(long);
        sbyte var5 = default(sbyte);
        byte var6 = default(byte);
        bool var7 = default(bool);
        return ref s_8;
    }

    static uint M29()
    {
        {
            return s_1[0];
        }

        return s_1[0];
    }

    static long M30()
    {
        return default(long);
    }

    static uint[] M31()
    {
        ulong var0 = default(ulong);
        var0 = 4787032656520567745UL;
        s_rt.Checksum("c_131", var0);
        return new uint[]{3193491721U, 10421100U, 1506745144U, 3833949630U, 76394140U, 1U, 10U, 0U};
    }

    static sbyte M32(byte arg0)
    {
        M33(ref s_4);
        s_rt.Checksum("c_145", arg0);
        return M35(new ushort[][]{new ushort[]{0, 63006}, new ushort[]{0}, new ushort[]{48792}, new ushort[]{0, 60115, 34623}, new ushort[]{41051, 18924, 25146}}, ref s_5, ref s_40[0][0, 0]);
    }

    static ref long M33(ref int[][] arg0)
    {
        M34(new long[][]{new long[]{-1L}, new long[]{8191470258571274071L}, new long[]{-6191645651997984133L}, new long[]{8370325159813168593L}});
        s_rt.Checksum("c_144", arg0[0][0]);
        return ref s_6;
    }

    static ref int[,, ] M34(long[][] arg0)
    {
        arg0[0] = new long[]{904619366651735080L, 10L, -9223372036854775808L, 1L, 10L, -8232593673608858547L, 9223372036854775807L, 9223372036854775807L, 9223372036854775806L};
        s_rt.Checksum("c_143", arg0[0][0]);
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
        bool var0 = default(bool);
        arg0 = arg0;
        arg2 = 14;
        if (s_23[0])
        {
            s_33 = 3690277061U;
            arg1 = arg1;
        }
        else
        {
            var0 = false && (arg2 != arg1[0, 0][0][0, 0]);
            arg2 = arg3;
            s_rt.Checksum("c_148", var0);
        }

        arg1[0, 0][0][0, 0]--;
        s_rt.Checksum("c_149", arg0);
        s_rt.Checksum("c_150", arg1[0, 0][0][0, 0]);
        s_rt.Checksum("c_151", arg2);
        s_rt.Checksum("c_152", arg3);
        return false;
    }

    static bool M38(sbyte arg0, ushort arg1)
    {
        s_28 = s_30[0];
        s_34 = 1;
        s_rt.Checksum("c_158", arg0);
        s_rt.Checksum("c_159", arg1);
        return true;
    }

    static uint[] M39(ulong arg0, uint arg1)
    {
        int var0 = default(int);
        if (s_3[0] < -1)
        {
            var0 = -1433555422;
            s_rt.Checksum("c_174", var0);
        }

        s_30 = s_30;
        if (s_23[0])
        {
            if (s_34 > arg1)
            {
                arg0 = arg0;
            }
        }

        s_7 &= true;
        if (s_17 == s_26++)
        {
            arg1 = 3921717108U;
            arg1 = arg1;
            try
            {
                arg1 = arg1--;
            }
            finally
            {
                arg1 = arg1;
            }
        }

        s_rt.Checksum("c_175", arg0);
        s_rt.Checksum("c_176", arg1);
        return new uint[]{0U, 1U, 769262346U, 2799153063U, 0U, 1U, 2340544295U};
    }

    static long[] M40(short arg0, int arg1)
    {
        return default(long[]);
    }

    static bool M41(short arg0)
    {
        bool var0 = default(bool);
        ushort var1 = default(ushort);
        if (((byte)(arg0++ | -127) | (sbyte)(s_16[0, 0]-- * 722096999U)) == s_13[0, 0]--)
        {
            arg0 = (short)(-69 & s_17);
            {
                arg0 = -1;
                return s_7;
            }

            arg0 = -26283;
        }
        else
        {
            M42();
        }

        if (s_23[0])
        {
            arg0 = arg0++;
        }
        else
        {
            var0 = false || (arg0 > (uint)(arg0 + 2967628336U));
            {
                var1 = s_3[0];
                s_rt.Checksum("c_186", var1);
            }

            {
                M42();
            }

            arg0 = 1;
            s_rt.Checksum("c_187", var0);
        }

        s_rt.Checksum("c_188", arg0);
        return false;
    }

    static ref uint M42()
    {
        int var0 = default(int);
        var0 = s_12;
        var0 <<= 1965711740;
        var0 = var0++;
        s_rt.Checksum("c_185", var0);
        return ref s_33;
    }

    static byte M43(ref short arg0)
    {
        int[][] var0 = default(int[][]);
        var0 = new int[][]{new int[]{1921100264}, new int[]{0}, new int[]{1730370135}, new int[]{22642782}, new int[]{-793182294}, new int[]{-1}, new int[]{1}, new int[]{-2105634838}, new int[]{1302024080}};
        var0 = var0;
        s_rt.Checksum("c_201", arg0);
        s_rt.Checksum("c_202", var0[0][0]);
        return s_34;
    }

    static sbyte M44()
    {
        s_1[0] = 4180879016U;
        return -22;
    }

    static ulong M45(int arg0, ushort arg1, ref bool arg2, int arg3, short[] arg4)
    {
        return default(ulong);
    }

    static short M46(ushort[] arg0, uint arg1)
    {
        try
        {
            try
            {
                try
                {
                    M47(s_6);
                }
                finally
                {
                    M47(M47(9223372036854775807L)) = 0L;
                }
            }
            finally
            {
                M48((byte)M47(-1881504500373196860L));
                arg0 = arg0;
                M47(s_14[0]--);
                M48(26);
                s_42 = 0;
            }
        }
        finally
        {
            s_9[0] = 45;
        }

        arg1 = arg1++;
        s_rt.Checksum("c_216", arg0[0]);
        s_rt.Checksum("c_217", arg1);
        return s_24;
    }

    static ref long M47(long arg0)
    {
        long var0 = default(long);
        byte var1 = default(byte);
        ushort var2 = default(ushort);
        return ref s_29;
    }

    static short[] M48(byte arg0)
    {
        {
            arg0 = 0;
        }

        if (s_36[0][0])
        {
            s_11 = 7622133557170225341L;
        }

        s_rt.Checksum("c_211", arg0);
        return new short[]{-32767, 24875, 32767, 14425, 0, -32768, 32766, 32766};
    }

    static sbyte M49(ushort[] arg0, int arg1, short arg2)
    {
        s_41 = 15479215369114711402UL;
        M50(ref arg1);
        s_9[0] = s_2[0]++;
        arg1 ^= arg1;
        s_rt.Checksum("c_223", arg0[0]);
        s_rt.Checksum("c_224", arg1);
        s_rt.Checksum("c_225", arg2);
        return (sbyte)(s_21 & arg0[0]--);
    }

    static ushort[, ] M50(ref int arg0)
    {
        uint var1 = default(uint);
        sbyte var2 = default(sbyte);
        try
        {
            ref byte var0 = ref s_25;
            s_rt.Checksum("c_219", var0);
        }
        finally
        {
            var1 = 1U;
            s_rt.Checksum("c_220", var1);
        }

        try
        {
            s_24 = (short)(0UL & s_18);
        }
        finally
        {
            var2 = 17;
            s_rt.Checksum("c_221", var2);
        }

        s_22[0] = new uint[]{1099968913U, 4054913079U, 0U, 0U, 10U, 1U, 871899698U, 1U, 3439137607U, 2U};
        arg0 = ref arg0;
        s_rt.Checksum("c_222", arg0);
        return new ushort[, ]{{65534, 0, 0, 57866, 65535}, {0, 60829, 6449, 18064, 1}, {0, 0, 0, 57891, 49319}, {47696, 629, 0, 40034, 47045}, {63774, 22933, 4607, 48817, 45030}, {59509, 0, 65534, 65534, 8750}, {65534, 1, 26685, 22361, 4701}, {1, 65535, 1, 0, 59933}, {1, 0, 44096, 58598, 1}, {65535, 54828, 0, 65535, 22083}};
    }

    static short M51(int arg0, uint arg1, ref bool arg2, int arg3, uint arg4)
    {
        short var0 = default(short);
        var0 = 9946;
        s_rt.Checksum("c_235", arg0);
        s_rt.Checksum("c_236", arg1);
        s_rt.Checksum("c_237", arg2);
        s_rt.Checksum("c_238", arg3);
        s_rt.Checksum("c_239", arg4);
        s_rt.Checksum("c_240", var0);
        return var0;
    }

    static short M52(long arg0, ushort arg1)
    {
        ushort[] var0 = default(ushort[]);
        var0 = new ushort[]{0, 65534, 65535, 11030, 0, 0, 1, 46467};
        s_rt.Checksum("c_297", arg0);
        s_rt.Checksum("c_298", arg1);
        s_rt.Checksum("c_299", var0[0]);
        return 0;
    }

    static ulong M53(ref uint arg0)
    {
        return default(ulong);
    }

    static int M54(ref int arg0, ref long arg1)
    {
        bool var0 = default(bool);
        {
            M55(s_22[0], (sbyte)M81());
            if (s_53[0, 0, 0])
            {
                {
                    var0 = s_7;
                    if (var0)
                    {
                        M89(arg0);
                    }
                    else
                    {
                        s_101 = s_101;
                        s_39 = true;
                        M55(new uint[]{3870818745U, 0U, 2539678334U, 1U, 3015754263U, 1U}, s_99++);
                    }

                    arg0 = 0;
                    arg0 = -1246678707;
                    M69(s_99, M76(ref s_71, new sbyte[]{127, -127, -77, 0, 0, 126}, 21896 < (ushort)(s_17-- ^ 1))--, (ulong)(s_42-- / (int)(0 | 1)), ref s_26, 254, var0, (long)M87()) = M69((sbyte)M61(), -2147483648, (ulong)M70(), ref s_72[0], (byte)M56(), false, arg1);
                    s_rt.Checksum("c_559", var0);
                }
            }
        }

        s_rt.Checksum("c_560", arg0);
        s_rt.Checksum("c_561", arg1);
        return -2147483648;
    }

    static ushort[][] M55(uint[] arg0, sbyte arg1)
    {
        return default(ushort[][]);
    }

    static ref ulong M56()
    {
        byte var1 = default(byte);
        short var2 = default(short);
        long[, ] var3 = default(long[, ]);
        uint var4 = default(uint);
        ushort var5 = default(ushort);
        bool var6 = default(bool);
        short[][][] var7 = default(short[][][]);
        ref short var0 = ref s_30[0];
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
        arg0 = arg0;
        ref uint var0 = ref s_1[0];
        s_rt.Checksum("c_332", arg0);
        s_rt.Checksum("c_333", var0);
        return s_4[0];
    }

    static ulong M61()
    {
        return default(ulong);
    }

    static bool M62()
    {
        s_58 = 255;
        return s_7;
    }

    static ref long M63()
    {
        s_38[0][0] = -2;
        M64();
        s_21 = s_31++;
        return ref s_35;
    }

    static short M64()
    {
        return default(short);
    }

    static long[][] M65(sbyte arg0)
    {
        M66();
        M84();
        s_rt.Checksum("c_503", arg0);
        return new long[][]{new long[]{-560141165836780619L, -2570158660325190393L}, new long[]{-4433038264652697728L, 8414674925830972799L, 6075659794050118378L, 0L, 8632336213436303812L, -3527749624917822146L, 9223372036854775806L}, new long[]{-3224884425625331743L, -2140846024026865403L, -9223372036854775808L, 9223372036854775806L, 5092793882956095036L, 9223372036854775806L, 9223372036854775807L, 9223372036854775806L, -4913010782114410021L, 2270890734784443975L}};
    }

    static bool M66()
    {
        return default(bool);
    }

    static ulong M67(ushort[,, ] arg0)
    {
        arg0 = new ushort[,, ]{{{42743, 18929}, {0, 10}, {1, 60613}, {1419, 4356}, {2, 0}, {15803, 2}, {1, 0}, {0, 1}, {46098, 50198}}, {{1, 0}, {1, 50694}, {1, 47610}, {12698, 5471}, {10, 23078}, {10776, 17859}, {55685, 1}, {4206, 1}, {47863, 47404}}, {{65534, 65534}, {1, 31924}, {40004, 13448}, {35189, 0}, {10869, 0}, {0, 0}, {1, 29453}, {46434, 33198}, {0, 61070}}, {{30395, 62090}, {0, 53306}, {0, 1}, {0, 26434}, {0, 51819}, {13616, 0}, {65534, 1}, {29404, 12860}, {0, 1}}, {{65035, 65495}, {4644, 0}, {11957, 1}, {0, 1}, {47032, 0}, {0, 10}, {20172, 10}, {0, 0}, {55269, 65534}}, {{1, 55913}, {31377, 2}, {10321, 53527}, {1, 43318}, {65535, 52708}, {35209, 0}, {13870, 47552}, {1, 28512}, {11607, 0}}};
        s_rt.Checksum("c_340", arg0[0, 0, 0]);
        return 3224594519270290593UL;
    }

    static sbyte[] M68(sbyte arg0)
    {
        return default(sbyte[]);
    }

    static ref bool M69(sbyte arg0, int arg1, ulong arg2, ref sbyte arg3, byte arg4, bool arg5, long arg6)
    {
        int var0 = default(int);
        uint var1 = default(uint);
        uint var2 = default(uint);
        long var3 = default(long);
        return ref s_46;
    }

    static long M70()
    {
        return default(long);
    }

    static ushort[][] M71()
    {
        s_50[0][0] = 0;
        return s_50;
    }

    static ushort M72()
    {
        return default(ushort);
    }

    static ref ulong[] M73(int arg0, sbyte arg1, ref sbyte arg2, bool arg3, long arg4, uint arg5)
    {
        short var0 = default(short);
        bool var1 = default(bool);
        ulong var2 = default(ulong);
        int var3 = default(int);
        ushort var4 = default(ushort);
        ulong var5 = default(ulong);
        sbyte[] var6 = default(sbyte[]);
        uint[] var8 = default(uint[]);
        bool var9 = default(bool);
        ref short var7 = ref s_54[0];
        return ref s_10;
    }

    static sbyte M74(ref sbyte arg0)
    {
        M75(ref s_23, s_46, true, ref s_21, new bool[]{false, true, false, true, true, false, true});
        arg0 = (sbyte)(arg0 + s_52[0]++);
        arg0 = 127;
        s_rt.Checksum("c_417", arg0);
        return arg0;
    }

    static bool M75(ref bool[] arg0, bool arg1, bool arg2, ref int arg3, bool[] arg4)
    {
        return default(bool);
    }

    static ref int M76(ref ushort arg0, sbyte[] arg1, bool arg2)
    {
        uint var0 = default(uint);
        short var1 = default(short);
        int var2 = default(int);
        return ref s_4[0][0];
    }

    static long M77(ulong[][] arg0, sbyte arg1, ushort[] arg2)
    {
        bool var0 = default(bool);
        if ((sbyte)(s_19-- % (long)(s_59[0]-- | 1)) == arg2[0])
        {
            arg0[0] = new ulong[]{1UL, 429455367443649211UL, 18446744073709551614UL, 2UL};
        }
        else
        {
            {
                try
                {
                    var0 = false;
                    s_rt.Checksum("c_347", var0);
                }
                finally
                {
                    arg2[0] = arg2[0]++;
                }
            }
        }

        s_rt.Checksum("c_348", arg0[0][0]);
        s_rt.Checksum("c_349", arg1);
        s_rt.Checksum("c_350", arg2[0]);
        return 1864377746738087952L;
    }

    static ulong[][] M78()
    {
        uint var0 = default(uint);
        var0 = 0U;
        s_rt.Checksum("c_351", var0);
        return new ulong[][]{new ulong[]{14512988967422436812UL, 11722771867974734503UL, 18446744073709551615UL, 12976961772911120469UL}, new ulong[]{1UL, 2503323721261690782UL, 1UL, 13142816938197938376UL, 18446744073709551614UL, 18446744073709551615UL}, new ulong[]{9389184152153229465UL, 1UL, 18446744073709551614UL}, new ulong[]{16760042309932406064UL, 0UL, 1UL}, new ulong[]{1UL, 1UL, 0UL, 1342838692465107UL, 1928393630231800427UL}, new ulong[]{11529851535409344551UL, 17612538283661160513UL, 0UL, 18446744073709551615UL, 1UL, 18446744073709551614UL, 3089073821720654974UL}, new ulong[]{1UL, 18446744073709551615UL, 16380860044031446442UL, 0UL, 116036276351870827UL, 11627908559790285408UL, 1UL}};
    }

    static int M79(sbyte arg0, sbyte[] arg1, uint arg2)
    {
        bool var0 = default(bool);
        long[] var1 = default(long[]);
        if (true & ((byte)(s_3[0]++ | arg2) != 65534))
        {
            arg2 = arg2++;
        }

        var0 = M80(-9223372036854775807L);
        arg1 = arg1;
        if (s_55[0])
        {
            var1 = new long[]{1L, 748189618062369328L, 1L, -9223372036854775808L};
            s_rt.Checksum("c_397", var1[0]);
        }

        s_rt.Checksum("c_398", arg0);
        s_rt.Checksum("c_399", arg1[0]);
        s_rt.Checksum("c_400", arg2);
        s_rt.Checksum("c_401", var0);
        return 1;
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
        uint var0 = default(uint);
        M83();
        s_18 *= (byte)M84();
        s_64[0] = 100;
        {
            s_62 = (byte)M84();
        }

        var0 = s_67--;
        var0 = var0--;
        var0 = var0;
        s_rt.Checksum("c_368", var0);
        return ref s_33;
    }

    static uint M83()
    {
        uint var0 = default(uint);
        if (s_46)
        {
            M84();
            var0 = 228150594U;
            var0 = var0;
            s_6 = 9223372036854775806L;
            var0 *= var0--;
            var0 = (uint)M84();
            var0 = var0;
            var0 = 2751273067U;
            s_rt.Checksum("c_367", var0);
        }
        else
        {
            s_67 ^= 1U;
        }

        return (uint)M84();
    }

    static int M84()
    {
        s_6 = 4960799877529508864L;
        s_36[0][0] = s_7;
        {
            s_25 = s_45[0, 0][0][0][0, 0]--;
        }

        return -459766720;
    }

    static bool M85(ref ulong arg0)
    {
        arg0 = 7283545859550719420UL;
        s_rt.Checksum("c_407", arg0);
        return s_55[0];
    }

    static short[][] M86(int arg0, ushort arg1, ulong arg2, long arg3, ref long arg4, sbyte[] arg5, uint arg6)
    {
        return default(short[][]);
    }

    static uint M87()
    {
        s_25 = s_15++;
        s_45 = s_45;
        return 0U;
    }

    static uint M88()
    {
        sbyte var0 = default(sbyte);
        var0 = s_16[0, 0]++;
        s_rt.Checksum("c_460", var0);
        return 0U;
    }

    static ulong M89(int arg0)
    {
        bool var0 = default(bool);
        var0 = false;
        s_rt.Checksum("c_513", arg0);
        s_rt.Checksum("c_514", var0);
        return s_57;
    }

    static bool[] M90(ref uint arg0, ref sbyte arg1, ref ushort arg2)
    {
        long var0 = default(long);
        var0 = s_77--;
        s_rt.Checksum("c_517", arg0);
        s_rt.Checksum("c_518", arg1);
        s_rt.Checksum("c_519", arg2);
        s_rt.Checksum("c_520", var0);
        return new bool[]{false, false, true};
    }

    static short M91()
    {
        M92(254);
        return (short)(0U / (ulong)(s_86[0][0][0][0] | 1));
    }

    static uint M92(byte arg0)
    {
        arg0 = arg0;
        s_rt.Checksum("c_573", arg0);
        return 0U;
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
        int var0 = default(int);
        if (s_61)
        {
            s_72 = s_72;
            s_11 = s_44;
        }

        var0 = s_12;
        s_rt.Checksum("c_578", var0);
        return s_14[0]++;
    }

    static int M96(short arg0)
    {
        ulong[] var0 = default(ulong[]);
        var0 = new ulong[]{0UL};
        {
            M97();
        }

        s_rt.Checksum("c_674", arg0);
        s_rt.Checksum("c_675", var0[0]);
        return M97();
    }

    static int M97()
    {
        return default(int);
    }

    static ulong[] M98()
    {
        ulong var0 = default(ulong);
        M99(s_95--);
        s_87 = true;
        try
        {
            var0 = s_90;
            s_rt.Checksum("c_664", var0);
        }
        finally
        {
            if (s_15-- < 0)
            {
                ref ushort var1 = ref s_71;
                if (s_46)
                {
                    var1 = var1;
                }

                if ((int)M99((long)((uint)(s_1[0]-- + -10) / (uint)(s_13[0, 0] | 1))) > (byte)M99((long)(188 & s_90)))
                {
                    s_1[0] = (uint)M100();
                    s_60 = s_52[0];
                }
                else
                {
                    var1 = 7267;
                }

                if (s_94)
                {
                    var1 = s_88[0]--;
                }

                s_rt.Checksum("c_665", var1);
            }
        }

        s_8 = -25756;
        s_31 = s_38[0][0];
        M100();
        s_123 = s_52[0]++;
        return new ulong[]{1UL, 0UL, 10445110140036976039UL};
    }

    static long M99(long arg0)
    {
        return default(long);
    }

    static short M100()
    {
        sbyte[] var0 = default(sbyte[]);
        var0 = s_78;
        s_rt.Checksum("c_650", var0[0]);
        return 32766;
    }

    static uint M101()
    {
        s_46 = true;
        s_38[0][0] = -2147483647;
        s_92 = s_66++;
        M102();
        return M102();
    }

    static ref uint M102()
    {
        s_106 = new int[][]{new int[]{0, -454528488, 1198159803, -1496100400, 2065113707}, new int[]{-789304628, 670084262, 10, -441003482}, new int[]{-2, -2147483647, -1995700422, -545423404, 68309670, -989641503}, new int[]{1749482438, 621842627, 1011835206, -1252759882}, new int[]{1974176442, -931646052, -887091786, 1762004697}, new int[]{2147483647, 837780491, 1, 0, -440306634, -655686759}, new int[]{-1, 1, 285811154, 1, 10, 560401466}};
        return ref s_1[0];
    }
}