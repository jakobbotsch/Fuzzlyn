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
        s_rt.Checksum("c_682", s_1[0]);
        s_rt.Checksum("c_683", s_2[0]);
        s_rt.Checksum("c_684", s_3[0]);
        s_rt.Checksum("c_685", s_4[0][0]);
        s_rt.Checksum("c_686", s_5);
        s_rt.Checksum("c_687", s_6);
        s_rt.Checksum("c_688", s_7);
        s_rt.Checksum("c_689", s_8);
        s_rt.Checksum("c_690", s_9[0]);
        s_rt.Checksum("c_691", s_10[0]);
        s_rt.Checksum("c_692", s_11);
        s_rt.Checksum("c_693", s_12);
        s_rt.Checksum("c_694", s_13[0, 0]);
        s_rt.Checksum("c_695", s_14[0]);
        s_rt.Checksum("c_696", s_15);
        s_rt.Checksum("c_697", s_16[0, 0]);
        s_rt.Checksum("c_698", s_17);
        s_rt.Checksum("c_699", s_18);
        s_rt.Checksum("c_700", s_19);
        s_rt.Checksum("c_701", s_20[0]);
        s_rt.Checksum("c_702", s_21);
        s_rt.Checksum("c_703", s_22[0][0]);
        s_rt.Checksum("c_704", s_23[0]);
        s_rt.Checksum("c_705", s_24);
        s_rt.Checksum("c_706", s_25);
        s_rt.Checksum("c_707", s_26);
        s_rt.Checksum("c_708", s_27);
        s_rt.Checksum("c_709", s_28);
        s_rt.Checksum("c_710", s_29);
        s_rt.Checksum("c_711", s_30[0]);
        s_rt.Checksum("c_712", s_31);
        s_rt.Checksum("c_713", s_32[0, 0, 0]);
        s_rt.Checksum("c_714", s_33);
        s_rt.Checksum("c_715", s_34);
        s_rt.Checksum("c_716", s_35);
        s_rt.Checksum("c_717", s_36[0][0]);
        s_rt.Checksum("c_718", s_37);
        s_rt.Checksum("c_719", s_38[0][0]);
        s_rt.Checksum("c_720", s_39);
        s_rt.Checksum("c_721", s_40[0][0, 0]);
        s_rt.Checksum("c_722", s_41);
        s_rt.Checksum("c_723", s_42);
        s_rt.Checksum("c_724", s_43);
        s_rt.Checksum("c_725", s_44);
        s_rt.Checksum("c_726", s_45[0, 0][0][0][0, 0]);
        s_rt.Checksum("c_727", s_46);
        s_rt.Checksum("c_728", s_47);
        s_rt.Checksum("c_729", s_48);
        s_rt.Checksum("c_730", s_49);
        s_rt.Checksum("c_731", s_50[0][0]);
        s_rt.Checksum("c_732", s_51);
        s_rt.Checksum("c_733", s_52[0]);
        s_rt.Checksum("c_734", s_53[0, 0, 0]);
        s_rt.Checksum("c_735", s_54[0]);
        s_rt.Checksum("c_736", s_55[0]);
        s_rt.Checksum("c_737", s_56);
        s_rt.Checksum("c_738", s_57);
        s_rt.Checksum("c_739", s_58);
        s_rt.Checksum("c_740", s_59[0]);
        s_rt.Checksum("c_741", s_60);
        s_rt.Checksum("c_742", s_61);
        s_rt.Checksum("c_743", s_62);
        s_rt.Checksum("c_744", s_63);
        s_rt.Checksum("c_745", s_64[0]);
        s_rt.Checksum("c_746", s_65);
        s_rt.Checksum("c_747", s_66);
        s_rt.Checksum("c_748", s_67);
        s_rt.Checksum("c_749", s_68);
        s_rt.Checksum("c_750", s_69);
        s_rt.Checksum("c_751", s_70);
        s_rt.Checksum("c_752", s_71);
        s_rt.Checksum("c_753", s_72[0]);
        s_rt.Checksum("c_754", s_73);
        s_rt.Checksum("c_755", s_74);
        s_rt.Checksum("c_756", s_75[0]);
        s_rt.Checksum("c_757", s_76);
        s_rt.Checksum("c_758", s_77);
        s_rt.Checksum("c_759", s_78[0]);
        s_rt.Checksum("c_760", s_79);
        s_rt.Checksum("c_761", s_80);
        s_rt.Checksum("c_762", s_81);
        s_rt.Checksum("c_763", s_82);
        s_rt.Checksum("c_764", s_83[0, 0]);
        s_rt.Checksum("c_765", s_84[0]);
        s_rt.Checksum("c_766", s_85[0]);
        s_rt.Checksum("c_767", s_86[0][0][0][0]);
        s_rt.Checksum("c_768", s_87);
        s_rt.Checksum("c_769", s_88[0]);
        s_rt.Checksum("c_770", s_89);
        s_rt.Checksum("c_771", s_90);
        s_rt.Checksum("c_772", s_91);
        s_rt.Checksum("c_773", s_92);
        s_rt.Checksum("c_774", s_93);
        s_rt.Checksum("c_775", s_94);
        s_rt.Checksum("c_776", s_95);
        s_rt.Checksum("c_777", s_96);
        s_rt.Checksum("c_778", s_97[0]);
        s_rt.Checksum("c_779", s_98[0, 0]);
        s_rt.Checksum("c_780", s_99);
        s_rt.Checksum("c_781", s_100[0]);
        s_rt.Checksum("c_782", s_101[0, 0]);
        s_rt.Checksum("c_783", s_102);
        s_rt.Checksum("c_784", s_103);
        s_rt.Checksum("c_785", s_104);
        s_rt.Checksum("c_786", s_105);
        s_rt.Checksum("c_787", s_106[0][0]);
        s_rt.Checksum("c_788", s_107);
        s_rt.Checksum("c_789", s_108);
        s_rt.Checksum("c_790", s_109);
        s_rt.Checksum("c_791", s_110);
        s_rt.Checksum("c_792", s_111);
        s_rt.Checksum("c_793", s_112);
        s_rt.Checksum("c_794", s_113);
        s_rt.Checksum("c_795", s_114);
        s_rt.Checksum("c_796", s_115[0]);
        s_rt.Checksum("c_797", s_116);
        s_rt.Checksum("c_798", s_117[0][0]);
        s_rt.Checksum("c_799", s_118);
        s_rt.Checksum("c_800", s_119);
        s_rt.Checksum("c_801", s_120[0][0]);
        s_rt.Checksum("c_802", s_121[0][0][0]);
        s_rt.Checksum("c_803", s_122);
        s_rt.Checksum("c_804", s_123);
        s_rt.Checksum("c_805", s_124);
    }

    static void M0()
    {
        byte var0 = (byte)(-1592527126 | (byte)(1 % (sbyte)((sbyte)(1 - (int)(0UL * (uint)(0 % (ushort)((ushort)(0 % (int)((int)(M1(2, 880340794U, (byte)(490360998U - (long)((ulong)(s_113++ - (int)M72()) + (byte)M36())), 10L, ref s_117, ref s_9[0], ref s_26, 37, ref s_60, s_47, ref s_78, (byte)M81(), s_77++) + (sbyte)M3((ushort)M18(), ref s_70, (short)(s_108 / (long)(-6951955190531076674L | 1)), -2057119971763330565L)) | 1)) | 1)))) | 1)));
        if (M66())
        {
            var0 = 38;
            {
                s_86[0][0][0][0] = 16164789200425507777UL;
                M12(new sbyte[, ]{{0, -1, -128, 11}, {-127, 69, 1, 0}, {28, -1, 0, 34}, {0, 121, 122, -32}, {127, 2, 111, 2}, {0, -28, 1, 126}, {100, -127, 1, -128}, {-105, 0, 1, 127}, {-10, 1, -22, -110}, {-128, 73, -118, 0}}, var0, s_97[0]--);
                M63() = -2L;
                M20() = new uint[, ]{{1U, 0U, 4294967295U, 0U}, {1U, 3958668575U, 4294967294U, 1U}, {1201915523U, 0U, 1838647066U, 1007366462U}, {4294967294U, 1U, 4294967295U, 0U}, {2891956976U, 2155597238U, 1U, 3212708334U}};
                ref bool var1 = ref s_89;
                var1 &= M41(0);
                var0 = (byte)(s_68 + 10);
                {
                    var1 = var1;
                    if (s_101[0, 0])
                    {
                        ushort[, ] var2 = new ushort[, ]{{29529, 29730, 1}, {1944, 65534, 10}};
                        s_rt.Checksum("c_644", var2[0, 0]);
                    }
                    else
                    {
                        s_37 = 1158118867;
                        var0 = 126;
                        ref byte var3 = ref var0;
                        s_rt.Checksum("c_645", var3);
                    }

                    {
                        try
                        {
                            M2(ref s_22);
                        }
                        finally
                        {
                            var1 &= var1;
                        }

                        ref bool var4 = ref var1;
                        s_17 = (ushort)M44();
                        s_rt.Checksum("c_646", var4);
                    }

                    s_118 = M41(s_112--);
                    var0 = s_45[0, 0][0][0][0, 0];
                }

                ushort[] var5 = new ushort[]{15847, 7147, 9471, 24513, 65534};
                s_rt.Checksum("c_647", var1);
                s_rt.Checksum("c_648", var5[0]);
            }

            s_18 = var0;
            M27();
            M96(32767);
            byte var6 = 83;
            if (s_118)
            {
                M39((ulong)M13((byte)M46(new ushort[]{53798, 6343, 51158, 58865}, M101()), ref M102(), s_106[0]), (uint)M70());
                M33(ref s_4);
                short var7 = s_112++;
                uint var8 = s_124;
                byte var9 = (byte)(s_84[0] | (uint)M32(164));
                s_rt.Checksum("c_676", var7);
                s_rt.Checksum("c_677", var8);
                s_rt.Checksum("c_678", var9);
            }

            s_rt.Checksum("c_679", var6);
        }
        else
        {
            var0 = 1;
            if (s_23[0])
            {
                var0 = var0;
            }
        }

        uint var10 = s_67;
        if (1U == s_70)
        {
            var10 = var10;
            M98();
        }
        else
        {
            var10 = var10;
            var0 %= (byte)(var0 | 1);
            {
                var10 = 1U;
            }
        }

        s_rt.Checksum("c_680", var0);
        s_rt.Checksum("c_681", var10);
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
        arg1 = 11318137128244086683UL;
        if (M4(ref s_98))
        {
            if (s_94)
            {
                try
                {
                    s_113 = (short)M83();
                }
                finally
                {
                    uint var0 = s_81--;
                    var0 = 1U;
                    M42();
                    s_rt.Checksum("c_623", var0);
                }

                s_11 = arg3;
                M74(ref s_9[0]);
            }
        }

        s_114 = 0;
        if (M38(-10, arg0))
        {
            short var1 = s_19--;
            s_115 = new short[]{-2, -3486, 663, 0, 2, -2, 31317, -10};
            s_rt.Checksum("c_624", var1);
        }
        else
        {
            M83();
            arg1 = arg1;
            int var2 = s_20[0] & arg0;
            M69(s_26--, (int)M61(), arg1, ref s_96, s_20[0]--, M26(), arg3) = s_46;
            s_rt.Checksum("c_625", var2);
        }

        s_116 = 9223372036854775807L;
        arg3 = arg3;
        s_rt.Checksum("c_626", arg0);
        s_rt.Checksum("c_627", arg1);
        s_rt.Checksum("c_628", arg2);
        s_rt.Checksum("c_629", arg3);
        return -2147483647;
    }

    static bool M4(ref ulong[, ] arg0)
    {
        {
            arg0[0, 0] = 132693170054025997UL;
            if (arg0[0, 0] > 0)
            {
                M5((ushort)(55 * (short)(1 ^ s_1[0])), 9223372036854775807L, new long[]{8661840434811268092L, -8465449770485648124L, -1814241165019188175L, 6132373567192031852L, 9223372036854775807L, -9223372036854775808L});
            }

            arg0 = ref arg0;
            M5((ushort)(0 % (short)((short)(145 | (byte)(0 % (sbyte)(s_2[0] | 1))) | 1)), 6641404611578949758L, new long[]{-8916367911469285972L, -685134847234331051L, 1L, -9223372036854775807L, -3121990891763617900L, 0L, 0L, 0L, 1L});
            try
            {
                if (M6(true, s_3[0]++, (long)(1UL & (byte)(0U * (short)(M9(ref s_48) & M52(s_27++, s_50[0][0])))), ref s_50[0][0], 126, s_25++))
                {
                    sbyte var0 = s_26;
                    M8();
                    arg0 = new ulong[, ]{{1UL, 15797075046127076784UL, 13147378918255787610UL, 0UL, 0UL, 11316617526271234261UL}, {6873163290184261498UL, 1UL, 6712732914444017550UL, 0UL, 1UL, 1UL}, {1093755499519010400UL, 5279327011684114299UL, 0UL, 2269515745318399257UL, 1UL, 15202922821037993063UL}};
                    int[] var1 = new int[]{-1521369650, -2147483648, -883946343, 2147483647};
                    var1[0] = (int)M51(var1[0], 0U, ref s_39, 378748732, (uint)(M53(ref s_68) / (ushort)((ushort)M17(var0, ref M82(), M93(), s_70, s_83[0, 0], var0) | 1)));
                    M67(new ushort[,, ]{{{65535, 40919, 46441, 5409, 41151, 2006, 31776}, {22792, 62930, 30741, 0, 26653, 49464, 1}, {57783, 65535, 8539, 44095, 0, 47081, 8616}, {45008, 52244, 65534, 60664, 42451, 65402, 57384}, {0, 5666, 50074, 6021, 2125, 8594, 65534}, {1, 30618, 42891, 65534, 32678, 27097, 57653}, {1, 49735, 24844, 55875, 1, 3533, 0}}, {{1, 54943, 18021, 1, 22575, 43513, 3919}, {1, 65535, 4738, 48002, 40193, 65534, 44445}, {48734, 24345, 0, 1, 56038, 26406, 57850}, {0, 10014, 0, 29335, 10, 30475, 65534}, {26600, 38606, 65212, 0, 65535, 1, 16647}, {0, 1, 65535, 10, 48527, 0, 9213}, {63506, 0, 17607, 40064, 1, 43630, 61858}}, {{2, 33713, 8032, 9610, 36283, 65535, 10}, {2324, 1, 1, 26130, 2, 46125, 12210}, {38275, 42953, 1, 16099, 38419, 8402, 0}, {0, 47189, 18135, 1, 1, 47303, 15943}, {8034, 41189, 19393, 1, 1, 13295, 0}, {1, 43989, 1410, 54861, 50309, 2, 17867}, {39557, 50584, 6577, 13110, 0, 0, 3822}}, {{41485, 34996, 10, 0, 18024, 46578, 2}, {15392, 0, 4510, 1, 56225, 4359, 53975}, {1, 14820, 9839, 0, 4607, 15971, 58523}, {1397, 65534, 24817, 35824, 19795, 65534, 1}, {0, 10, 20322, 2, 0, 0, 46024}, {3121, 14097, 0, 1, 65535, 65535, 62429}, {0, 65535, 1533, 49228, 55060, 1, 0}}, {{0, 11688, 10077, 55511, 1, 61494, 1}, {1060, 30453, 10, 10, 0, 1, 5707}, {1, 1, 1, 25733, 23806, 10443, 44440}, {9204, 36770, 65534, 32465, 10, 0, 60152}, {36128, 0, 0, 2, 29525, 62736, 47990}, {10, 10634, 20216, 65535, 30885, 27695, 0}, {28967, 63754, 1, 7060, 65534, 0, 0}}});
                    var1[0]--;
                    try
                    {
                        M72();
                        M14();
                    }
                    finally
                    {
                        arg0 = arg0;
                        int var2 = var1[0];
                        s_108 += s_21;
                        int var3 = var1[0];
                        M20();
                        s_rt.Checksum("c_602", var2);
                        s_rt.Checksum("c_603", var3);
                    }

                    sbyte var4 = var0;
                    ulong var5 = s_10[0];
                    bool[] var6 = new bool[]{true, false, false, false, true, false};
                    if (M62())
                    {
                        long var7 = (long)M89((int)M10(ref arg0[0, 0], ref s_89));
                        var1 = new int[]{1};
                        bool[] var8 = s_23;
                        {
                            var6[0] = M62();
                        }

                        ref byte var9 = ref s_15;
                        s_rt.Checksum("c_604", var7);
                        s_rt.Checksum("c_605", var8[0]);
                        s_rt.Checksum("c_606", var9);
                        return false;
                    }

                    s_59[0] = s_29--;
                    s_rt.Checksum("c_607", var0);
                    s_rt.Checksum("c_608", var1[0]);
                    s_rt.Checksum("c_609", var4);
                    s_rt.Checksum("c_610", var5);
                    s_rt.Checksum("c_611", var6[0]);
                }
                else
                {
                    uint var10 = s_33++;
                    int[] var11 = new int[]{945691856, 0, 1094709766, -535670382, -1432605493, 1363072048};
                    s_rt.Checksum("c_612", var10);
                    s_rt.Checksum("c_613", var11[0]);
                }
            }
            finally
            {
                s_16 = new sbyte[, ]{{68}, {-39}, {10}, {127}, {-81}, {-108}, {87}};
                short var12 = -30836;
                ushort var13 = s_17;
                M18();
                byte var14 = (byte)(5188465721317387458UL - s_34++);
                M76(ref var13, s_9, s_94) = 2147483647;
                s_rt.Checksum("c_614", var12);
                s_rt.Checksum("c_615", var13);
                s_rt.Checksum("c_616", var14);
            }

            if (s_104)
            {
                arg0 = arg0;
                uint var15 = (uint)M91();
                s_19 = s_54[0]--;
                s_109 = (byte)(arg0[0, 0] / (uint)(var15 | 1));
                arg0[0, 0] = arg0[0, 0];
                s_rt.Checksum("c_617", var15);
            }
            else
            {
                s_110 = M94();
            }

            M73((int)M64(), -128, ref s_2[0], M41(s_42), 6804781619317926953L, (uint)(32766 - s_2[0]++)) = new ulong[]{0UL, 5735822109356904137UL, 18446744073709551614UL, 8082015322326661631UL, 0UL, 3376069854299098080UL, 8401536039270433698UL, 11478142067773248618UL};
            if (s_48)
            {
                bool[][] var16 = new bool[][]{new bool[]{false}, new bool[]{true}, new bool[]{false}, new bool[]{true}, new bool[]{true}, new bool[]{true}};
                M36();
                s_rt.Checksum("c_618", var16[0][0]);
            }

            if (s_55[0])
            {
                arg0[0, 0] = (ulong)M23(1, 1263538730, (sbyte)((ushort)M88() ^ 1390681945), ref s_16, (long)M83(), s_17, (sbyte)M54(ref s_108, ref s_6), 2480524804U, new ushort[]{1, 55400, 0, 43811});
                {
                    arg0 = arg0;
                    s_111 = arg0[0, 0];
                }
            }

            try
            {
                arg0[0, 0] = arg0[0, 0];
            }
            finally
            {
                {
                    M42() = 0U;
                    s_112 = -1;
                    arg0[0, 0] = (ulong)M95();
                }
            }

            arg0 = new ulong[, ]{{2UL, 5987218777420309242UL, 7325982409298237866UL, 18446744073709551615UL, 14685111036573798746UL, 15623759982101058580UL}};
            M89(s_4[0][0]);
            if (M80(s_11--))
            {
                if (M80((long)M76(ref s_85[0], new sbyte[]{-32, 108, 0, 0, 2, -128, 0}, false)))
                {
                    sbyte[] var17 = new sbyte[]{-128};
                    s_rt.Checksum("c_619", var17[0]);
                    return true;
                }
                else
                {
                    short var18 = -10;
                    --var18;
                    arg0 = arg0;
                    M54(ref s_31, ref s_73);
                    s_rt.Checksum("c_620", var18);
                }
            }

            M31();
            sbyte var19 = s_26--;
            s_rt.Checksum("c_621", var19);
        }

        s_rt.Checksum("c_622", arg0[0, 0]);
        return M38((sbyte)M63(), 0);
    }

    static ref sbyte[] M5(ushort arg0, long arg1, long[] arg2)
    {
        arg0 = arg0;
        if (255 < (short)(arg0++ ^ (92 | (short)(arg1 * 69))))
        {
            long var0 = arg2[0]++;
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
        arg0 = arg0;
        arg1 = arg1;
        arg1 = arg1;
        arg5 = 134;
        arg3 = arg3;
        M7();
        if (true || ((short)(arg1 / (sbyte)(arg4 | 1)) <= -2147483647))
        {
            M7();
        }

        if (arg0)
        {
            arg2 = arg2;
            {
                if (arg0)
                {
                    M8();
                    M8();
                    long var0 = arg2;
                    s_2 = s_2;
                    short var1 = (short)(arg3 & -22027);
                    s_rt.Checksum("c_4", var0);
                    s_rt.Checksum("c_5", var1);
                }
                else
                {
                    s_2[0] = arg4++;
                    arg0 = arg0;
                }

                try
                {
                    ref uint var2 = ref s_1[0];
                    arg2 = 1L;
                    M7();
                    try
                    {
                        M7();
                    }
                    finally
                    {
                        uint var3 = var2++;
                        var3 = s_1[0];
                        s_rt.Checksum("c_6", var3);
                    }

                    s_rt.Checksum("c_7", var2);
                }
                finally
                {
                    arg0 ^= false;
                    s_1 = new uint[]{1491416518U, 1607520489U, 392317939U, 1162369618U, 2847565021U, 1U};
                    ref byte var4 = ref arg5;
                    byte var5 = var4;
                    var5 = 255;
                    arg0 = false;
                    {
                        s_3 = new ushort[]{13684, 0, 3998, 10, 65534};
                        if (arg0)
                        {
                            M7();
                            M8();
                            s_1 = s_1;
                        }
                        else
                        {
                            var4 = var5--;
                            M8();
                            ulong var6 = (ulong)M8();
                            s_rt.Checksum("c_8", var6);
                        }

                        try
                        {
                            ++var5;
                        }
                        finally
                        {
                            arg1 = arg3;
                        }

                        s_4 = new int[][]{new int[]{0, -1697498587}, new int[]{-319828437}, new int[]{1044304653, -916602562}, new int[]{1923920494, 2147483647}, new int[]{393385485}, new int[]{2034897491, 240571504}, new int[]{1407481744, 1}, new int[]{1}, new int[]{1, 1643935281}, new int[]{2147483646}};
                    }

                    s_rt.Checksum("c_9", var4);
                    s_rt.Checksum("c_10", var5);
                }
            }

            arg3 = s_3[0]--;
            if (arg0)
            {
                bool var7 = arg0;
                arg1 = arg3;
                s_rt.Checksum("c_11", var7);
            }
            else
            {
                if (arg0)
                {
                    arg2 = (long)(65535 - arg4++);
                    arg4 = -127;
                    arg0 = arg2 == -10;
                }

                arg1 = s_3[0];
                ref ulong var8 = ref s_5;
                if (arg0)
                {
                    M8();
                    bool var9 = true;
                    s_rt.Checksum("c_12", var9);
                }
                else
                {
                    uint var10 = 0U;
                    short var11 = (short)(s_4[0][0]-- / (int)(s_4[0][0]-- | 1));
                    s_1 = new uint[]{2489492800U, 3602523092U, 2172169016U, 3648157679U, 1U, 534085079U, 1211647533U, 4158260100U, 3514073192U};
                    var8 = var8;
                    short[] var12 = new short[]{2};
                    arg3 = arg3;
                    arg5 = arg5;
                    s_rt.Checksum("c_13", var10);
                    s_rt.Checksum("c_14", var11);
                    s_rt.Checksum("c_15", var12[0]);
                }

                s_rt.Checksum("c_16", var8);
            }

            arg4 = 67;
            arg4 = s_2[0];
            s_4 = new int[][]{new int[]{1, 2147483647}};
        }

        arg1 = arg1;
        s_rt.Checksum("c_17", arg0);
        s_rt.Checksum("c_18", arg1);
        s_rt.Checksum("c_19", arg2);
        s_rt.Checksum("c_20", arg3);
        s_rt.Checksum("c_21", arg4);
        s_rt.Checksum("c_22", arg5);
        return arg0;
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
        if (-44 >= s_1[0])
        {
            {
                M10(ref s_10[0], ref s_39);
                M10(ref s_41, ref arg0);
                if (arg0)
                {
                    M16();
                    arg0 = ref arg0;
                    byte[] var0 = new byte[]{255};
                    s_13[0, 0] = (uint)M18();
                    s_rt.Checksum("c_275", var0[0]);
                }

                arg0 = (ulong)((s_33 ^ s_43--) / (byte)(s_18-- | 1)) <= 10198763656840531723UL;
                arg0 = M26();
                M33(ref s_4) = 5102062864750846522L;
                {
                    return -4650898483829189856L;
                }

                M18();
                if (arg0)
                {
                    arg0 = true;
                }
                else
                {
                    arg0 = M41(s_42--);
                    bool var1 = arg0;
                    if (M26())
                    {
                        ref long var2 = ref s_11;
                        s_rt.Checksum("c_276", var2);
                    }

                    s_rt.Checksum("c_277", var1);
                }
            }
        }

        byte var3 = 165;
        uint var4 = s_22[0][0];
        byte var5 = 186;
        M51(s_37++, var4, ref arg0, s_37++, M42()++);
        var5 = var5;
        var4 = var4;
        try
        {
            try
            {
                bool[, ] var6 = new bool[, ]{{true, true, false, false, true, false, true, false, true}, {false, false, true, false, true, false, true, true, false}, {true, true, false, false, false, false, true, false, false}};
                s_rt.Checksum("c_278", var6[0, 0]);
            }
            finally
            {
                s_23 = new bool[]{false, true, false, false, false, false, true};
            }

            try
            {
                var5 /= (byte)(1 | 1);
            }
            finally
            {
                M33(ref s_4);
            }

            M30();
            uint var7 = var4--;
            M17((sbyte)(-2 & s_4[0][0]), ref var4, -22908, 10973034589174924987UL, (int)M29(), (sbyte)((long)M46(new ushort[]{42582, 435, 23750, 1, 60466, 1}, var7) | 9223372036854775806L));
            if (arg0 | s_36[0][0])
            {
                uint[] var8 = new uint[]{1762967682U, 1666038855U, 1U, 2262761083U, 4109095242U, 1006329062U, 0U, 2299140818U, 667321214U, 2852557486U};
                var3 = (byte)M45(s_32[0, 0, 0]++, s_17, ref arg0, -2147483648, new short[]{-15234, -6425, 21879, 31004, 32766});
                {
                    if (arg0)
                    {
                        var8[0] = var7;
                        byte[] var9 = new byte[]{254, 108, 120, 32, 147, 255, 143};
                        s_rt.Checksum("c_279", var9[0]);
                    }
                    else
                    {
                        {
                            var7 = var4;
                            var3 = 254;
                            var5 = 0;
                            M15();
                            sbyte var10 = s_2[0]--;
                            M34(new long[][]{new long[]{-9223372036854775808L}, new long[]{8656904023889073621L}, new long[]{9223372036854775807L, -10L}, new long[]{-9223372036854775808L}}) = s_32;
                            var7 = s_1[0];
                            s_rt.Checksum("c_280", var10);
                        }

                        var3 = var5;
                        s_47--;
                        var5 = (byte)M11(var8);
                        M29();
                    }

                    ulong[] var11 = new ulong[]{10UL, 9767901163662185374UL, 0UL, 15977936205797865757UL, 2UL};
                    sbyte var12 = -2;
                    var11[0] = var11[0];
                    ref sbyte var13 = ref s_26;
                    s_48 = arg0;
                    s_rt.Checksum("c_281", var11[0]);
                    s_rt.Checksum("c_282", var12);
                    s_rt.Checksum("c_283", var13);
                }

                s_rt.Checksum("c_284", var8[0]);
            }

            if (arg0 ^ arg0)
            {
                M35(new ushort[][]{new ushort[]{10, 65534, 10815, 20528}, new ushort[]{32892, 1, 0, 65535, 0}, new ushort[]{24361, 2, 0, 65534}, new ushort[]{20833}, new ushort[]{0, 65535, 1, 39058}, new ushort[]{1, 4694, 11638, 6758}, new ushort[]{15309, 4574, 24076, 0, 16679}}, ref s_5, ref s_14[0]);
                arg0 = s_48;
                var4 = var4;
            }

            int var14 = (int)(M30() ^ 1246872860);
            var3 = 0;
            short var15 = (short)M49(s_3, 1, (short)(2300925610046489953UL + (uint)M17(92, ref var4, 0, 826631402900473900UL, s_37, s_9[0]--)));
            short[] var16 = new short[]{-31155, 0, 32767, -9802, -32767};
            if (arg0)
            {
                {
                    var16[0] = var16[0];
                    uint var17 = (uint)M33(ref s_4);
                    M51(var14, var17, ref s_36[0][0], s_31, s_13[0, 0]);
                    if (arg0)
                    {
                        var3 = var3;
                        var7 = 0U;
                        var5 = var3;
                        M22();
                        s_49 ^= (byte)(var16[0] + -7);
                    }

                    {
                        short var18 = (short)M13(var5, ref var4, new int[]{-836798278, 1668319188, -2147483647, 763803149, 538626178, -1549306013, -2147483647, -2147483647});
                        byte[] var19 = new byte[]{43, 254, 1};
                        var18 &= (short)((uint)((long)M32(s_49) / (long)((long)M46(new ushort[]{65535, 12039}, s_33) | 1)) / (ulong)(s_5 | 1));
                        var19[0] = s_20[0];
                        s_50 = new ushort[][]{new ushort[]{0, 28154}};
                        var19 = var19;
                        s_rt.Checksum("c_285", var18);
                        s_rt.Checksum("c_286", var19[0]);
                        return s_11++;
                    }

                    var16[0] = s_24;
                    var15 = -27130;
                    s_rt.Checksum("c_287", var17);
                }
            }
            else
            {
                s_10 = new ulong[]{2UL, 11532361435674516821UL, 14297266301542107221UL};
            }

            s_rt.Checksum("c_288", var7);
            s_rt.Checksum("c_289", var14);
            s_rt.Checksum("c_290", var15);
            s_rt.Checksum("c_291", var16[0]);
        }
        finally
        {
            ushort var20 = s_3[0];
            var20 = 19099;
            s_rt.Checksum("c_292", var20);
        }

        s_51 = s_5;
        s_rt.Checksum("c_293", arg0);
        s_rt.Checksum("c_294", var3);
        s_rt.Checksum("c_295", var4);
        s_rt.Checksum("c_296", var5);
        return s_40[0][0, 0]--;
    }

    static long M10(ref ulong arg0, ref bool arg1)
    {
        if (arg1)
        {
            arg1 = arg1;
            if (arg1)
            {
                arg0 = s_5;
                arg0 = 15022371188452047616UL;
                if (arg1)
                {
                    uint var0 = s_1[0]--;
                    var0 = 0U;
                    arg0 = 18446744073709551615UL;
                    s_rt.Checksum("c_23", var0);
                }
            }

            arg1 = arg1;
            arg0 = arg0;
        }
        else
        {
            M11(M31());
        }

        if (arg1)
        {
            M29();
            s_46 = s_39;
            uint var1 = s_13[0, 0]--;
            if (arg1)
            {
                try
                {
                    ref uint var2 = ref var1;
                    s_rt.Checksum("c_267", var2);
                }
                finally
                {
                    uint[][][] var3 = new uint[][][]{new uint[][]{new uint[]{3713255602U, 387794647U}, new uint[]{4294967295U}, new uint[]{0U, 2977160725U}, new uint[]{1U}, new uint[]{4294967295U, 1326424842U}, new uint[]{0U}}, new uint[][]{new uint[]{1676776290U}, new uint[]{2U, 4294967294U}, new uint[]{4294967294U}, new uint[]{0U}, new uint[]{4294967294U, 1810062630U}, new uint[]{0U, 2418284113U}, new uint[]{1U}, new uint[]{1897022280U}}, new uint[][]{new uint[]{3790895793U, 3430317561U}, new uint[]{0U, 1U}, new uint[]{3929424796U, 1U}, new uint[]{2730894256U, 2634379707U}}, new uint[][]{new uint[]{3099130890U, 1U}}, new uint[][]{new uint[]{3984281740U}, new uint[]{277306780U}, new uint[]{2502089079U}}};
                    s_rt.Checksum("c_268", var3[0][0][0]);
                }

                ushort var4 = 1457;
                s_rt.Checksum("c_269", var4);
            }
            else
            {
                try
                {
                    s_16[0, 0] = s_2[0];
                    arg1 = arg1;
                }
                finally
                {
                    ushort var5 = (ushort)M36();
                    s_rt.Checksum("c_270", var5);
                }
            }

            var1 = 2170762871U;
            s_23[0] = arg1;
            s_rt.Checksum("c_271", var1);
        }

        arg0 = ref arg0;
        ref int[][] var6 = ref s_4;
        s_rt.Checksum("c_272", arg0);
        s_rt.Checksum("c_273", arg1);
        s_rt.Checksum("c_274", var6[0][0]);
        return s_27;
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
        {
            try
            {
                try
                {
                    if (s_4[0][0] != 1)
                    {
                        sbyte var0 = arg0[0, 0];
                        arg0[0, 0] = s_2[0];
                        arg0 = arg0;
                        short var1 = -32767;
                        arg0[0, 0] = -1;
                        sbyte[] var2 = new sbyte[]{126, 12, 70, 17, 94, -127, -128, -76, 127};
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
                            sbyte var3 = arg0[0, 0];
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
            long var5 = (long)(1 / (short)((short)M13(arg1--, ref s_1[0], new int[]{2044349693, -2147483647, 1, -2147483648, -10, 1350566660, -71041840, 0, 450850015}) | 1));
            s_rt.Checksum("c_35", var5);
        }

        if (s_7)
        {
            ushort var6 = (ushort)M13(arg1, ref s_1[0], new int[]{-2147483647, -1502575172, -85402593, -2071209967, -2000332689, 767121773, 1418662593, -2147483647, -1162568171, -2147483648});
            s_rt.Checksum("c_36", var6);
        }
        else
        {
            int var7 = 2147483647;
            arg2 = (short)(51431005U | (short)M13(arg1, ref s_1[0], new int[]{751191425, 1, -2147483647, -2003659586, 0, -595842771}));
            M13(arg1, ref s_1[0], new int[]{-1111734879, 2147483646, 923341667, 82077352, 0, 2147483646});
            arg1 = 10;
            short var8 = arg2;
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
                    byte var9 = 154;
                    var9 /= (byte)(224 | 1);
                    int var10 = var7;
                    var10 = var10;
                    s_rt.Checksum("c_37", var9);
                    s_rt.Checksum("c_38", var10);
                }

                uint var11 = s_1[0];
                if ((ulong)M13(arg1, ref s_1[0], new int[]{469759899}) <= var11++)
                {
                    s_8 = var8;
                    M13(1, ref s_1[0], s_4[0]);
                    byte var12 = 26;
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
                bool var13 = s_36[0][0];
                arg2 = var8;
                arg0[0, 0] += arg0[0, 0];
                uint var14 = s_13[0, 0]++ + s_34++;
                if (M26())
                {
                    M42();
                    s_44 = 0L;
                    var8 = -2;
                    ulong var15 = s_5;
                    arg0 = arg0;
                    ref byte[, ][][][, ] var16 = ref s_45;
                    {
                        ulong var17 = (ulong)M27();
                        bool var18 = var13;
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
                        int var2 = arg2[0];
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
        if (s_4[0][0] == s_6)
        {
            s_3 = new ushort[]{4870};
            uint var0 = 1U;
            bool var1 = s_8 <= 50;
            s_rt.Checksum("c_41", var0);
            s_rt.Checksum("c_42", var1);
        }

        s_3 = new ushort[]{41893, 23091, 1, 39520, 3931, 65535, 0, 1, 1};
        if (false || s_7)
        {
            bool var2 = false;
            M15();
            s_32 = M34(new long[][]{new long[]{-6975931450108514578L}, new long[]{-8055387808231810575L}, new long[]{-7401758982966498344L}, new long[]{10L}, new long[]{0L}, new long[]{9223372036854775806L}, new long[]{-2035431844826559151L}, new long[]{-6799940605471072472L}, new long[]{1L}, new long[]{9223372036854775806L}});
            {
                ref long var3 = ref s_14[0];
                {
                    var2 = var2;
                    M50(ref s_21);
                }

                M49(s_3, -1886091507, -11756);
                var2 = var2;
                s_rt.Checksum("c_248", var3);
            }

            s_rt.Checksum("c_249", var2);
        }

        if (s_36[0][0])
        {
            uint var4 = M42()--;
            if (s_39)
            {
                var4 = var4;
            }

            {
                bool var5 = true;
                var4 = s_22[0][0]--;
                M35(new ushort[][]{new ushort[]{18665, 39229}, new ushort[]{7121, 32876, 65534}}, ref s_41, ref s_29);
                var4 = 3715985311U;
                s_rt.Checksum("c_250", var5);
            }

            s_rt.Checksum("c_251", var4);
        }
        else
        {
            long var6 = s_6;
            M20() = new uint[, ]{{1897112581U, 85962728U, 2556839154U, 60308014U, 1U, 3529380856U, 320732545U, 0U}, {786968350U, 1304706343U, 0U, 1526233700U, 1166370605U, 3640455025U, 4294967294U, 2095698729U}, {0U, 3211284173U, 10U, 4294967295U, 1U, 284916076U, 810721382U, 1U}, {1057285143U, 1773615746U, 1U, 2030061776U, 1U, 3036272597U, 556554537U, 4028966114U}, {2070831039U, 3005348273U, 4294967295U, 544181224U, 1U, 462205335U, 3960851339U, 4294967294U}, {318159490U, 1838087412U, 1U, 0U, 0U, 0U, 2U, 1U}, {1U, 1854446599U, 3049126165U, 4294967295U, 1800868750U, 0U, 4173649580U, 4189285782U}, {1U, 4294967294U, 3555752390U, 0U, 2624048407U, 4294967294U, 4294967295U, 0U}, {2365286597U, 4294967294U, 2364224769U, 2850958888U, 4016224566U, 4032275778U, 4294967295U, 3239668001U}, {4294967295U, 68170570U, 2237939033U, 355596096U, 0U, 0U, 3765930728U, 4294967294U}};
            ushort var7 = s_17;
            var7 = var7++;
            var6 = s_35;
            s_rt.Checksum("c_252", var6);
            s_rt.Checksum("c_253", var7);
        }

        s_38[0][0] = s_31--;
        --s_43;
        s_16 = new sbyte[, ]{{-10, 6, 0, -127, -98, 126, -2, 88}, {14, -127, 1, 1, -127, 39, 0, 1}, {10, -127, -5, -71, 127, 126, 126, -32}, {-128, 34, -104, -1, 44, -127, -36, 43}};
        return s_36[0][0];
    }

    static bool[] M15()
    {
        s_10 = new ulong[]{18118214526235327036UL, 16648714977630228080UL};
        if (s_7)
        {
            s_3[0] = s_3[0]++;
            if (0UL >= s_1[0]--)
            {
                return new bool[]{true, true};
            }

            s_9 = new sbyte[]{74, -127, -54, -57, -128, 1, 2};
            sbyte var0 = -108;
            try
            {
                if (s_7)
                {
                    long var1 = s_6;
                    s_rt.Checksum("c_43", var1);
                }
            }
            finally
            {
                var0 = s_9[0];
            }

            var0 = s_2[0];
            s_4 = new int[][]{new int[]{-2, -1380330483, 0, 0, 1105410740, -475266803, 0}, new int[]{-751555279, -912663631}, new int[]{0, -1245607988, 1713292993, 2147483646}, new int[]{324083798, -10, -2, 0, -668760947, 2147483646, 1556782561, 0}, new int[]{147348701, 234024220, -1903164296, 24845747, 2147483647}, new int[]{-2147483647}};
            var0 %= (sbyte)((sbyte)(s_1[0] / (ushort)(0 | 1)) | 1);
            M16();
            ulong var2 = 18446744073709551615UL;
            if (s_39)
            {
                try
                {
                    if ((short)M33(ref s_38) >= s_38[0][0]--)
                    {
                        var0 |= var0;
                        M50(ref s_31);
                        var2 = var2;
                        s_43 = 3985342756U;
                        return new bool[]{false};
                    }
                }
                finally
                {
                    M40(s_28, -1821084591);
                    ulong var3 = var2;
                    s_rt.Checksum("c_244", var3);
                }

                ref long[] var4 = ref s_14;
                var0 = var0;
                var2 = 1860072935169368681UL;
                s_rt.Checksum("c_245", var4[0]);
            }

            s_rt.Checksum("c_246", var0);
            s_rt.Checksum("c_247", var2);
        }
        else
        {
            s_16 = s_16;
            if (0 != (ushort)M32(s_20[0]))
            {
                s_33 = 3124224671U;
                {
                    s_42 = 32767;
                }
            }
            else
            {
                s_37 = s_37;
            }

            s_21 = s_38[0][0]--;
            M24();
            s_38 = M25();
        }

        return new bool[]{true, true, false, false, true, true, true};
    }

    static ulong M16()
    {
        short var0 = s_8;
        var0 = var0;
        M17(-81, ref s_1[0], 32766, (ulong)M17(127, ref s_1[0], var0, 0UL, 1, -128) / (uint)(2259218901U | 1), s_4[0][0]++, (sbyte)(254 % (long)(s_6-- | 1)));
        var0 = var0;
        if (s_7)
        {
            {
                M17(s_9[0], ref s_1[0], M17((sbyte)M17((sbyte)(s_2[0]-- + 112), ref s_1[0], (short)(7155 ^ s_4[0][0]--), (ulong)M17(-76, ref s_1[0], (short)((uint)(-2147483647 & s_9[0]) * (byte)(s_6-- & M17(s_9[0]--, ref s_1[0], var0, 745614966877508870UL, s_4[0][0]++, -127))), 9352707486386095167UL, s_4[0][0]++, (sbyte)M17(9, ref s_1[0], var0, 2UL, -2, s_9[0]--)), (int)M17(s_2[0]--, ref s_1[0], M17(s_2[0]--, ref s_1[0], M17((sbyte)M17(s_2[0]--, ref s_1[0], var0, (ulong)M17(14, ref s_1[0], M17(s_2[0], ref s_1[0], -16047, (ulong)M17(7, ref s_1[0], 32766, 8457600465970406527UL, s_4[0][0], 58), 2147483647, (sbyte)M17(0, ref s_1[0], var0++, 0UL, s_4[0][0], (sbyte)M17(53, ref s_1[0], var0, s_10[0], s_4[0][0], (sbyte)(s_10[0] - s_3[0])))), s_10[0], s_4[0][0]++, (sbyte)M17((sbyte)M17(s_9[0], ref s_1[0], var0, 1UL, (int)(var0 + s_6), (sbyte)M17((sbyte)M17(s_2[0], ref s_1[0], var0, s_5, -2090690306, (sbyte)(var0 ^ 2179297289U)), ref s_1[0], 0, (ulong)M17((sbyte)M17((sbyte)M17(10, ref s_1[0], var0, 1UL, s_4[0][0]++, -119), ref s_1[0], (short)((long)((byte)M17(s_2[0], ref s_1[0], var0, 9223336888783397185UL, (int)(s_6++ | s_8--), (sbyte)((byte)M17((sbyte)M17((sbyte)(s_2[0]-- & -14877), ref s_1[0], var0, (ulong)M17(M18(), ref s_13[0, 0], var0, s_5, s_12++ | s_26++, -10), (int)M23(1, s_4[0][0], -13, ref s_16, (long)M17(-127, ref s_22[0][0], M17(M18(), ref s_1[0], (short)M29(), 9008039238329753637UL, s_12++, s_9[0]), (ulong)(M30() + M32((byte)M36())), 729916534, s_9[0]), (ushort)(1 % (int)(s_38[0][0] | 1)), s_2[0], s_13[0, 0]++, new ushort[]{0, 65534, 0, 0, 3735, 65534, 14821, 42826, 1, 1}), -84), ref s_13[0, 0], (short)M33(ref s_4), s_41, (int)M32(0), -1) + -1L)) / (ushort)((ushort)M24() | 1)) & 16599), 18446744073709551615UL, 0, M44()), ref s_13[0, 0], var0, 18446744073709551615UL, (int)(s_26-- ^ s_33++), s_16[0, 0]), 10, s_9[0]--)), ref s_1[0], var0++, 3999434680244708037UL, s_32[0, 0, 0]++, s_26++)), 1, M35(new ushort[][]{new ushort[]{1, 4767, 42357}, new ushort[]{0, 1, 21613, 1}, new ushort[]{60482, 38195, 1, 1, 1}, new ushort[]{54433}, new ushort[]{62505, 0, 34971, 34695, 45235}, new ushort[]{1, 1, 43313, 1, 56063, 62075, 20072}, new ushort[]{1}}, ref s_5, ref s_40[0][0, 0])), ref s_1[0], var0, (ulong)M32(61), -2147483648, (sbyte)M43(ref var0)), 1161032424465406515UL, s_4[0][0], -128), (ulong)M28(s_27), 2147483646, (sbyte)((ushort)M23(254, s_12--, M35(new ushort[][]{new ushort[]{11718, 0, 1}, new ushort[]{65535, 0, 52604, 1, 1, 39534}, new ushort[]{25046, 35438, 65535}, new ushort[]{1, 2, 62717, 1, 57237, 12709, 26461}, new ushort[]{65534}, new ushort[]{1, 43142, 29516, 0, 62388}, new ushort[]{65535, 0, 59907, 48100, 64044, 0, 6918}, new ushort[]{1, 61231, 42692}, new ushort[]{26961}}, ref s_5, ref M33(ref s_4)), ref s_16, s_27--, 10, 0, M29(), s_3) ^ 0)), 22), ref s_13[0, 0], 1, (ulong)M35(new ushort[][]{new ushort[]{65534, 19166}, new ushort[]{1, 1, 46993, 14376, 552, 9102}, new ushort[]{2216, 7544, 0, 1, 5527}, new ushort[]{62143, 0, 1}, new ushort[]{0, 65535, 1, 1}, new ushort[]{1, 37257, 37942, 40368, 15440, 65534, 30685}, new ushort[]{65199, 47358, 1, 65535, 1}, new ushort[]{1, 1, 0, 65534, 44804, 63322, 65534, 0}}, ref s_5, ref s_29), s_21, -68), M45(622521613, (ushort)M21(s_38[0][0]--), ref s_7, 2147483646, s_30), s_21--, 73);
                long var1 = 10L;
                var1 = 9223372036854775806L;
                var1 = var1;
                if (14551 <= s_1[0])
                {
                    ref long var2 = ref s_6;
                    var0 = (short)M30();
                    if (s_36[0][0])
                    {
                        var1 = (long)M27();
                    }
                    else
                    {
                        var2 = ref var2;
                    }

                    s_rt.Checksum("c_234", var2);
                    return (ulong)M46(s_3, 1U);
                }
                else
                {
                    var0 = M51(-2147483648, s_33--, ref s_39, (sbyte)((uint)M17(s_16[0, 0], ref s_13[0, 0], var0, 13784490384085270091UL, 131041786, s_9[0]++) / (short)(s_42++ | 1)) / (sbyte)(1 | 1), s_13[0, 0]++);
                    s_22[0] = s_1;
                }

                uint var3 = 122694802U;
                var1 = var1;
                s_rt.Checksum("c_241", var1);
                s_rt.Checksum("c_242", var3);
            }
        }

        s_rt.Checksum("c_243", var0);
        return 10UL;
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
        s_2[0] = s_9[0];
        s_3[0] *= (ushort)(-21327 | s_8++);
        if (s_7)
        {
            s_4[0] = new int[]{1};
            s_10 = new ulong[]{1UL, 0UL, 1UL};
            if (s_7)
            {
                if (s_7)
                {
                    return -2;
                }
                else
                {
                    s_2[0] = s_2[0]--;
                    s_8 = 20985;
                }
            }
        }
        else
        {
            s_3 = new ushort[]{65534, 43642, 1};
            {
                ref int var0 = ref s_4[0][0];
                --s_4[0][0];
                s_rt.Checksum("c_50", var0);
            }
        }

        M19(ref s_13[0, 0], s_8, new uint[]{2007079903U, 2928364922U, 0U});
        if ((ulong)((short)M21(1393037859) & 1U) >= 10531534789933114588UL)
        {
            try
            {
                s_1[0] = s_1[0];
                sbyte var1 = s_2[0];
                s_rt.Checksum("c_62", var1);
            }
            finally
            {
                s_11 = -6982326905491414656L;
                {
                    s_6 = s_14[0]++;
                    short var2 = (short)((sbyte)(s_8++ + s_3[0]--) % (uint)(s_1[0]++ | 1));
                    long var3 = -1L;
                    s_rt.Checksum("c_63", var2);
                    s_rt.Checksum("c_64", var3);
                }

                M21((int)M21((int)M21(2147483647)));
            }

            ref ulong var4 = ref s_5;
            bool var5 = true;
            s_15 = 2;
            M20() = s_13;
            if (var5 || var5)
            {
                int var6 = s_4[0][0]--;
                byte var7 = M23(s_15++, (int)M21(M24()), s_9[0], ref s_16, s_6, s_3[0], 1, s_13[0, 0]++, new ushort[]{65534, 43215, 0, 65534, 23043, 0, 56905});
                var5 ^= true;
                s_rt.Checksum("c_80", var6);
                s_rt.Checksum("c_81", var7);
            }

            ulong var8 = 13220115286037351746UL / (ulong)(var4 | 1);
            var5 = true;
            ushort[] var9 = s_3;
            var5 = var5;
            s_17 = s_3[0];
            s_rt.Checksum("c_82", var4);
            s_rt.Checksum("c_83", var5);
            s_rt.Checksum("c_84", var8);
            s_rt.Checksum("c_85", var9[0]);
        }
        else
        {
            s_18 = 0;
            M22();
            s_4 = M25();
            s_23 = new bool[]{false, true, false, true, true, true, true, true, false};
            s_23 = s_23;
        }

        if (s_7)
        {
            M28(1L) = 0;
            if (s_23[0] || M26())
            {
                M19(ref s_1[0], (short)(1U + s_14[0]), new uint[]{0U, 2U, 4294967295U, 2U, 3731373174U, 4294967295U, 264228039U, 1U, 1U});
            }

            M28((long)M22())--;
            s_21 = -1939140204;
            M28((long)M22());
            s_5 = (ulong)((uint)M24() + 2147483646) + s_5;
            short var10 = 32766;
            try
            {
                M25();
                s_6 = s_14[0];
            }
            finally
            {
                var10 = 1;
                bool var11 = false;
                try
                {
                    var11 = var11;
                }
                finally
                {
                    s_24 = var10;
                    s_1[0] = (uint)M28(-2291255229784339879L);
                    if (var11)
                    {
                        var11 = s_7;
                    }
                    else
                    {
                        M28(-10L) = var10;
                        ushort var12 = s_17++;
                        s_rt.Checksum("c_116", var12);
                    }
                }

                s_rt.Checksum("c_117", var11);
            }

            var10 = var10;
            s_18 = 255;
            s_rt.Checksum("c_118", var10);
        }

        short var13 = s_24;
        {
            M28(s_6);
            if (s_7)
            {
                return (sbyte)((ulong)M23((byte)M29(), 1, s_16[0, 0], ref s_16, s_6, (ushort)(7780186450424156339L * s_16[0, 0]--), s_9[0], (uint)M27(), s_3) + (byte)M29());
            }
            else
            {
                long[, ][] var14 = new long[, ][]{{new long[]{-2501106294853838729L, 3378895331082875076L}, new long[]{0L, 3763917057734474099L, 0L, -1058740372623305087L}, new long[]{-2222883223302504405L, -3085916324620492591L}, new long[]{-6568546273535836497L, 7397389474725830540L, -9223372036854775808L, -187765277629754306L, 3993500737808176140L}, new long[]{-1962348721567372890L, -10L, 5497255172687862355L, -9055331007661614099L, 551951626699487616L, 0L}, new long[]{10L, -10L, -6942987649869402841L, 9223372036854775806L, 452210862698768735L, -3115426301167036953L}}};
                uint var15 = s_1[0]--;
                s_rt.Checksum("c_119", var14[0, 0][0]);
                s_rt.Checksum("c_120", var15);
            }

            var13 = var13;
            var13 = var13--;
            {
                {
                    var13 = var13;
                    s_1[0] = 4294967294U;
                    ushort var16 = s_3[0];
                    s_rt.Checksum("c_121", var16);
                }

                int var17 = (int)M21(s_12++);
                var17 = var17;
                s_rt.Checksum("c_122", var17);
            }

            {
                s_19 = (short)(s_2[0] % (sbyte)((sbyte)(28812 + s_14[0]--) | 1));
            }

            byte var18 = 11;
            s_25 = (byte)(-9223372036854775807L ^ s_17--);
            s_26 = 0;
            {
                s_21 = -2;
                var13 = var13;
            }

            try
            {
                ulong var19 = s_10[0];
                s_rt.Checksum("c_123", var19);
            }
            finally
            {
                s_2[0] = -35;
                s_14 = new long[]{-3831265056152661688L, 1L, -1436644387655487835L, -9223372036854775808L, 9223372036854775806L};
                if (s_7)
                {
                    var18 = 160;
                    ref ushort var20 = ref s_17;
                    {
                        if (M26())
                        {
                            var13 = var13;
                            M20() = new uint[, ]{{86363060U, 1U, 4294967295U}, {4115741587U, 0U, 2U}};
                        }

                        M23(178, 0, s_16[0, 0]--, ref s_16, s_11++, var20, s_2[0], (uint)(var13++ | 1U), new ushort[]{57446, 34465});
                        int var21 = s_21++;
                        s_27 = s_6;
                        var18 = var18;
                        long var22 = (long)M24();
                        var21 = -552058055;
                        M27();
                        s_rt.Checksum("c_124", var21);
                        s_rt.Checksum("c_125", var22);
                    }

                    ref uint var23 = ref s_13[0, 0];
                    s_rt.Checksum("c_126", var20);
                    s_rt.Checksum("c_127", var23);
                }

                if (M26())
                {
                    var18 = var18;
                    try
                    {
                        var18 = s_18;
                        --var13;
                    }
                    finally
                    {
                        var13 = s_8;
                        var13 = 1583;
                    }

                    s_11 = s_11;
                    s_4[0][0] = M24();
                    sbyte var24 = (sbyte)M28(s_14[0]);
                    M29();
                    s_rt.Checksum("c_128", var24);
                }

                M28(s_11++) = var13;
                var18 = (byte)(33106 / (long)(s_14[0] | 1));
            }

            var13 = s_24--;
            s_rt.Checksum("c_129", var18);
        }

        {
            var13 = var13;
            s_28 = var13--;
            M24();
            var13 = var13;
            s_15 ^= 187;
            if (s_23[0])
            {
                s_27 = (long)M29();
            }

            {
                M24();
                s_4[0] = new int[]{2147483647, 2008214370, 1488870619, 362258395, 2};
                M22();
                var13 = -32768;
            }

            s_29 = s_27;
            var13 = var13;
            var13 = -32768;
        }

        s_rt.Checksum("c_130", var13);
        return s_26++;
    }

    static ulong[] M19(ref uint arg0, short arg1, uint[] arg2)
    {
        long var0 = arg2[0] ^ -32665;
        ref uint var1 = ref arg2[0];
        long var2 = var0;
        arg0 = 2U;
        M20();
        int var3 = s_12++;
        s_14 = new long[]{9223372036854775806L, -3281498678345351126L, -9223372036854775808L, -3122657114242456583L, 5695272276153936953L, 2176666584825965377L, -6551355637144547308L, 2L, 1L};
        ref sbyte var4 = ref s_2[0];
        s_3 = new ushort[]{65535, 57626, 38477, 63700, 34501, 0, 65534, 1, 39454};
        long var5 = 9173985135957611349L;
        try
        {
            var1 = arg2[0];
        }
        finally
        {
            s_7 = s_7;
        }

        var2 = var5;
        M21(var3);
        arg2[0] = (uint)M22();
        s_8 = arg1;
        s_rt.Checksum("c_53", arg0);
        s_rt.Checksum("c_54", arg1);
        s_rt.Checksum("c_55", arg2[0]);
        s_rt.Checksum("c_56", var0);
        s_rt.Checksum("c_57", var1);
        s_rt.Checksum("c_58", var2);
        s_rt.Checksum("c_59", var3);
        s_rt.Checksum("c_60", var4);
        s_rt.Checksum("c_61", var5);
        return new ulong[]{1UL, 1UL, 0UL, 15423146024344056026UL, 1UL, 18446744073709551615UL};
    }

    static ref uint[, ] M20()
    {
        s_3 = new ushort[]{48756, 0, 1, 22371, 0, 3289, 1};
        {
            M21(s_4[0][0]++);
            s_3 = new ushort[]{26922, 3046, 38349, 25841, 28119, 0, 57341};
        }

        bool var0 = false;
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
        M24();
        s_3[0] = arg8[0];
        {
            s_6 = -4132708345189436987L;
            M24();
            {
                {
                    int var0 = arg1--;
                    arg0 = s_15;
                    s_rt.Checksum("c_65", var0);
                }

                M24();
            }

            {
                M24();
                sbyte var1 = -93;
                bool var2 = true;
                arg8 = s_3;
                arg3 = arg3;
                ulong[] var3 = new ulong[]{0UL, 11769882297530445718UL, 16460209444906240365UL};
                sbyte var4 = arg2;
                M24();
                int var5 = (int)(1484787614U ^ (ulong)M24());
                M24();
                if (var2)
                {
                    s_3[0] = arg5;
                    arg3[0, 0] = (sbyte)(0 ^ arg8[0]);
                }

                s_rt.Checksum("c_66", var1);
                s_rt.Checksum("c_67", var2);
                s_rt.Checksum("c_68", var3[0]);
                s_rt.Checksum("c_69", var4);
                s_rt.Checksum("c_70", var5);
            }
        }

        s_rt.Checksum("c_71", arg0);
        s_rt.Checksum("c_72", arg1);
        s_rt.Checksum("c_73", arg2);
        s_rt.Checksum("c_74", arg3[0, 0]);
        s_rt.Checksum("c_75", arg4);
        s_rt.Checksum("c_76", arg5);
        s_rt.Checksum("c_77", arg6);
        s_rt.Checksum("c_78", arg7);
        s_rt.Checksum("c_79", arg8[0]);
        return arg0;
    }

    static int M24()
    {
        s_10 = s_10;
        return s_4[0][0];
    }

    static int[][] M25()
    {
        try
        {
            if (M26())
            {
                bool var0 = true;
                M28(1L);
                var0 = var0;
                s_rt.Checksum("c_113", var0);
            }
            else
            {
                s_9[0] = 0;
                s_3[0] = s_3[0]++;
                s_22 = new uint[][]{new uint[]{1U}, new uint[]{2208540405U, 1U}, new uint[]{1U}, new uint[]{61961135U, 0U, 0U}, new uint[]{4294967294U}};
                {
                    M27();
                }
            }

            s_6 = (long)M27();
        }
        finally
        {
            M26();
            s_22 = new uint[][]{new uint[]{1U, 0U}, new uint[]{1188609893U, 2U, 0U}, new uint[]{891345313U}};
        }

        {
            uint var1 = s_22[0][0]--;
            M26();
            s_rt.Checksum("c_114", var1);
        }

        ref ushort var2 = ref s_3[0];
        s_rt.Checksum("c_115", var2);
        return s_4;
    }

    static bool M26()
    {
        if (s_7)
        {
            if (-5939421279753052427L > s_11)
            {
                sbyte var0 = s_9[0];
                s_rt.Checksum("c_86", var0);
            }

            if (s_7)
            {
                s_14[0] = 5620087363910517090L;
                ulong var1 = 10037972119732552812UL;
                try
                {
                    if ((int)(4164895191U + s_3[0]--) == 2835991067U)
                    {
                        s_7 = true;
                        M27();
                    }
                    else
                    {
                        ushort[] var2 = new ushort[]{20234, 52302};
                        int var3 = -1834367155;
                        bool[, ] var4 = new bool[, ]{{true, false, true, false, false, false}, {true, false, true, true, true, false}, {true, false, true, false, true, true}, {false, false, false, false, true, true}, {false, true, true, false, false, true}, {false, true, true, false, true, false}, {false, false, true, true, false, true}, {true, false, true, false, false, false}};
                        short var5 = 0;
                        s_rt.Checksum("c_88", var2[0]);
                        s_rt.Checksum("c_89", var3);
                        s_rt.Checksum("c_90", var4[0, 0]);
                        s_rt.Checksum("c_91", var5);
                    }
                }
                finally
                {
                    var1 = 13438501823296519221UL;
                    var1 = var1;
                    var1 -= 18446744073709551614UL;
                }

                uint[] var6 = new uint[]{0U, 2535587414U, 3736969003U, 1U, 1061972498U, 4294967294U};
                {
                    var6 = s_1;
                    var6[0] = var6[0];
                    var6 = var6;
                    var6[0] = var6[0];
                    var6[0] = 1548797251U;
                    byte[] var7 = new byte[]{0, 0, 100, 0, 1};
                    s_4[0] = new int[]{0, 2, 2147483646, 1};
                    s_rt.Checksum("c_92", var7[0]);
                }

                s_rt.Checksum("c_93", var1);
                s_rt.Checksum("c_94", var6[0]);
            }
        }

        ref bool var8 = ref s_7;
        if (s_8 == 11932)
        {
            M28(s_6--);
            {
                M29();
            }

            if (var8)
            {
                if (var8)
                {
                    var8 = var8;
                    ushort[][] var9 = new ushort[][]{new ushort[]{6607}, new ushort[]{27393, 37656, 65534}};
                    s_rt.Checksum("c_104", var9[0][0]);
                }

                var8 = ref var8;
            }

            if (var8)
            {
                s_5 = 18446744073709551615UL;
                var8 = s_7;
                M27();
                M29();
            }
            else
            {
                var8 = ref s_7;
                var8 = var8;
                M28((long)(s_20[0]++ * 10)) = s_8++;
                byte var10 = (byte)M29();
                s_rt.Checksum("c_105", var10);
            }

            {
                M28(s_11) = s_8--;
                var8 = var8;
            }

            var8 = var8;
            var8 = true;
            M27();
            var8 = false;
        }
        else
        {
            M28(s_11++);
            {
                int var11 = 1845037249;
                s_rt.Checksum("c_106", var11);
            }
        }

        if (var8)
        {
            if (s_7)
            {
                sbyte var12 = 58;
                {
                    var8 = var8;
                    sbyte[] var13 = new sbyte[]{82, -127, -85, 127, -127};
                    s_17 = (ushort)M29();
                    int var14 = s_12;
                    M28(0L);
                    s_rt.Checksum("c_107", var13[0]);
                    s_rt.Checksum("c_108", var14);
                }

                M28(9223372036854775806L);
                s_rt.Checksum("c_109", var12);
            }

            try
            {
                try
                {
                    try
                    {
                        var8 = ref var8;
                    }
                    finally
                    {
                        var8 = false;
                        var8 = ref var8;
                    }
                }
                finally
                {
                    if (s_10[0] <= (ulong)M29())
                    {
                        M28(-9223372036854775807L) = -32768;
                        var8 = var8 || var8;
                        var8 = ref s_7;
                        var8 = ref var8;
                    }
                    else
                    {
                        s_21 = s_12;
                        s_22 = new uint[][]{new uint[]{1174595855U}, new uint[]{0U, 0U}, new uint[]{10U, 4160269689U, 4214580266U}};
                        s_16 = new sbyte[, ]{{119, 1, -127, 126, 1}, {0, 0, 22, 91, -120}, {-80, -10, -34, 6, -33}, {53, -120, 33, 126, -128}, {-10, 1, 97, 0, 127}, {0, 127, 10, -110, 115}, {0, 1, -80, -94, 1}, {-128, -76, -127, 0, 1}, {-128, 1, -33, 94, 1}, {113, -41, -61, 17, 0}};
                        M28(0L);
                        var8 = var8;
                    }
                }
            }
            finally
            {
                ref ushort var15 = ref s_3[0];
                s_rt.Checksum("c_110", var15);
            }

            bool[] var16 = new bool[]{false, true, true, false, true, false, true, false, true};
            var8 = var8;
            s_rt.Checksum("c_111", var16[0]);
        }

        s_rt.Checksum("c_112", var8);
        return var8;
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
        if (s_7)
        {
            ushort[] var0 = new ushort[]{32768, 26897, 13695, 1, 4638, 12555, 37480, 1};
            s_rt.Checksum("c_95", var0[0]);
        }

        if (s_7)
        {
            try
            {
                arg0 |= s_11;
            }
            finally
            {
                arg0 = arg0;
            }

            int[] var1 = new int[]{1440886930, 1662999250, -10, -2147483648, 1571726538, 1};
            s_rt.Checksum("c_96", var1[0]);
        }

        arg0 = arg0;
        s_18 = (byte)(s_17 ^ 182);
        try
        {
            {
                M29();
            }
        }
        finally
        {
            s_19 = s_8;
            arg0 = arg0;
            {
                s_19 = -6420;
                ref ushort var2 = ref s_17;
                {
                    ++var2;
                    byte var3 = (byte)M29();
                    s_20 = new byte[]{0};
                    M29();
                    M29();
                    long var4 = arg0;
                    var4 = 7844966598244298994L;
                    s_rt.Checksum("c_97", var3);
                    s_rt.Checksum("c_98", var4);
                }

                s_rt.Checksum("c_99", var2);
            }
        }

        if (s_7)
        {
            M29();
            sbyte var5 = s_2[0]++;
            arg0 = s_6;
            arg0 = arg0;
            byte var6 = s_15--;
            s_rt.Checksum("c_100", var5);
            s_rt.Checksum("c_101", var6);
        }

        arg0 = s_14[0];
        M29();
        arg0 = (long)M29();
        bool var7 = s_7 | true;
        s_rt.Checksum("c_102", arg0);
        s_rt.Checksum("c_103", var7);
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
        s_23 = s_23;
        s_30 = new short[]{-1, 11945, 28500, -10, -25602, -21293, -32768, -8044};
        if (s_17-- >= s_26)
        {
            s_24 = s_19;
            s_24 = s_24++;
            try
            {
                M31();
                {
                    long var0 = s_14[0];
                    M31();
                    uint var1 = s_22[0][0]++;
                    var0 = s_6;
                    M31();
                    if (s_23[0])
                    {
                        M31();
                        ref sbyte[] var2 = ref s_9;
                        s_rt.Checksum("c_132", var2[0]);
                    }
                    else
                    {
                        byte var3 = s_18++;
                        ushort var4 = 0;
                        s_rt.Checksum("c_133", var3);
                        s_rt.Checksum("c_134", var4);
                    }

                    --s_26;
                    s_rt.Checksum("c_135", var0);
                    s_rt.Checksum("c_136", var1);
                }
            }
            finally
            {
                try
                {
                    sbyte var5 = s_26--;
                    s_rt.Checksum("c_137", var5);
                }
                finally
                {
                    M31();
                    M31();
                    s_31 = s_31;
                    s_15 = 17;
                }

                short var6 = 1;
                s_rt.Checksum("c_138", var6);
            }

            ++s_16[0, 0];
            s_29 = s_27++;
            if (s_23[0])
            {
                s_11 = 1440939976202332709L;
                s_13[0, 0] = 1115023340U;
                try
                {
                    M31();
                }
                finally
                {
                    if (s_7)
                    {
                        long var7 = -9223372036854775808L;
                        M31();
                        var7 %= (long)(s_29 | 1);
                        var7 = var7--;
                        s_rt.Checksum("c_139", var7);
                    }
                    else
                    {
                        if (s_7)
                        {
                            M31();
                        }

                        s_29 = -7155846240209685667L;
                        s_19 = 9962;
                    }
                }

                M31();
            }
        }
        else
        {
            s_4 = new int[][]{new int[]{-2147483648, -805982672, 1, 2147483647}, new int[]{1505452516, -1751433094, 1344734383}, new int[]{910077736, 10, -10, -1169840563}, new int[]{2147483647, -432552860, 576558872}, new int[]{-856700924, 0, 203554976, -2147483648}, new int[]{405073323, 0, 752602499, 1381182394}};
            s_24 = s_30[0];
            ushort[] var8 = s_3;
            M31();
            {
                byte var9 = s_15;
                s_rt.Checksum("c_140", var9);
            }

            {
                M31();
            }

            var8 = s_3;
            bool var10 = false;
            var8[0] = var8[0];
            s_rt.Checksum("c_141", var8[0]);
            s_rt.Checksum("c_142", var10);
        }

        return 202120809106338645L;
    }

    static uint[] M31()
    {
        ulong var0 = 4787032656520567745UL;
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
        arg0[0][0] = 24332;
        if (s_23[0])
        {
            ref byte var0 = ref s_20[0];
            s_rt.Checksum("c_146", var0);
        }
        else
        {
            {
                ++arg2;
            }

            ref uint var1 = ref s_13[0, 0];
            if (s_1[0] == s_11)
            {
                int var2 = s_21;
                s_rt.Checksum("c_147", var2);
            }
            else
            {
                try
                {
                    {
                        if (s_7)
                        {
                            var1 = ref s_22[0][0];
                            var1 = s_1[0];
                        }

                        s_1[0] = var1;
                        arg0[0][0] = 2;
                    }

                    arg1 = ref arg1;
                    M36();
                }
                finally
                {
                    M39(arg1, s_33--);
                }

                arg0[0] = new ushort[]{13216, 54836, 0, 1, 14044, 1, 1, 0};
                M37(ref arg2, new short[, ][][, ]{{new short[][, ]{new short[, ]{{-27168, 15123, 0, -32768, 13128}}}}, {new short[][, ]{new short[, ]{{0, 1, -32768, 0, 32766}}}}, {new short[][, ]{new short[, ]{{1, 31883, -1, -32768, -32767}}}}, {new short[][, ]{new short[, ]{{24085, -2, 14713, -10, 32767}}}}, {new short[][, ]{new short[, ]{{26362, -11747, 32766, 32766, 32767}}}}, {new short[][, ]{new short[, ]{{-32132, 0, 25170, -13795, -4860}}}}}, s_9[0], (sbyte)M36());
                M38(-33, (ushort)(s_20[0] & (short)M36()));
                var1 = (uint)(arg2 ^ s_3[0]++);
            }

            {
                byte[] var3 = s_20;
                var3[0]++;
                s_11 |= -6123530783080863779L;
                s_32 = new int[,, ]{{{1644192081, 0, 2147483646, -2147483648, 657337028, 514605455, -1, 0, 506873077, 0}, {-2147483647, 1, 1, 2147483647, 287161755, 244130849, 1040780113, -2147483647, 1, 0}, {2, 2147483646, -1592734895, 1238632324, 733120299, 0, 1, -317321065, -10, 397582417}, {2147483647, 0, -373877722, 0, -833131522, -1, -22270918, 1706598921, -1126223269, 824888591}, {-1457163346, 613656544, 1, 0, 1, 0, 2, -2147483648, 1773043271, -2147483647}, {-1907229447, -1, -2147483648, -2147483647, 0, 2, 2147483646, 0, -1714992345, 10}}, {{10, 0, 2, -2147483647, 1043919523, 1, 2147483646, 288116906, -2147483647, 1512137146}, {383216214, -10, 1027389398, 2147483647, 2069042961, 413463961, 0, -10, 2147483647, -1}, {-1, -2147483648, -1013100966, 432671254, -410846938, -27069980, 2147483646, -2147483648, -2124349868, -2}, {-2147483648, -1868317618, 1, -907195954, 2, 0, 1, 2147483646, 2147483647, 1}, {0, -2147483647, -2147483648, 2, -10, -2147483648, 2147483647, 1, 1811859088, 10}, {1, -1270495042, -483117253, -2147483647, -54785423, -2, 874570875, 1280239441, -2, 2147483647}}, {{-10, 2147483647, -353573941, -52395198, -1147783953, -2147483647, 2147483647, -2147483647, -10, -1030249327}, {0, -738543931, 2009708361, -670248101, -1534902548, 1, -2147483647, 0, 0, -2147483648}, {1, -1953612605, 1, -2, -1311676505, 1, 2147483647, 1601872886, -1301158021, -1873111491}, {1642379649, -2147483647, -357392149, -2147483648, 1869548439, 1074663370, 0, 2087822593, 0, 2147483647}, {2147483646, 1, -1935112914, -198228611, 10, 1, 959505873, 0, 1822856447, -2147483648}, {-322822942, 1, 2147483647, 1, -2147483648, 2147483646, -727080080, -271212402, 235796399, -1}}, {{-667583732, 1985733729, 666800800, 228229819, -2147483648, 302538751, 460210107, 126266835, 2147483647, -983012387}, {-333004521, 1370212456, 1875644305, 2147483646, -1298568806, 2147483647, -169074797, 436555372, 190246676, -2147483648}, {-2147483647, 1836959280, 2147483647, 1402669525, -2147483648, -2043042746, -309223339, 1857211121, -1659048351, 2147483646}, {-56280306, 0, 10, 2012765909, -1, 1287698566, -2, -1721539634, 0, -10}, {531002189, 324947286, 2147483646, 515082605, 1691675991, -2147483648, 561072793, -1, 0, 2147483646}, {1, 0, -1510130223, 980012344, 414583778, 2147483646, 1802740695, -1721897976, -1, 1519329138}}};
                s_13 = new uint[, ]{{2770849606U, 3930682774U, 4291797167U, 2274102951U, 0U, 4294967295U}, {4294967294U, 4294967295U, 10U, 1626024211U, 1U, 3441123574U}, {1U, 2211367547U, 2466647209U, 1103784685U, 3807746817U, 1U}};
                M39(11133535487298317325UL, 1U);
                {
                    byte var4 = 3;
                    arg1 = arg1;
                    s_rt.Checksum("c_178", var4);
                }

                M38(s_16[0, 0]--, 0);
                {
                    M39(arg1, var1);
                }

                s_rt.Checksum("c_179", var3[0]);
            }

            var1 = 1U;
            if (s_23[0])
            {
                try
                {
                    M38((sbyte)(arg1 * s_22[0][0]), 0);
                }
                finally
                {
                    var1 = var1;
                    arg0 = new ushort[][]{new ushort[]{65534, 10}, new ushort[]{2191}, new ushort[]{0, 62048, 49712, 0}, new ushort[]{1, 56515}, new ushort[]{1, 1, 2, 10, 32718, 0}};
                }

                s_33 = 1U;
            }

            short var5 = s_24;
            byte var6 = (byte)M36();
            s_23 = new bool[]{false, true, true, true, true};
            s_4 = new int[][]{new int[]{-121241096}, new int[]{1485421566, 1}, new int[]{-2, 1}, new int[]{2}, new int[]{1, 0}, new int[]{1475310965}, new int[]{0}, new int[]{0}};
            {
                arg0[0][0] = arg0[0][0];
                sbyte var7 = 1;
                M38(0, (ushort)M36());
                ref sbyte var8 = ref var7;
                s_rt.Checksum("c_180", var7);
                s_rt.Checksum("c_181", var8);
            }

            s_14[0] = arg2;
            s_rt.Checksum("c_182", var1);
            s_rt.Checksum("c_183", var5);
            s_rt.Checksum("c_184", var6);
        }

        long[] var9 = M40(7022, 2147483647);
        {
            s_37 ^= s_21;
            if (var9[0] <= s_33)
            {
                s_38 = new int[][]{new int[]{1, -1195254711, 0, 2147483646, 0, 1, 2147483646}, new int[]{-2, -2147483648, -2147483647, 29298245, 0, -1014793009, 0}};
                ref uint var10 = ref s_13[0, 0];
                s_29 = (long)M42();
                var9[0] = arg2--;
                arg2 = 8556625560205651375L;
                M36();
                if (s_23[0])
                {
                    s_2 = s_9;
                }

                s_rt.Checksum("c_200", var10);
            }
            else
            {
                s_24 = s_28--;
                s_39 = s_23[0];
                s_40 = s_40;
            }

            arg0[0] = new ushort[]{4616, 56194, 14928, 1, 18710, 1, 0, 56629, 22023};
            try
            {
                if (M37(ref arg2, new short[, ][][, ]{{new short[][, ]{new short[, ]{{1}, {13569}}, new short[, ]{{1}, {-11625}, {-25092}, {10865}}, new short[, ]{{-15337}, {23870}, {32767}}, new short[, ]{{-32768}, {-32767}, {1}, {1}}, new short[, ]{{-25236}, {1}, {13930}, {22959}}}}, {new short[][, ]{new short[, ]{{22787}, {-30318}}, new short[, ]{{25470}, {32766}, {1}, {17437}}, new short[, ]{{25296}, {0}}, new short[, ]{{1}, {1}}, new short[, ]{{-19675}}, new short[, ]{{-32768}, {10}, {-6088}}, new short[, ]{{-2242}, {0}}, new short[, ]{{25673}, {-19585}, {-28978}, {-1}}, new short[, ]{{10}, {9913}}}}, {new short[][, ]{new short[, ]{{-20188}}, new short[, ]{{-18851}, {3708}, {1294}}, new short[, ]{{-17335}}, new short[, ]{{-1555}, {1}}, new short[, ]{{1}, {-1}, {5646}, {-16879}}, new short[, ]{{-16136}, {19392}, {32766}, {1}}, new short[, ]{{-32768}, {1}}, new short[, ]{{-10}, {-32768}, {-18922}}, new short[, ]{{-10}, {32767}, {-4149}, {18176}}}}, {new short[][, ]{new short[, ]{{5631}}, new short[, ]{{-32768}, {15996}, {-32767}, {24326}}, new short[, ]{{17446}, {-23871}, {10885}, {-32767}}}}, {new short[][, ]{new short[, ]{{23710}, {371}, {-5105}, {26812}}}}, {new short[][, ]{new short[, ]{{10}, {9961}, {22756}}, new short[, ]{{0}, {32767}, {1}, {-26303}}}}, {new short[][, ]{new short[, ]{{10}, {-32767}}, new short[, ]{{13398}, {31978}}, new short[, ]{{-32015}, {29049}, {3440}, {27815}}}}}, s_9[0], 1))
                {
                    M39(arg1, 4294967295U);
                    byte var11 = M43(ref s_8);
                    uint[] var12 = new uint[]{4294967294U, 1U, 545153929U, 1U, 2415608057U, 1U, 1U, 1460005043U, 2276515527U};
                    uint var13 = var12[0];
                    try
                    {
                        byte var14 = (byte)M36();
                        s_rt.Checksum("c_203", var14);
                    }
                    finally
                    {
                        s_32[0, 0, 0] = 1;
                        M39(arg1, M42());
                    }

                    try
                    {
                        M41(-32767);
                    }
                    finally
                    {
                        M42();
                        s_41 = (ulong)M43(ref s_8);
                    }

                    s_rt.Checksum("c_204", var11);
                    s_rt.Checksum("c_205", var12[0]);
                    s_rt.Checksum("c_206", var13);
                }
            }
            finally
            {
                s_23[0] = s_39;
            }
        }

        s_rt.Checksum("c_207", arg0[0][0]);
        s_rt.Checksum("c_208", arg1);
        s_rt.Checksum("c_209", arg2);
        s_rt.Checksum("c_210", var9[0]);
        return 79;
    }

    static uint M36()
    {
        s_20[0] = s_25--;
        if (s_32[0, 0, 0] != s_13[0, 0])
        {
            s_2 = new sbyte[]{0, 24, 1, -107, 1, 1, -1, 10, 37};
            if (M37(ref s_11, new short[, ][][, ]{{new short[][, ]{new short[, ]{{32767, 16323, -8828, -32768}, {12870, 13801, -29320, 8599}}}, new short[][, ]{new short[, ]{{12112, 6941, 30185, -32768}, {0, 32766, -30579, 0}}, new short[, ]{{14416, 1, 29898, -25557}, {-32767, 32766, 32767, 31217}, {-4502, 14190, 0, 1}, {-30738, 10, 18158, -27873}, {-32768, 32767, 32767, 7204}}}}, {new short[][, ]{new short[, ]{{-28413, 0, 30704, 0}, {-32767, -32767, -32758, 24053}, {32767, 32767, -32768, 0}}}, new short[][, ]{new short[, ]{{-13395, 1, 2, -32768}, {-2746, 6812, -32130, 32767}, {13934, -2, -1, 0}}, new short[, ]{{-2812, 1, 1, -32768}, {32767, 862, 1, 9048}, {1, 31180, -21916, 19617}, {26342, 1, 32766, 12561}, {14326, -32768, -30014, -2005}}}}, {new short[][, ]{new short[, ]{{17893, 0, 0, 0}, {-32767, -32768, -3470, -32768}, {-32767, 32766, 2, 1}}, new short[, ]{{0, -32767, 9829, -19346}, {5200, -8083, 0, 25380}, {1, -23486, 27422, 9113}}}, new short[][, ]{new short[, ]{{-32768, 2, -3420, -18434}, {12490, -29461, 31719, 24472}, {-8931, 17539, -6291, 27749}}}}}, -91, -33))
            {
                M37(ref s_11, new short[, ][][, ]{{new short[][, ]{new short[, ]{{-2, 1137}}, new short[, ]{{-32767, 5357}}, new short[, ]{{28540, 32767}}, new short[, ]{{9292, 0}}}, new short[][, ]{new short[, ]{{21752, 32766}}}, new short[][, ]{new short[, ]{{32766, -15758}}, new short[, ]{{-32768, 1}, {32766, -11401}}, new short[, ]{{1, 0}}}, new short[][, ]{new short[, ]{{32766, -13451}}, new short[, ]{{25225, -13240}, {1, -32767}}, new short[, ]{{-32767, -2}}}, new short[][, ]{new short[, ]{{0, 0}, {-5448, -2}}, new short[, ]{{-32768, -4276}}}, new short[][, ]{new short[, ]{{1, -15319}, {0, -1}}, new short[, ]{{-32767, -32767}}, new short[, ]{{32766, 21422}}, new short[, ]{{2, 1}}}, new short[][, ]{new short[, ]{{0, 32766}}, new short[, ]{{32198, -2}}, new short[, ]{{-11816, -15920}}, new short[, ]{{0, -26189}, {13040, -32767}}}, new short[][, ]{new short[, ]{{-32768, 32766}}, new short[, ]{{-6366, 18366}}, new short[, ]{{1, 22360}}, new short[, ]{{-4170, 0}, {-9969, -864}}, new short[, ]{{10, -16859}, {-18308, -9041}}, new short[, ]{{12300, -32767}}, new short[, ]{{-1, -2}, {-5307, -30887}}}}}, -10, 1);
                {
                    s_15 -= 79;
                }
            }

            uint var0 = 1519632089U;
            {
                var0 = var0++;
                {
                    var0 = s_1[0];
                    ulong var1 = 1UL;
                    var0 = var0--;
                    s_rt.Checksum("c_153", var1);
                }
            }

            s_rt.Checksum("c_154", var0);
        }
        else
        {
            if (M37(ref s_11, new short[, ][][, ]{{new short[][, ]{new short[, ]{{-16098, -17328}}, new short[, ]{{1, 10}}}}, {new short[][, ]{new short[, ]{{-18615, 32766}}, new short[, ]{{0, 0}}}}, {new short[][, ]{new short[, ]{{-17809, -32768}}, new short[, ]{{-32767, -9688}}}}, {new short[][, ]{new short[, ]{{-31161, -8897}}, new short[, ]{{29886, 23322}}}}, {new short[][, ]{new short[, ]{{-1, 0}}, new short[, ]{{0, 27193}}}}, {new short[][, ]{new short[, ]{{25768, -2231}}}}, {new short[][, ]{new short[, ]{{0, -4542}}, new short[, ]{{1, 0}}}}, {new short[][, ]{new short[, ]{{1, -18479}}, new short[, ]{{-10, -29650}}}}, {new short[][, ]{new short[, ]{{1, 26817}}, new short[, ]{{-18282, 32767}}}}, {new short[][, ]{new short[, ]{{0, 1}}, new short[, ]{{-23379, -32768}}}}}, s_9[0]--, s_9[0]++))
            {
                if (s_7)
                {
                    s_28 = s_30[0];
                    s_19 = (short)(21 + (long)(s_18 + s_4[0][0]++));
                }
                else
                {
                    {
                        s_34 = 0;
                        long var2 = s_29;
                        var2 = var2;
                        {
                            var2 = 9223372036854775807L;
                            uint var3 = 0U;
                            s_rt.Checksum("c_155", var3);
                        }

                        s_rt.Checksum("c_156", var2);
                    }
                }
            }
            else
            {
                int var4 = s_4[0][0];
                if (s_23[0])
                {
                    s_2[0] = 0;
                    if (M37(ref s_14[0], new short[, ][][, ]{{new short[][, ]{new short[, ]{{14531}, {0}}}, new short[][, ]{new short[, ]{{23106}}, new short[, ]{{31338}}, new short[, ]{{14311}}, new short[, ]{{0}}, new short[, ]{{-14925}}, new short[, ]{{22032}, {7960}}, new short[, ]{{0}, {1}}}, new short[][, ]{new short[, ]{{32766}, {-30616}}, new short[, ]{{29207}, {17289}}, new short[, ]{{-1}, {8371}}, new short[, ]{{19744}}}, new short[][, ]{new short[, ]{{32766}}, new short[, ]{{-5417}}}, new short[][, ]{new short[, ]{{0}}, new short[, ]{{0}}, new short[, ]{{27814}}, new short[, ]{{8605}}, new short[, ]{{14833}}, new short[, ]{{0}}}}, {new short[][, ]{new short[, ]{{-14671}}}, new short[][, ]{new short[, ]{{24176}}, new short[, ]{{0}, {22580}}, new short[, ]{{-32767}, {3627}}}, new short[][, ]{new short[, ]{{-17789}, {-24790}}, new short[, ]{{0}, {-336}}, new short[, ]{{-17542}, {2}}, new short[, ]{{-7397}}}, new short[][, ]{new short[, ]{{-32768}, {-32767}}}, new short[][, ]{new short[, ]{{18170}, {27044}}}}, {new short[][, ]{new short[, ]{{-2}, {4618}}}, new short[][, ]{new short[, ]{{-14422}, {-31936}}, new short[, ]{{0}, {31133}}, new short[, ]{{-32767}, {-32767}}, new short[, ]{{-13775}}, new short[, ]{{-10}}, new short[, ]{{-28422}}, new short[, ]{{7963}, {17124}}}, new short[][, ]{new short[, ]{{23760}}, new short[, ]{{-19571}, {32767}}, new short[, ]{{1}}, new short[, ]{{32766}}, new short[, ]{{26185}, {-32768}}}, new short[][, ]{new short[, ]{{-12992}}, new short[, ]{{28170}}, new short[, ]{{-32767}, {-24580}}, new short[, ]{{0}, {-24623}}, new short[, ]{{-25986}}, new short[, ]{{5693}, {0}}}, new short[][, ]{new short[, ]{{-10403}, {32767}}, new short[, ]{{1}}, new short[, ]{{0}, {-10}}, new short[, ]{{-2694}}}}}, 18, -1))
                    {
                        s_33 = 2641845235U;
                        M37(ref s_11, new short[, ][][, ]{{new short[][, ]{new short[, ]{{0}}}}, {new short[][, ]{new short[, ]{{0}}, new short[, ]{{-16140}}}}, {new short[][, ]{new short[, ]{{-32767}}, new short[, ]{{1}}}}, {new short[][, ]{new short[, ]{{-10942}}, new short[, ]{{-30915}}, new short[, ]{{5226}}}}, {new short[][, ]{new short[, ]{{-32768}}}}, {new short[][, ]{new short[, ]{{32767}}}}}, -1, s_26);
                        var4 = var4;
                        M37(ref s_11, new short[, ][][, ]{{new short[][, ]{new short[, ]{{-18076}}, new short[, ]{{-1}, {-1558}}, new short[, ]{{-32767}, {0}}, new short[, ]{{17829}, {0}, {-1171}, {12585}, {22750}, {-32768}, {23366}}, new short[, ]{{-32768}, {0}}, new short[, ]{{-32767}, {-8508}, {26446}, {1}, {-12310}, {25438}}}, new short[][, ]{new short[, ]{{-10684}, {-15457}}, new short[, ]{{-27633}, {-32768}, {-29211}, {3886}}}}}, 1, -54);
                        if (M37(ref s_27, new short[, ][][, ]{{new short[][, ]{new short[, ]{{9222}, {9014}, {11954}, {32766}}, new short[, ]{{-29267}, {10190}, {1}, {32767}, {28986}, {-1835}}, new short[, ]{{-32767}, {0}}, new short[, ]{{14132}, {32767}, {0}}, new short[, ]{{-32767}, {0}, {29678}}, new short[, ]{{3377}}, new short[, ]{{-14154}, {2}, {1}, {1}}}, new short[][, ]{new short[, ]{{8994}, {-25213}, {10}, {30641}, {6912}, {-7633}}, new short[, ]{{0}, {0}, {-12848}}}}}, s_2[0], 1))
                        {
                            var4 = 1794679436;
                        }

                        var4 = s_12;
                    }

                    if (s_7)
                    {
                        var4 = var4;
                    }
                }

                s_rt.Checksum("c_157", var4);
            }
        }

        if (M38(s_2[0]++, s_3[0]--))
        {
            short var5 = 1;
            byte var6 = 203;
            var5 = var5++;
            ref sbyte[] var7 = ref s_9;
            s_rt.Checksum("c_160", var5);
            s_rt.Checksum("c_161", var6);
            s_rt.Checksum("c_162", var7[0]);
        }

        try
        {
            sbyte var8 = s_26;
            if (M38(10, 0))
            {
                var8 = 0;
                var8 = var8;
                var8 = var8;
                s_2 = new sbyte[]{1, 43};
                {
                    M37(ref s_29, new short[, ][][, ]{{new short[][, ]{new short[, ]{{9475, 0}}, new short[, ]{{-9355, -2}, {1, 1}}, new short[, ]{{10, 10216}}, new short[, ]{{19976, 1}, {13122, 903}}, new short[, ]{{23347, 25714}, {1, 2}}}, new short[][, ]{new short[, ]{{11876, -32767}}, new short[, ]{{-13212, 1}}, new short[, ]{{1, 1}, {0, 7999}}}}, {new short[][, ]{new short[, ]{{-9331, 1}, {-11483, -31443}}, new short[, ]{{-25712, -10}, {1, 32767}}, new short[, ]{{1, -9895}}}, new short[][, ]{new short[, ]{{-30709, 14961}, {0, 0}}}}}, var8, var8);
                }

                var8 = s_26--;
                ref long var9 = ref s_6;
                s_rt.Checksum("c_163", var9);
            }
            else
            {
                if (35588 != s_30[0])
                {
                    var8 = 84;
                }
                else
                {
                    byte var10 = s_18;
                    uint var11 = 4294967295U;
                    short var12 = (short)(65534 | s_30[0]++);
                    var11 = var11;
                    s_rt.Checksum("c_164", var10);
                    s_rt.Checksum("c_165", var11);
                    s_rt.Checksum("c_166", var12);
                }

                if (M37(ref s_27, new short[, ][][, ]{{new short[][, ]{new short[, ]{{-14736, 0, 7779, -32767}, {32767, 32767, -27970, 0}, {32767, 0, -1, -21444}, {0, 21485, 32766, 26513}, {0, 0, -10, -10}, {-2, 32766, 16077, -29946}, {32767, -13084, -1, 1499}, {-8485, -3002, -1, 2}}}, new short[][, ]{new short[, ]{{31221, -32768, -10, -27944}, {-13589, -32768, 1, 0}, {-1378, 7297, 32766, -14775}, {32767, 10, 12514, 1}, {24255, 32766, -10, 24887}}}, new short[][, ]{new short[, ]{{-32767, 13154, 7385, -32767}, {2, -5938, 10, -1}, {10393, 32766, 31632, 0}, {-32767, 8328, 19066, 21815}, {24026, -26188, -32767, 1}, {-32768, -24069, 32486, 2}}}, new short[][, ]{new short[, ]{{28287, 0, -19116, 6176}, {-3684, 16410, -32768, -4014}, {31467, 22269, 25545, 17971}, {-212, -16127, 0, -1566}, {32766, -16075, -32768, -2}, {-10, 32766, 1, 32766}, {1, 14693, -1, -18733}}}}, {new short[][, ]{new short[, ]{{-25147, 1, 23675, 0}, {0, -3645, -25790, -10}, {-9192, -32768, -4042, 7356}, {18511, 32766, -32768, 32658}}}, new short[][, ]{new short[, ]{{21020, 1, 1, 11795}, {0, 16145, -1000, 11249}, {0, 1, 0, 27999}, {21162, 32766, 9187, -4092}, {11907, 378, 1, -1}}}, new short[][, ]{new short[, ]{{20919, 1, 32767, -25355}, {-9446, -26206, -10, 11180}}}, new short[][, ]{new short[, ]{{-19814, 1, 0, 15032}, {-32767, 1, 1, -32767}}}}}, 20, var8))
                {
                    var8 = var8;
                    var8 = s_9[0];
                    if (s_7)
                    {
                        ulong var13 = (ulong)(s_3[0] | s_3[0]) - 237;
                        short var14 = 16326;
                        short var15 = var14;
                        var8 >>= (int)((uint)(var13 % (ushort)(s_3[0]++ | 1)) ^ (long)(50 / (int)(s_31 | 1)));
                        if (M37(ref s_27, new short[, ][][, ]{{new short[][, ]{new short[, ]{{11406}, {-3322}, {-2}, {0}, {32766}}}, new short[][, ]{new short[, ]{{1316}, {8068}, {-26304}, {-1174}, {-32767}}}, new short[][, ]{new short[, ]{{-18773}, {32766}, {-24313}, {-2}}}, new short[][, ]{new short[, ]{{-26522}, {1}, {15204}, {-7773}, {883}}}, new short[][, ]{new short[, ]{{-32768}, {1}, {2}, {14867}}}, new short[][, ]{new short[, ]{{0}, {1}}}}, {new short[][, ]{new short[, ]{{20328}, {1}, {-22295}, {-32767}, {10723}}}, new short[][, ]{new short[, ]{{-16722}, {-596}, {4364}, {13770}}}, new short[][, ]{new short[, ]{{-19475}}}, new short[][, ]{new short[, ]{{-32768}}}, new short[][, ]{new short[, ]{{-8708}, {18268}, {-32768}, {16943}, {-16419}}}, new short[][, ]{new short[, ]{{1}}}}, {new short[][, ]{new short[, ]{{0}, {-25631}, {239}, {1}}}, new short[][, ]{new short[, ]{{-10}, {-22412}, {1}, {10}}}, new short[][, ]{new short[, ]{{-26381}, {9287}, {10284}, {-32767}}}, new short[][, ]{new short[, ]{{1}}}, new short[][, ]{new short[, ]{{1}, {-10}}}, new short[][, ]{new short[, ]{{32766}, {-14349}, {0}, {11600}, {22692}}}}, {new short[][, ]{new short[, ]{{32766}, {-24988}}}, new short[][, ]{new short[, ]{{14866}, {0}, {32766}}}, new short[][, ]{new short[, ]{{-15544}}}, new short[][, ]{new short[, ]{{-32767}, {-9844}, {3079}, {32766}}}, new short[][, ]{new short[, ]{{-1}}}, new short[][, ]{new short[, ]{{17152}, {-11670}}}}, {new short[][, ]{new short[, ]{{0}, {16405}}}, new short[][, ]{new short[, ]{{22419}, {-5136}}}, new short[][, ]{new short[, ]{{20391}, {-4468}, {1}, {14888}}}, new short[][, ]{new short[, ]{{-32768}, {10}, {32767}, {-1}}}, new short[][, ]{new short[, ]{{19843}}}, new short[][, ]{new short[, ]{{-2863}, {-7829}, {1}, {32766}}}}, {new short[][, ]{new short[, ]{{0}, {10}, {-17229}}}, new short[][, ]{new short[, ]{{-11086}, {-32767}, {11151}, {32767}}}, new short[][, ]{new short[, ]{{21326}, {1}}}, new short[][, ]{new short[, ]{{10}, {-30241}, {32766}, {-32767}}}, new short[][, ]{new short[, ]{{1}, {5524}, {-8936}}}, new short[][, ]{new short[, ]{{6966}, {32766}, {10228}, {-9931}, {-29835}}}}, {new short[][, ]{new short[, ]{{-32767}, {13449}, {10}}}, new short[][, ]{new short[, ]{{-18223}}}, new short[][, ]{new short[, ]{{3810}, {0}, {0}, {-21208}}}, new short[][, ]{new short[, ]{{32366}, {32766}}}, new short[][, ]{new short[, ]{{-83}}}, new short[][, ]{new short[, ]{{0}, {0}}}}, {new short[][, ]{new short[, ]{{-32767}, {0}, {1}, {2844}}}, new short[][, ]{new short[, ]{{1}, {-10548}}}, new short[][, ]{new short[, ]{{-19689}, {1}, {32766}, {2498}, {13332}}}, new short[][, ]{new short[, ]{{10}, {32767}, {14363}}}, new short[][, ]{new short[, ]{{22077}, {32766}, {19678}, {-16461}, {32766}}}, new short[][, ]{new short[, ]{{1567}, {2}, {-6086}, {-32767}}}}}, 69, s_9[0]))
                        {
                            M38(var8, (ushort)(var15-- | 1));
                            short var16 = var15;
                            s_rt.Checksum("c_167", var16);
                        }

                        s_rt.Checksum("c_168", var13);
                        s_rt.Checksum("c_169", var14);
                        s_rt.Checksum("c_170", var15);
                    }
                    else
                    {
                        var8 ^= s_9[0];
                        var8 = var8;
                        var8 = var8;
                    }

                    ushort var17 = s_17--;
                    s_rt.Checksum("c_171", var17);
                }

                long[][] var18 = new long[][]{new long[]{-2075051747109869103L, 1866653020324556934L, 9223372036854775806L}, new long[]{2602493167615209883L, -9223372036854775807L}, new long[]{8280216372257752328L, 9099323642435393727L}, new long[]{0L, 1L}, new long[]{-4457441377854146932L, -9200479096141202908L}, new long[]{9223372036854775807L, 1L, 7304268054001546693L, -7649732128353390153L, 1700285919550920256L, 0L, -2420285902968231015L, 5357664131843792267L}};
                s_rt.Checksum("c_172", var18[0][0]);
            }

            s_rt.Checksum("c_173", var8);
        }
        finally
        {
            s_1 = M39(1UL, 1U);
        }

        long var19 = 7772384902374106328L;
        var19 = var19;
        s_rt.Checksum("c_177", var19);
        return 4294967294U;
    }

    static bool M37(ref long arg0, short[, ][][, ] arg1, sbyte arg2, sbyte arg3)
    {
        arg0 = arg0;
        arg2 = 14;
        if (s_23[0])
        {
            s_33 = 3690277061U;
            arg1 = arg1;
        }
        else
        {
            bool var0 = false && (arg2 != arg1[0, 0][0][0, 0]);
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
        if (s_3[0] < -1)
        {
            int var0 = -1433555422;
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
        {
            if (M41(-10))
            {
                ++M42();
            }
            else
            {
                arg1 = arg1;
                if (M41(27709))
                {
                    s_35 = 7088703865432684451L;
                    if (M41(arg0))
                    {
                        M42() = 4294967294U;
                        arg1 = arg1;
                    }
                    else
                    {
                        ref int var0 = ref arg1;
                        M41(8366);
                        long var1 = 6061911560257761272L;
                        M42();
                        arg1 = s_21;
                        ++var0;
                        var1 = var1++;
                        s_rt.Checksum("c_189", var0);
                        s_rt.Checksum("c_190", var1);
                    }

                    arg1 = arg1;
                }

                M41(arg0);
                M41(s_19);
                arg0 = (short)(arg0 % (byte)(s_34 | 1));
                arg0 = (short)(s_34 * M42());
            }
        }

        {
            {
                arg1 = arg1--;
                arg0 = s_30[0]++;
            }

            s_30 = new short[]{2, -1966, 1, 27266, 18123, -5788, 1, 13284, 0, 18921};
            sbyte var2 = (sbyte)M42();
            s_rt.Checksum("c_191", var2);
        }

        arg1 = arg1--;
        if (s_23[0])
        {
            if (s_23[0])
            {
                M41(arg0--);
                s_9[0] = (sbyte)M42();
                M42() = M42();
                arg0 = (short)M42();
                arg1 <<= (int)M42();
            }

            ref bool[][] var3 = ref s_36;
            s_rt.Checksum("c_192", var3[0][0]);
        }
        else
        {
            ushort var4 = 3020;
            M42();
            ref short var5 = ref arg0;
            ulong var6 = (ulong)(s_24 | M42());
            {
                ref sbyte var7 = ref s_9[0];
                short var8 = 15856;
                s_rt.Checksum("c_193", var7);
                s_rt.Checksum("c_194", var8);
            }

            arg0 = var5--;
            var5 >>= -2147483648;
            s_rt.Checksum("c_195", var4);
            s_rt.Checksum("c_196", var5);
            s_rt.Checksum("c_197", var6);
        }

        s_rt.Checksum("c_198", arg0);
        s_rt.Checksum("c_199", arg1);
        return new long[]{-1295988226537275091L, 9223372036854775807L, 1L, -2621539943863433122L, 3687468604783904753L, -4194265956950229295L, 9223372036854775806L};
    }

    static bool M41(short arg0)
    {
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
            bool var0 = false || (arg0 > (uint)(arg0 + 2967628336U));
            {
                ushort var1 = s_3[0];
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
        int var0 = s_12;
        var0 <<= 1965711740;
        var0 = var0++;
        s_rt.Checksum("c_185", var0);
        return ref s_33;
    }

    static byte M43(ref short arg0)
    {
        int[][] var0 = new int[][]{new int[]{1921100264}, new int[]{0}, new int[]{1730370135}, new int[]{22642782}, new int[]{-793182294}, new int[]{-1}, new int[]{1}, new int[]{-2105634838}, new int[]{1302024080}};
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
        {
            arg4[0] = -32768;
            arg1 = 17577;
            ref short var0 = ref arg4[0];
            M46(new ushort[]{534}, (uint)((ushort)(1 - (ushort)M46(new ushort[]{3456, 24952, 43255, 1, 61920}, s_13[0, 0])) * (int)M47(s_27--)));
            s_rt.Checksum("c_218", var0);
        }

        try
        {
            if (s_36[0][0])
            {
                arg0 |= s_37;
                byte var1 = 219;
                sbyte var2 = M49(s_3, arg3, 32766);
                uint[][] var3 = s_22;
                s_rt.Checksum("c_226", var1);
                s_rt.Checksum("c_227", var2);
                s_rt.Checksum("c_228", var3[0][0]);
            }
            else
            {
                M50(ref arg0);
            }
        }
        finally
        {
            if (arg2)
            {
                arg2 = arg2;
                arg3 = (int)M49(new ushort[]{0, 65534, 56570, 52465, 65535, 46983}, arg0--, (short)(9223372036854775806L % (ushort)(arg1++ | 1)));
            }
        }

        s_rt.Checksum("c_229", arg0);
        s_rt.Checksum("c_230", arg1);
        s_rt.Checksum("c_231", arg2);
        s_rt.Checksum("c_232", arg3);
        s_rt.Checksum("c_233", arg4[0]);
        return (ulong)(arg0 % (short)(arg4[0] | 1));
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
        long var0 = -9223372036854775807L;
        if (s_39)
        {
            arg0 = arg0;
            arg0 = (long)(6072 % (ushort)(s_17 | 1));
        }
        else
        {
            M48(s_25--);
            if (s_3[0] >= s_2[0]--)
            {
                s_20 = s_20;
                if ((short)(-2147483648 % (long)(arg0 | 1)) <= s_17)
                {
                    arg0 = arg0;
                    arg0 = s_14[0];
                    var0 = var0;
                    arg0++;
                    M48(s_25--);
                }
            }
            else
            {
                var0 = s_6;
            }

            {
                {
                    M48(1);
                    byte var1 = s_34--;
                    s_3 = new ushort[]{0};
                    s_rt.Checksum("c_212", var1);
                }

                if (s_39)
                {
                    ushort var2 = 38146;
                    s_rt.Checksum("c_213", var2);
                }

                arg0 = -1195125622908598097L;
            }
        }

        var0 |= var0;
        M48(s_34++);
        if (s_7)
        {
            M48(0);
        }
        else
        {
            {
                s_40[0] = new long[, ]{{0L, -2420203062460262570L, 1L}};
                M48(s_15--);
            }

            {
                var0 = 8405998828626439758L;
                var0 = arg0;
                try
                {
                    arg0 = (long)(4294967295U - (ushort)(s_41 ^ s_20[0]--));
                }
                finally
                {
                    var0 = -7565507856364849229L;
                }

                arg0 = arg0;
            }
        }

        s_rt.Checksum("c_214", arg0);
        s_rt.Checksum("c_215", var0);
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
        try
        {
            ref byte var0 = ref s_25;
            s_rt.Checksum("c_219", var0);
        }
        finally
        {
            uint var1 = 1U;
            s_rt.Checksum("c_220", var1);
        }

        try
        {
            s_24 = (short)(0UL & s_18);
        }
        finally
        {
            sbyte var2 = 17;
            s_rt.Checksum("c_221", var2);
        }

        s_22[0] = new uint[]{1099968913U, 4054913079U, 0U, 0U, 10U, 1U, 871899698U, 1U, 3439137607U, 2U};
        arg0 = ref arg0;
        s_rt.Checksum("c_222", arg0);
        return new ushort[, ]{{65534, 0, 0, 57866, 65535}, {0, 60829, 6449, 18064, 1}, {0, 0, 0, 57891, 49319}, {47696, 629, 0, 40034, 47045}, {63774, 22933, 4607, 48817, 45030}, {59509, 0, 65534, 65534, 8750}, {65534, 1, 26685, 22361, 4701}, {1, 65535, 1, 0, 59933}, {1, 0, 44096, 58598, 1}, {65535, 54828, 0, 65535, 22083}};
    }

    static short M51(int arg0, uint arg1, ref bool arg2, int arg3, uint arg4)
    {
        short var0 = 9946;
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
        ushort[] var0 = new ushort[]{0, 65534, 65535, 11030, 0, 0, 1, 46467};
        s_rt.Checksum("c_297", arg0);
        s_rt.Checksum("c_298", arg1);
        s_rt.Checksum("c_299", var0[0]);
        return 0;
    }

    static ulong M53(ref uint arg0)
    {
        short var0 = s_8--;
        bool[] var1 = new bool[]{true, false, true, true, true, true, true, false, true};
        var1[0] = true;
        if (var1[0])
        {
            if (s_14[0] >= -8065670836712051826L)
            {
                var1[0] = var1[0];
            }
            else
            {
                arg0 = 0U;
                int var2 = -1354268804;
                s_52 = new byte[]{254, 1, 79};
                s_rt.Checksum("c_300", var2);
            }

            uint var3 = arg0;
            s_rt.Checksum("c_301", var3);
        }

        if (var1[0])
        {
            s_2 = new sbyte[]{-92, -119};
            {
                M54(ref s_37, ref s_11);
                var1[0] = M85(ref s_10[0]);
                bool var4 = var1[0];
                s_42 = var0;
                var0 = var0;
                var0 = s_42--;
                s_rt.Checksum("c_562", var4);
            }

            ushort var5 = (ushort)M87();
            s_76 = var5;
            sbyte var6 = 75;
            {
                var6 = 99;
                bool var7 = var1[0];
                sbyte var8 = s_56;
                byte[] var9 = new byte[]{197, 246, 253, 249, 234, 0, 127, 141, 239, 255};
                s_rt.Checksum("c_563", var7);
                s_rt.Checksum("c_564", var8);
                s_rt.Checksum("c_565", var9[0]);
            }

            if (var1[0])
            {
                M88();
                var0 = 3661;
            }

            if (s_53[0, 0, 0])
            {
                sbyte var10 = s_16[0, 0];
                s_41 = (ulong)(s_97[0] | s_77);
                s_rt.Checksum("c_566", var10);
            }

            s_rt.Checksum("c_567", var5);
            s_rt.Checksum("c_568", var6);
        }
        else
        {
            arg0 = s_67--;
        }

        if (var1[0])
        {
            uint var11 = 4007882522U;
            bool var12 = var1[0];
            ulong var13 = (ulong)M88();
            sbyte var14 = (sbyte)M79(1, new sbyte[]{2, 0, 15, 127, 1, -55, -127, -52, -127, 58}, 1U);
            s_102 = var0--;
            s_rt.Checksum("c_569", var11);
            s_rt.Checksum("c_570", var12);
            s_rt.Checksum("c_571", var13);
            s_rt.Checksum("c_572", var14);
        }

        {
            var1[0] = false;
        }

        M91();
        bool[] var15 = var1;
        s_rt.Checksum("c_574", arg0);
        s_rt.Checksum("c_575", var0);
        s_rt.Checksum("c_576", var1[0]);
        s_rt.Checksum("c_577", var15[0]);
        return (ulong)M54(ref s_83[0, 0], ref s_95);
    }

    static int M54(ref int arg0, ref long arg1)
    {
        {
            M55(s_22[0], (sbyte)M81());
            if (s_53[0, 0, 0])
            {
                {
                    bool var0 = s_7;
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
        long[] var0 = s_14;
        bool[] var1 = new bool[]{false};
        {
            {
                if (var1[0])
                {
                    M56();
                    var0 = new long[]{-9223372036854775808L, -2L, -9223372036854775808L, -5486155620693117660L, -2L, -8045146591217874582L, 9122386105440711226L, -2L};
                }

                uint[] var2 = arg0;
                {
                    sbyte[][] var3 = new sbyte[][]{new sbyte[]{-78, 0, -60, -127}, new sbyte[]{0}, new sbyte[]{-127, 0, 1, 127}, new sbyte[]{0}, new sbyte[]{124, -116, 1, -118}};
                    var2[0] ^= (uint)M56();
                    int var4 = (int)M56();
                    byte var5 = (byte)(s_20[0]-- & s_17++);
                    ++arg0[0];
                    s_rt.Checksum("c_325", var3[0][0]);
                    s_rt.Checksum("c_326", var4);
                    s_rt.Checksum("c_327", var5);
                }

                M56();
                s_57 = 0UL;
                s_rt.Checksum("c_328", var2[0]);
            }

            try
            {
                ulong var6 = 18084654172577678392UL;
                var0 = var0;
                ref byte[] var7 = ref s_20;
                s_rt.Checksum("c_329", var6);
                s_rt.Checksum("c_330", var7[0]);
            }
            finally
            {
                {
                    s_58 = M59(M71(), ref s_98, ref s_5);
                    M89((int)M81());
                    s_86[0][0][0] = new ulong[]{11572426010455992696UL, 10941028364506132568UL, 1826257423108565308UL};
                    var1[0] = M75(ref s_55, s_87, true, ref s_4[0][0], new bool[]{true, false, false, false, false, false, true, false, true, true});
                }

                try
                {
                    arg0 = s_22[0];
                    ref byte var8 = ref s_58;
                    {
                        M86(575490270, 58596, s_98[0, 0], var0[0], ref var0[0], M68(arg1), arg0[0]);
                        long var9 = s_84[0];
                        long[] var10 = new long[]{-9223372036854775808L, -9223372036854775807L, -9223372036854775807L, 4201685713794396246L, -7101189006756891202L, 2884501921213726578L};
                        uint[] var11 = arg0;
                        arg0[0] = M88();
                        s_rt.Checksum("c_549", var9);
                        s_rt.Checksum("c_550", var10[0]);
                        s_rt.Checksum("c_551", var11[0]);
                    }

                    s_rt.Checksum("c_552", var8);
                }
                finally
                {
                    s_99 = arg1++;
                    s_20 = s_64;
                    var0 = var0;
                    short var12 = 1;
                    s_rt.Checksum("c_553", var12);
                }

                {
                    arg0 = arg0;
                    ulong var13 = (ulong)M70();
                    s_100 = new ulong[]{2830587306523321224UL};
                    s_rt.Checksum("c_554", var13);
                }

                var1 = var1;
                var1[0] = s_48;
            }
        }

        s_rt.Checksum("c_555", arg0[0]);
        s_rt.Checksum("c_556", arg1);
        s_rt.Checksum("c_557", var0[0]);
        s_rt.Checksum("c_558", var1[0]);
        return new ushort[][]{new ushort[]{1, 0}, new ushort[]{2}};
    }

    static ref ulong M56()
    {
        ref short var0 = ref s_30[0];
        if (s_4[0][0]-- < var0--)
        {
            var0 = var0--;
            s_53 = new bool[,, ]{{{false, false, false, false, false, false, true, true, true, true}, {true, false, true, true, true, true, true, false, true, true}, {false, true, false, false, false, false, true, true, true, true}, {false, true, true, true, false, true, true, false, true, true}, {false, true, false, true, false, true, true, true, true, false}}, {{false, false, false, false, true, true, true, true, false, true}, {true, true, false, false, false, false, true, false, true, false}, {false, true, true, false, false, true, false, false, true, false}, {true, true, false, true, true, true, true, false, true, true}, {false, false, true, true, false, false, false, false, false, false}}, {{true, false, false, false, false, false, false, false, true, true}, {true, true, false, true, true, false, true, true, false, false}, {true, true, false, true, true, false, false, true, false, false}, {false, true, true, false, true, false, false, false, false, false}, {true, false, true, true, false, true, true, true, false, false}}, {{true, false, false, false, false, false, true, false, false, true}, {true, false, false, false, false, false, true, false, true, true}, {true, false, false, true, false, true, true, false, false, true}, {true, true, false, true, true, true, true, true, true, false}, {true, false, false, true, false, false, false, false, false, false}}};
            M57(5474859200122226104L);
            M58(new sbyte[]{-96, 86, 0});
            byte var1 = 216;
            {
                short var2 = (short)(2247 ^ s_11++);
                s_rt.Checksum("c_317", var2);
            }

            M57(s_6--);
            {
                if (s_46)
                {
                    var1 = var1;
                    M57(-9223372036854775807L);
                    long[, ] var3 = s_40[0];
                    s_rt.Checksum("c_318", var3[0, 0]);
                }
                else
                {
                    var1 = 172;
                }

                uint var4 = s_13[0, 0]--;
                var1 = var1--;
                s_rt.Checksum("c_319", var4);
            }

            M58(new sbyte[]{-111, -1, 1, -86, -2, 1, 0, -127, 0, 0});
            ushort var5 = s_17;
            s_rt.Checksum("c_320", var1);
            s_rt.Checksum("c_321", var5);
        }

        M58(s_2);
        s_56 = s_2[0]--;
        M57(0L);
        var0 = var0;
        bool var6 = true;
        if (var6)
        {
            var6 = var6;
        }
        else
        {
            var0 = var0;
        }

        if (var6)
        {
            {
                short[][][] var7 = new short[][][]{new short[][]{new short[]{16766}, new short[]{32766}, new short[]{1, 32767}}, new short[][]{new short[]{5590}, new short[]{1}}};
                var6 = var6;
                M57(s_11--);
                s_rt.Checksum("c_322", var7[0][0][0]);
            }

            s_18 = 1;
        }

        s_rt.Checksum("c_323", var0);
        s_rt.Checksum("c_324", var6);
        return ref s_10[0];
    }

    static long[] M57(long arg0)
    {
        arg0 = arg0;
        ushort[] var0 = M58(new sbyte[]{-10, 19, 127, -85, 117, 126, -85, -1, 72});
        if (s_46)
        {
            var0 = M58(new sbyte[]{126, 1, 103, 0, -128, -128, 10, 100, 50});
            {
                {
                    var0 = new ushort[]{1, 50458, 0, 9043, 55998};
                    int[] var1 = s_4[0];
                    s_rt.Checksum("c_307", var1[0]);
                }

                arg0 = arg0;
                M58(new sbyte[]{-18, 1, 10, 82, 93, 53, 125, -2, -49, 1});
                {
                    s_40[0][0, 0] = arg0;
                    s_43 = s_13[0, 0]--;
                }

                if (s_7)
                {
                    var0[0] = var0[0]--;
                }

                uint var2 = 1786118333U;
                s_rt.Checksum("c_308", var2);
            }

            s_5 = 0UL;
            {
                if (s_7)
                {
                    arg0 = arg0--;
                    var0 = var0;
                }
                else
                {
                    s_54 = s_54;
                    int[][, ] var3 = new int[][, ]{new int[, ]{{1210005803, -979467337, -196401287, 2147483646, 1277335719, -2147483648, -1430519326, 1, -627794847, 10}}, new int[, ]{{-462292643, 1596573795, 2139135581, -1165167437, 2041124143, 2147483647, 10, 0, 1, -1}, {-431637094, -1870506385, 0, 2028813830, 1, 1, 10, -1121149880, 526645718, 1}}, new int[, ]{{1642320878, -2, -2147483647, 257220512, -75954405, 2147483647, 427020908, -2127116693, 33505052, 2147483646}}, new int[, ]{{-2, -1527236517, 0, 2147483646, 2118033718, 1838639951, 467608430, 2119977552, 2, -1376577310}, {590723180, -1383302549, 1, 2147483646, -10, -1741342557, -10, -2147483647, 2147483646, 2147483646}}, new int[, ]{{884182795, 2147483646, 2147483646, 1726416410, -2147483648, 1, 2147483646, -1, 1, 0}}, new int[, ]{{-1923276546, -1810198339, -2147483648, -2147483648, -364482896, -1541379186, 1, 0, -486076789, -10}}, new int[, ]{{1282779971, 2147483646, -2147483647, 1121443138, -321012102, -2, 1756661142, 294631931, 618748152, -1277761588}}, new int[, ]{{0, -594633161, 2, -165494717, -1961473745, 1, -556766813, 2147483646, 390029229, 42568581}, {-2147483647, -1713059501, 2, 1226582350, 0, 1721284439, 793178494, 0, 1, -706651028}}, new int[, ]{{-2147483648, 10, -2147483647, -718228108, -2147483648, 1, 2147483646, -490578447, -1419425687, -1091571911}, {-2064398844, 2147483647, 0, -585757078, -1, 1117198429, 90032537, 1148461169, -1898857928, -1388139665}}, new int[, ]{{1746211997, -31998301, 1561515100, -2099526638, 1020712595, 1, -774459709, -1402255054, -861108161, -1497142237}, {-577670460, -790916555, 1898289617, 138754087, 1805557773, 2147483646, 2147483646, 1437978201, 511736959, 1}}};
                    M58(new sbyte[]{-128, -128, 126, -2, -10, 127});
                    arg0 |= arg0;
                    s_rt.Checksum("c_309", var3[0][0, 0]);
                }

                short[] var4 = s_54;
                s_rt.Checksum("c_310", var4[0]);
            }

            arg0 = -1480322594707605622L;
            long var5 = arg0;
            s_rt.Checksum("c_311", var5);
        }
        else
        {
            {
                var0 = new ushort[]{1, 376, 53487, 65534};
                if (s_36[0][0])
                {
                    ref bool var6 = ref s_23[0];
                    s_55 = new bool[]{false, false, true, false, true, false, true, false, false, true};
                    var6 = false;
                    s_rt.Checksum("c_312", var6);
                }
                else
                {
                    var0 = new ushort[]{65535, 0, 11778};
                    int var7 = -1;
                    s_rt.Checksum("c_313", var7);
                }

                arg0 = arg0;
                ref byte[, ] var8 = ref s_45[0, 0][0][0];
                s_rt.Checksum("c_314", var8[0, 0]);
            }
        }

        var0 = var0;
        M58(new sbyte[]{59, 1, -19, 30, 0, 109});
        var0[0] = s_17;
        var0[0] = s_3[0];
        s_rt.Checksum("c_315", arg0);
        s_rt.Checksum("c_316", var0[0]);
        return s_14;
    }

    static ushort[] M58(sbyte[] arg0)
    {
        {
            s_50 = new ushort[][]{new ushort[]{2497, 2, 12819, 39581}, new ushort[]{32380, 65535, 20199, 42307}, new ushort[]{29221, 1, 0, 0, 65535}, new ushort[]{51803, 1, 57618, 9955, 65535, 0, 9512, 61182, 62401}, new ushort[]{16481}, new ushort[]{65036, 65534, 380, 65535, 0, 47248, 0, 1, 11153, 13216}};
            s_11++;
            uint var0 = 3760342882U;
            s_rt.Checksum("c_302", var0);
        }

        if (true || s_23[0])
        {
            arg0 = arg0;
            arg0[0] = arg0[0];
            bool var1 = (0L ^ s_15) != -30976;
            s_rt.Checksum("c_303", var1);
        }
        else
        {
            if (s_7)
            {
                {
                    ref int var2 = ref s_38[0][0];
                    s_rt.Checksum("c_304", var2);
                }
            }
            else
            {
                uint var3 = s_22[0][0]--;
                s_rt.Checksum("c_305", var3);
            }
        }

        arg0[0] = arg0[0];
        s_rt.Checksum("c_306", arg0[0]);
        return new ushort[]{14154, 14216, 0, 24818, 0, 0, 63899, 0, 6115, 65534};
    }

    static byte M59(ushort[][] arg0, ref ulong[, ] arg1, ref ulong arg2)
    {
        if (s_46)
        {
            arg0[0][0] = 1;
            {
                s_59 = s_14;
                arg1 = arg1;
                if (s_20[0]-- < s_14[0]--)
                {
                    arg2 = s_47;
                    if (s_55[0])
                    {
                        arg0 = arg0;
                        bool var0 = true;
                        var0 = var0;
                        s_rt.Checksum("c_331", var0);
                    }
                    else
                    {
                        arg1[0, 0] = arg2;
                        M60(arg0[0][0]);
                        arg0[0][0] = arg0[0][0];
                    }

                    s_9[0] = 0;
                    {
                        arg2 = M61();
                        byte[][] var1 = new byte[][]{new byte[]{176, 0, 0, 1, 68}, new byte[]{100, 154, 254, 118, 131}, new byte[]{1, 86, 255, 78}};
                        ref long var2 = ref s_79;
                        M69((sbyte)(62505 & M64()), 0, 0UL, ref s_16[0, 0], 0, M69((sbyte)M64(), s_32[0, 0, 0]--, s_47, ref s_96, 187, true, s_84[0]), var2);
                        short var3 = 32767;
                        arg2 = arg2;
                        var1 = new byte[][]{new byte[]{0, 38, 0}, new byte[]{10, 119, 173, 155, 1}, new byte[]{1, 1, 1, 184, 94, 0, 1}, new byte[]{83, 116, 81, 44, 0, 35, 37}, new byte[]{0, 0, 254, 1, 1, 0, 69, 1}, new byte[]{124, 200, 0, 255, 70}, new byte[]{243, 1, 244, 255}, new byte[]{0, 78, 1}};
                        s_rt.Checksum("c_538", var1[0][0]);
                        s_rt.Checksum("c_539", var2);
                        s_rt.Checksum("c_540", var3);
                    }
                }
                else
                {
                    M63() = 1397312935360913561L;
                    short var4 = s_74++;
                    s_rt.Checksum("c_541", var4);
                }

                uint var5 = M82()++;
                M70();
                s_8 = (short)(s_2[0] + M88());
                s_rt.Checksum("c_542", var5);
            }

            if (s_53[0, 0, 0])
            {
                int[, ][, ] var6 = new int[, ][, ]{{new int[, ]{{-911545550, -2062699416, -2147483648}}, new int[, ]{{1621064863, 2147483646, 2147483646}}, new int[, ]{{0, -2, -11239586}}, new int[, ]{{744739174, 0, -1006879957}}, new int[, ]{{-154827167, -2147483648, 0}}, new int[, ]{{2, -1875412878, 1466710263}}, new int[, ]{{0, 0, 2147483646}}, new int[, ]{{975804503, -1, -2101242133}}}, {new int[, ]{{0, 0, 2147483646}}, new int[, ]{{2147483647, 1, -1418745480}}, new int[, ]{{2147483647, -368540800, 1781573291}}, new int[, ]{{0, 1, 0}}, new int[, ]{{-2147483647, -2, 0}}, new int[, ]{{794232565, 2147483647, 290134421}}, new int[, ]{{-2147483648, -592917221, 2147483647}}, new int[, ]{{1, -508055177, 1}}}, {new int[, ]{{0, 895846758, 1777602265}}, new int[, ]{{-664723385, 1295638634, -1328478730}}, new int[, ]{{0, 968430458, -2}}, new int[, ]{{2147483646, 0, -556331827}}, new int[, ]{{2147483647, -976177852, 670418052}}, new int[, ]{{22124928, 2070286974, 1}}, new int[, ]{{163264353, 2147483647, 1}}, new int[, ]{{1, -2147483648, -2147483648}}}, {new int[, ]{{-266719208, 0, 886617352}}, new int[, ]{{-130678212, -708471612, -300004426}}, new int[, ]{{0, -1189080495, -1817714714}}, new int[, ]{{1, -1182583745, 1480794908}}, new int[, ]{{-2147483647, -953108423, -2091203271}}, new int[, ]{{-1547639314, -1, 0}}, new int[, ]{{2, 2147483647, -936148474}}, new int[, ]{{54882183, -345785874, -379531041}}}, {new int[, ]{{2147483646, -2, 667723412}}, new int[, ]{{-2147483648, -351928877, -1806077057}}, new int[, ]{{-312921630, 1, 0}}, new int[, ]{{0, 0, 0}}, new int[, ]{{-165526099, 2116658193, -1}}, new int[, ]{{134652117, 604232017, 2062627900}}, new int[, ]{{1, 996715089, 940743483}}, new int[, ]{{842847654, 282527936, -2147483647}}}, {new int[, ]{{2147483646, -1440551262, 0}}, new int[, ]{{-2147483648, -1391084770, -232324808}}, new int[, ]{{2147483646, 1234247068, -11394290}}, new int[, ]{{-509228083, 1146969321, -510819060}}, new int[, ]{{0, 2147483646, 1300875500}}, new int[, ]{{-2147483648, -619290175, 0}}, new int[, ]{{0, -2, 1986176330}}, new int[, ]{{1406420173, -10, 1317124443}}}, {new int[, ]{{1872942192, 10, 2112078135}}, new int[, ]{{953335507, 0, 1497656776}}, new int[, ]{{0, -2147483648, 0}}, new int[, ]{{1721420997, 2147483646, -881100874}}, new int[, ]{{-1162008718, 549424322, -898314578}}, new int[, ]{{-10, 1, 1858106636}}, new int[, ]{{1566402004, 2147483647, -2147483648}}, new int[, ]{{1, -1358429780, -2114366539}}}, {new int[, ]{{1, 627321481, -1}}, new int[, ]{{1, 2147483646, 1}}, new int[, ]{{-617060826, 803537532, 25632790}}, new int[, ]{{-2029058891, -633307404, -10}}, new int[, ]{{-587904625, -2147483647, 1}}, new int[, ]{{2147483646, 476705423, 2147483647}}, new int[, ]{{-1373780173, -1699219928, 2147483646}}, new int[, ]{{-2147483648, 1359835128, 1964582294}}}, {new int[, ]{{-1917121400, -639908425, 2147483646}}, new int[, ]{{1, 2147483647, 1421037320}}, new int[, ]{{10, 1738866790, 664401336}}, new int[, ]{{1950814983, 2147483647, 1737078626}}, new int[, ]{{0, 10, 0}}, new int[, ]{{-919823257, -1089713367, 2147483646}}, new int[, ]{{1948470670, 400140658, 0}}, new int[, ]{{-2147483648, -2147483648, 2147483647}}}};
                sbyte[] var7 = new sbyte[]{-104, -82, 82, 2};
                s_rt.Checksum("c_543", var6[0, 0][0, 0]);
                s_rt.Checksum("c_544", var7[0]);
            }
            else
            {
                arg0[0] = arg0[0];
                ++s_37;
                arg0[0] = arg0[0];
            }

            arg1 = arg1;
            M82();
        }

        byte var8 = 255;
        s_rt.Checksum("c_545", arg0[0][0]);
        s_rt.Checksum("c_546", arg1[0, 0]);
        s_rt.Checksum("c_547", arg2);
        s_rt.Checksum("c_548", var8);
        return var8;
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
        s_27 = (long)(s_50[0][0]-- ^ s_56);
        try
        {
            if (M62())
            {
                uint var0 = 4116611811U;
                if (s_36[0][0])
                {
                    var0 = var0;
                    ref sbyte var1 = ref s_26;
                    s_rt.Checksum("c_334", var1);
                }
                else
                {
                    ushort var2 = s_17--;
                    --var2;
                    var0 = s_13[0, 0];
                    ulong[] var3 = new ulong[]{14642226659287603301UL, 5406846881773099441UL, 8580664257784071388UL};
                    var3 = var3;
                    var2 = var2--;
                    var0 = var0;
                    s_rt.Checksum("c_335", var2);
                    s_rt.Checksum("c_336", var3[0]);
                }

                ref sbyte var4 = ref s_26;
                var4 &= var4;
                s_rt.Checksum("c_337", var0);
                s_rt.Checksum("c_338", var4);
            }
            else
            {
                if (M62())
                {
                    uint var5 = 0U;
                    s_rt.Checksum("c_339", var5);
                }

                M63();
                s_93 = s_38[0][0];
                bool var6 = true;
                if (s_61)
                {
                    byte[] var7 = new byte[]{1, 243};
                    s_rt.Checksum("c_524", var7[0]);
                }
                else
                {
                    long var8 = -8420470293647435217L;
                    s_rt.Checksum("c_525", var8);
                }

                if (s_53[0, 0, 0])
                {
                    {
                        ulong var9 = (ulong)M87();
                        var6 = s_61;
                        short var10 = 1;
                        {
                            long var11 = -2L;
                            M77(new ulong[][]{new ulong[]{4229908695040581407UL}, new ulong[]{0UL, 1UL}, new ulong[]{1UL}, new ulong[]{0UL, 3297220204646447542UL, 2UL}, new ulong[]{7587145483848833474UL, 0UL, 0UL, 0UL}, new ulong[]{1UL, 8641322995759975201UL, 2370098521720853033UL, 9317462873615097735UL}, new ulong[]{1UL, 3525024811971989854UL, 1UL, 1UL}, new ulong[]{7848032457987699662UL, 3680610322926918202UL}, new ulong[]{0UL, 7408890873873950684UL, 6300142016980172176UL, 1UL}}, (sbyte)(var9 + 64243), new ushort[]{1, 21699, 4994, 49256, 35936, 11444});
                            s_rt.Checksum("c_526", var11);
                        }

                        s_rt.Checksum("c_527", var9);
                        s_rt.Checksum("c_528", var10);
                    }

                    s_21 = -1;
                }

                s_rt.Checksum("c_529", var6);
            }

            s_59 = new long[]{-1795383337977189635L, -1L};
            s_94 = false;
        }
        finally
        {
            s_43 = s_13[0, 0]--;
            M73(-1162756589, (sbyte)(1L ^ s_74), ref s_9[0], true, s_59[0], 4294967295U) = s_10;
            if (M85(ref s_90))
            {
                if (s_94)
                {
                    ushort var12 = 25669;
                    long var13 = -2L;
                    s_rt.Checksum("c_530", var12);
                    s_rt.Checksum("c_531", var13);
                }
            }
            else
            {
                int var14 = 10;
                var14 = 2;
                {
                    var14 = var14++;
                }

                var14 = var14;
                ulong var15 = 629639324415425653UL;
                ref int var16 = ref var14;
                {
                    M82() = 900212934U;
                    s_95 = s_77;
                    var14 = var14;
                }

                short var17 = s_74++;
                var15++;
                s_rt.Checksum("c_532", var14);
                s_rt.Checksum("c_533", var15);
                s_rt.Checksum("c_534", var16);
                s_rt.Checksum("c_535", var17);
            }
        }

        M69(s_9[0]--, (int)(-18746 % (long)(s_77 | 1)), (ulong)M88(), ref s_78[0], s_60, false, -9223372036854775808L);
        s_30 = s_30;
        ushort var18 = s_88[0];
        s_96 = (sbyte)M88();
        {
            M81();
            var18 = 38241;
            M79(127, new sbyte[]{-113, 126, 1, 1, -107, -78, -127, 81}, 1372681422U);
            ulong[, ] var19 = new ulong[, ]{{17138809357939400113UL, 8007806920452224330UL, 10711192665565577340UL, 18446744073709551615UL, 16051193110696041865UL, 5199661719705579602UL, 1UL, 0UL, 4439515712086549174UL}, {17233879962964242763UL, 13617941636985300410UL, 14816131294305873881UL, 377840201118616238UL, 18446744073709551615UL, 0UL, 10667212963550604094UL, 8737771480993109287UL, 1UL}, {18446744073709551614UL, 0UL, 1UL, 1UL, 18446744073709551614UL, 1UL, 14395658642273602365UL, 1UL, 1UL}, {18446744073709551614UL, 18446744073709551615UL, 1UL, 9193224226850141182UL, 1UL, 15161920774694464274UL, 2690272869805294882UL, 3684273658229886295UL, 10340428790625722413UL}, {1517018729041593280UL, 0UL, 1600564101465999755UL, 5083482054603080269UL, 12360330902331005494UL, 5779267221531900807UL, 9604180890205154606UL, 1UL, 18446744073709551614UL}, {7673291058216605220UL, 12995735640557790031UL, 1UL, 4858118157513334261UL, 0UL, 1UL, 6049222921797245953UL, 1UL, 18446744073709551615UL}, {1UL, 1UL, 4057861059982169159UL, 1938699307454030231UL, 9357319411203170717UL, 8069108045831100704UL, 3557773697025153091UL, 3403220272587979334UL, 8944567368070198231UL}};
            s_rt.Checksum("c_536", var19[0, 0]);
        }

        s_42++;
        M81();
        M73((int)M70(), s_2[0], ref s_72[0], M66(), s_91++, 0U) = s_86[0][0][0];
        if (s_53[0, 0, 0])
        {
            s_97 = new short[]{1, 32767, 30914, -9128, 32766, -22290};
        }

        s_rt.Checksum("c_537", var18);
        return 7087561305359853504UL;
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
        if ((ulong)(s_19 - s_21--) < (byte)(s_25 & 0U))
        {
            {
                s_38 = new int[][]{new int[]{1447202408, 1268200912}, new int[]{2147483647, 1, 291915581}, new int[]{-2129385114, -2133462848}, new int[]{1024231524, -1, 1}, new int[]{10}, new int[]{1, 2147483646}, new int[]{0}, new int[]{1141840825}, new int[]{1}};
                uint var0 = 275330216U;
                var0 = var0++;
                var0 = 1274440952U;
                s_60 = s_58;
                s_50 = new ushort[][]{new ushort[]{2}, new ushort[]{1}, new ushort[]{60105}, new ushort[]{65534}, new ushort[]{63458}, new ushort[]{1}, new ushort[]{31851}, new ushort[]{27586}, new ushort[]{0}, new ushort[]{16437}};
                M65((sbyte)M76(ref s_76, M68((sbyte)M81()), true));
                try
                {
                    try
                    {
                        int var1 = -2143657894;
                        s_rt.Checksum("c_504", var1);
                    }
                    finally
                    {
                        try
                        {
                            short var2 = 31288;
                            s_rt.Checksum("c_505", var2);
                        }
                        finally
                        {
                            s_19 = 32767;
                        }
                    }
                }
                finally
                {
                    var0 = 1U;
                    var0 = (uint)M79(-81, M68(0), var0);
                }

                s_rt.Checksum("c_506", var0);
            }

            sbyte var3 = -90;
            var3 = var3;
            M69((sbyte)M77(s_86[0][0], 34, new ushort[]{22797, 55688}), (int)M67(new ushort[,, ]{{{31237, 1, 2956, 62487, 1, 65534, 65534, 46379, 65534}, {0, 51829, 23442, 65535, 63931, 65535, 57683, 39939, 1}, {20095, 10242, 23030, 0, 65534, 53522, 1, 0, 65534}, {0, 1, 55326, 65534, 0, 8071, 22057, 51108, 1}, {15911, 18945, 13933, 52889, 42665, 8851, 27718, 0, 1}, {60385, 1, 35814, 46734, 42558, 65534, 0, 1, 65534}}, {{16811, 2, 53399, 65535, 39976, 65535, 65534, 44081, 1}, {24889, 58336, 58484, 1, 1, 1, 10, 0, 0}, {0, 1, 1, 65535, 65535, 21503, 65534, 24017, 18175}, {56453, 65534, 2003, 1, 19006, 44036, 8592, 1, 25812}, {1, 0, 0, 0, 24137, 5478, 0, 65535, 22183}, {0, 65534, 0, 14990, 1, 1, 65535, 1, 2507}}, {{0, 30526, 65535, 43387, 1, 65534, 4971, 10161, 24912}, {0, 65534, 0, 36595, 9574, 0, 10, 65535, 23148}, {0, 10861, 0, 7681, 54737, 65535, 40219, 21400, 40324}, {56661, 0, 14567, 31355, 1, 17105, 1, 53798, 0}, {0, 34425, 65534, 0, 49455, 65534, 0, 65535, 1583}, {1, 1, 1, 1, 0, 20728, 3200, 53760, 3362}}, {{1, 1, 39739, 0, 1, 48935, 1, 1, 1}, {60561, 23374, 0, 23506, 38372, 0, 20050, 55253, 27427}, {14040, 39560, 32114, 0, 1, 0, 36668, 11985, 1}, {21808, 65534, 0, 48420, 1, 0, 65534, 0, 0}, {1, 49839, 0, 61645, 18252, 58120, 1, 39580, 65534}, {0, 34004, 65535, 41541, 49211, 65535, 21643, 168, 0}}, {{1, 10, 1, 31091, 52541, 0, 42468, 1, 45826}, {59358, 19078, 40921, 44588, 2, 65535, 52423, 28451, 0}, {32516, 0, 0, 61980, 18732, 1, 47685, 0, 1032}, {16213, 25160, 0, 1, 0, 36994, 284, 0, 65534}, {1, 58656, 0, 10, 64164, 65535, 65534, 0, 65534}, {1, 7998, 0, 65535, 0, 20778, 0, 26810, 20863}}}), 2693201272117539916UL, ref s_72[0], 1, (ushort)((byte)M70() | s_33) <= 1, (long)((ulong)((int)M82() ^ s_74--) + s_66--)) = s_55[0];
            ref long var4 = ref s_6;
            {
                M72();
                ulong var5 = s_41;
                bool var6 = true;
                s_88 = new ushort[]{65535, 42486, 1};
                ulong var7 = (ulong)(12572 + (sbyte)M79(1, M68(var3), 4034984066U));
                var5 = (ulong)M76(ref s_76, M68(var3++), false);
                s_rt.Checksum("c_507", var5);
                s_rt.Checksum("c_508", var6);
                s_rt.Checksum("c_509", var7);
            }

            s_89 = s_89;
            M81();
            s_rt.Checksum("c_510", var3);
            s_rt.Checksum("c_511", var4);
        }

        M74(ref s_9[0]);
        if (s_53[0, 0, 0])
        {
            if (s_23[0])
            {
                s_78 = new sbyte[]{-104};
                ushort var8 = 0;
                var8 = (ushort)M70();
                var8 = var8;
                s_rt.Checksum("c_512", var8);
            }

            s_21 = (int)M67(new ushort[,, ]{{{4789}, {24664}, {1}}, {{0}, {18440}, {65535}}, {{1}, {1}, {0}}, {{5475}, {1}, {15446}}, {{15557}, {0}, {2}}, {{0}, {49695}, {0}}});
            if (M75(ref s_23, (long)M76(ref s_82, s_2, s_7) != s_26--, s_36[0][0], ref s_38[0][0], new bool[]{true, true, false, false, false, true, true, false, false}))
            {
                s_90 = s_41;
                int var9 = 0;
                try
                {
                    M82() = s_1[0]++;
                }
                finally
                {
                    M78();
                }

                M69((sbyte)M67(new ushort[,, ]{{{26102}, {40297}, {4278}, {10}, {1}, {31334}}, {{1}, {1}, {10}, {10}, {0}, {4441}}, {{65534}, {1}, {1746}, {39726}, {1}, {5900}}, {{53116}, {65535}, {4891}, {0}, {1}, {65534}}, {{8157}, {44057}, {1}, {65534}, {27735}, {0}}, {{0}, {1}, {65534}, {16145}, {58265}, {65535}}}), var9++, M89(var9--), ref s_69, (byte)(s_24-- + -2828656759469958038L), true, M70()) = s_39;
                s_rt.Checksum("c_515", var9);
            }
        }
        else
        {
            M86(675664635, (ushort)M74(ref s_16[0, 0]), s_70, (long)M89((int)M89(s_4[0][0]++)), ref s_27, new sbyte[]{0, -10, 1, 10, -69, 0, 112, -2, -59}, 726141961U);
        }

        s_91 = s_35--;
        s_68 = 1U;
        if (M66())
        {
            short var10 = s_8++;
            s_rt.Checksum("c_516", var10);
        }
        else
        {
            s_85[0] = (ushort)M81();
            if (M75(ref s_36[0], false, M80(-9177143188118630577L), ref s_83[0, 0], M90(ref s_67, ref s_26, ref s_88[0])))
            {
                s_40 = new long[][, ]{new long[, ]{{9223372036854775806L, 9223372036854775807L, 9223372036854775807L, 6540333009579763213L, -5648924889323462471L}, {-9223372036854775808L, -2283572551390587465L, 8267943724047698753L, 0L, 10L}}, new long[, ]{{-826617725961065158L, -2163944019647441704L, 3006369208280464927L, 1L, 8785581998411564333L}, {1L, 2671475897884397707L, 2L, -6606952071667654395L, 9223372036854775807L}, {1929753141081187109L, -5452666327753375641L, 1L, 0L, -4661057276480854241L}, {0L, 0L, 0L, 3634447307359550029L, -8383849251899930981L}}, new long[, ]{{-9223372036854775807L, -2302997706760791594L, 9223372036854775806L, 8492567306069345674L, -9223372036854775807L}, {6489166924552725174L, 9223372036854775806L, -8335145657160562962L, 0L, 1L}, {5526624741372058467L, 9223372036854775807L, 0L, 6911326622167619032L, 10L}, {5714595823190409580L, -3990267431929015147L, 1L, -6133459792389407050L, 9223372036854775806L}, {8095690846884118669L, 7554165695043911162L, -9223372036854775807L, 9223372036854775806L, 0L}}, new long[, ]{{342069483927579115L, 1L, 1789301311027668059L, -9223372036854775807L, -296772116803683079L}, {-1941354832701768001L, 6786799633080120911L, -3267170789559839418L, 2L, -9223372036854775807L}, {1L, -1220320918438845147L, -1L, 1L, 4772476534963146804L}}, new long[, ]{{-2369547248677584450L, 218743770081878158L, -60652653825426046L, -2134413670629260416L, 7413287347130130375L}, {0L, 4447256556026982173L, -9223372036854775808L, -55882368209144370L, 2674011502952457070L}}, new long[, ]{{-1292207135359577903L, -8944986846396819331L, -9223372036854775808L, -1L, 0L}, {1L, -7911654831500139571L, -2543137965086175603L, -2L, 4981538088539266688L}}, new long[, ]{{-2399971923280035539L, 1058732530084939995L, -8764984250481647264L, -2819842722459957623L, -2031629483220739940L}}, new long[, ]{{0L, -5468736047265247756L, 0L, 1L, 1L}, {6551476929338288287L, 10L, 9223372036854775806L, -3379223284095912816L, -9223372036854775808L}, {0L, -1L, 3545953195234354434L, -3516076894217083679L, -10L}, {8750320819735509832L, 5106921329240477140L, -3293455685513436868L, -9223372036854775808L, -9223372036854775808L}}, new long[, ]{{-4970884638319833313L, -9223372036854775808L, 1L, -1964451144462791553L, -9223372036854775808L}, {0L, 1L, 1L, -2L, 9223372036854775807L}, {8356648769058654416L, 8545135831581848768L, -1187656345933633925L, -9223372036854775808L, -1L}}};
                ushort var11 = 61344;
                s_rt.Checksum("c_521", var11);
            }
        }

        if (M80(s_40[0][0, 0]--))
        {
            s_9 = M68((sbyte)M81());
            ulong var12 = s_57;
            s_rt.Checksum("c_522", var12);
        }
        else
        {
            return s_42;
        }

        byte var13 = (byte)M72();
        s_92 = (ushort)((int)(1UL / (uint)(s_22[0][0] | 1)) - s_68--);
        s_rt.Checksum("c_523", var13);
        return s_54[0];
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
        {
            M67(new ushort[,, ]{{{12336, 52858, 0, 2562, 65535, 2, 7336, 6210}, {13345, 1, 0, 30690, 1, 10, 1, 14545}}, {{51287, 24005, 57663, 8195, 2, 30094, 35916, 1}, {22844, 62294, 0, 0, 59306, 1, 61721, 1}}, {{1, 0, 19284, 0, 0, 49486, 33543, 1}, {0, 1, 40692, 1, 1758, 0, 0, 65534}}, {{0, 0, 1, 10975, 28236, 51936, 0, 46311}, {47862, 47938, 0, 10, 65535, 22547, 43200, 10}}, {{1, 0, 1, 44477, 54671, 65534, 1, 65535}, {1, 0, 0, 65535, 65534, 6363, 34858, 1}}, {{65534, 10, 0, 65534, 48074, 29251, 26149, 1913}, {53056, 1, 1, 0, 2, 48755, 27226, 1}}, {{0, 22734, 27788, 64566, 0, 0, 1, 65534}, {32204, 60789, 2, 0, 0, 40961, 6630, 0}}});
            sbyte[] var0 = M68(s_56);
            s_rt.Checksum("c_502", var0[0]);
        }

        M76(ref s_50[0][0], M68(11), false);
        {
            {
                M75(ref s_55, true || (M80(s_6++) == false), M69(-2, s_37--, 0UL, ref s_2[0], 10, false, s_14[0]), ref s_38[0][0], new bool[]{true, false, true, false});
            }

            {
                s_46 = M69(-57, -2147483647, s_57 % (uint)(s_67 | 1), ref s_56, 254, M69(1, s_31, (ulong)(s_34 + (long)(11310972994113654911UL % (byte)(s_65 | 1))), ref s_72[0], (byte)(s_47 % (byte)((byte)M72() | 1)), M85(ref s_51), 9223372036854775806L / (sbyte)((sbyte)M76(ref s_66, M68((sbyte)(s_29 & (short)M84())), M69((sbyte)(s_80 * 15253), s_32[0, 0, 0], (ulong)(M82() / (byte)(s_60-- | 1)), ref s_69, (byte)M67(new ushort[,, ]{{{29951, 65535, 1}, {26231, 10, 1}, {59902, 53804, 0}, {19081, 63771, 65534}, {57027, 16811, 2865}}, {{33028, 0, 42349}, {49343, 0, 51707}, {7741, 9623, 4914}, {10, 45173, 1}, {26315, 29019, 0}}, {{43474, 2, 18169}, {1, 65535, 0}, {36716, 1, 0}, {20900, 56828, 1258}, {52681, 0, 26851}}, {{0, 0, 19148}, {14485, 15664, 24453}, {26993, 6797, 0}, {0, 42845, 65535}, {29681, 1, 4648}}, {{59492, 1, 65534}, {0, 41599, 0}, {0, 0, 63858}, {51118, 1, 40680}, {36647, 7541, 1}}, {{1, 1, 65534}, {39949, 38983, 28181}, {34014, 52365, 14160}, {7739, 65534, 58216}, {47738, 35000, 1}}}), M80(1L), (long)M76(ref s_82, s_78, s_87))) | 1)), (long)M72());
            }
        }

        return M69((sbyte)M72(), s_12 | s_28++, (ulong)(0 + s_33++), ref s_2[0], 216, true, -684893295095327569L);
    }

    static ulong M67(ushort[,, ] arg0)
    {
        arg0 = new ushort[,, ]{{{42743, 18929}, {0, 10}, {1, 60613}, {1419, 4356}, {2, 0}, {15803, 2}, {1, 0}, {0, 1}, {46098, 50198}}, {{1, 0}, {1, 50694}, {1, 47610}, {12698, 5471}, {10, 23078}, {10776, 17859}, {55685, 1}, {4206, 1}, {47863, 47404}}, {{65534, 65534}, {1, 31924}, {40004, 13448}, {35189, 0}, {10869, 0}, {0, 0}, {1, 29453}, {46434, 33198}, {0, 61070}}, {{30395, 62090}, {0, 53306}, {0, 1}, {0, 26434}, {0, 51819}, {13616, 0}, {65534, 1}, {29404, 12860}, {0, 1}}, {{65035, 65495}, {4644, 0}, {11957, 1}, {0, 1}, {47032, 0}, {0, 10}, {20172, 10}, {0, 0}, {55269, 65534}}, {{1, 55913}, {31377, 2}, {10321, 53527}, {1, 43318}, {65535, 52708}, {35209, 0}, {13870, 47552}, {1, 28512}, {11607, 0}}};
        s_rt.Checksum("c_340", arg0[0, 0, 0]);
        return 3224594519270290593UL;
    }

    static sbyte[] M68(sbyte arg0)
    {
        {
            short var0 = 18455;
            s_rt.Checksum("c_341", var0);
        }

        arg0 += (sbyte)(arg0 % (ushort)(34903 | 1));
        if (s_48)
        {
            {
                if (s_48)
                {
                    arg0 = arg0;
                }
                else
                {
                    arg0 = 1;
                }

                {
                    M69(-14, s_38[0][0], s_5, ref arg0, (byte)((byte)((short)(arg0 / (long)(M81() | 1)) | s_24) + s_30[0]), false, s_14[0]++);
                }

                if (s_36[0][0])
                {
                    ref byte var1 = ref s_34;
                    s_rt.Checksum("c_497", var1);
                }

                arg0 = 35;
                arg0++;
                s_85 = new ushort[]{1, 65535, 49740};
                arg0 = arg0;
                s_86 = s_86;
            }
        }
        else
        {
            {
                arg0 = s_2[0];
                if (M80(s_29))
                {
                    M80(s_44);
                    M74(ref arg0);
                    arg0 /= (sbyte)(s_2[0]++ | 1);
                    M80(-2004781800317358114L);
                    ulong var2 = 2UL;
                    s_rt.Checksum("c_498", var2);
                }

                --arg0;
                arg0 = (sbyte)M87();
                bool var3 = true;
                M71();
                s_rt.Checksum("c_499", var3);
            }

            arg0 = -113;
            s_87 = M69(arg0, 2147483647, s_5, ref arg0, (byte)(9223372036854775806L - (sbyte)(s_52[0] ^ (-562809373 % (uint)(s_81 | 1)))), true, 9223372036854775806L);
            byte var4 = s_25;
            {
                if ((int)M72() > s_21)
                {
                    s_63 = 23936;
                    arg0 = 127;
                    arg0 = s_26;
                }
            }

            s_rt.Checksum("c_500", var4);
        }

        s_rt.Checksum("c_501", arg0);
        return s_78;
    }

    static ref bool M69(sbyte arg0, int arg1, ulong arg2, ref sbyte arg3, byte arg4, bool arg5, long arg6)
    {
        arg0 = arg0;
        if (arg5)
        {
            int var0 = 2147483646;
            {
                uint var1 = 0U;
                if (arg5)
                {
                    arg3 = -53;
                    uint var2 = var1;
                    arg4 = arg4;
                    var0 = var0;
                    var0 = 415115690;
                    s_rt.Checksum("c_342", var2);
                }

                s_rt.Checksum("c_343", var1);
            }

            M70();
            s_rt.Checksum("c_488", var0);
        }

        M73(arg1, -127, ref arg3, arg5, -2029156729063870913L, M82()--) = M73(arg1, arg3, ref arg3, arg5, arg6, 4279521233U);
        long var3 = arg6;
        M73(arg4 % (ushort)((ushort)M82() | 1), arg3, ref s_26, M80(s_35), (long)M76(ref s_17, new sbyte[]{-120}, M75(ref s_55, arg5, s_46, ref arg1, new bool[]{false, false, false, false, true, false, true})), M87()) = new ulong[]{1UL, 6094349442551970475UL, 12014132020281218584UL, 8298889677554800209UL};
        s_rt.Checksum("c_489", arg0);
        s_rt.Checksum("c_490", arg1);
        s_rt.Checksum("c_491", arg2);
        s_rt.Checksum("c_492", arg3);
        s_rt.Checksum("c_493", arg4);
        s_rt.Checksum("c_494", arg5);
        s_rt.Checksum("c_495", arg6);
        s_rt.Checksum("c_496", var3);
        return ref s_46;
    }

    static long M70()
    {
        {
            ushort var0 = 10;
            var0 /= (ushort)(s_50[0][0] | 1);
            if (s_36[0][0])
            {
                M71();
                var0 = var0;
                {
                    var0 = 0;
                    var0 = s_17;
                    s_36[0][0] = s_48 || s_39;
                    var0 = s_50[0][0];
                }

                M71();
            }
            else
            {
                var0 = var0;
            }

            if (s_53[0, 0, 0])
            {
                ushort var1 = s_50[0][0];
                {
                    int var2 = -97577244;
                    bool var3 = true;
                    s_rt.Checksum("c_344", var2);
                    s_rt.Checksum("c_345", var3);
                }

                var1 = 0;
                {
                    s_61 = false;
                    var1 = M72();
                    var1 = (ushort)(s_15++ - var0);
                    M77(new ulong[][]{new ulong[]{1UL, 9241298317481383361UL, 11534605575536574823UL, 9712806879556774885UL, 1UL}, new ulong[]{14364263989414448360UL, 0UL, 10282521801551754565UL, 0UL, 18446744073709551614UL}, new ulong[]{14400723069020463870UL}}, s_56++, new ushort[]{65534, 26253, 14492, 22508, 65535, 40041, 65534, 0, 0, 1});
                }

                M79(s_16[0, 0], new sbyte[]{-44, 99, 24, 0}, (uint)(s_16[0, 0]++ * 1));
                var1++;
                {
                    {
                        M81();
                        long var4 = (long)M84();
                        if ((short)M84() < s_80--)
                        {
                            var1 /= (ushort)(var0 | 1);
                        }
                        else
                        {
                            M81();
                            M74(ref s_78[0]);
                            s_16[0, 0] = s_78[0]--;
                            M77(new ulong[][]{new ulong[]{12706156443907576347UL, 7542155426624716021UL, 0UL, 14201900461076611116UL}, new ulong[]{2289159056391456825UL, 9275773968207846980UL}, new ulong[]{18446744073709551615UL, 12347523782964265674UL, 16699916090565251790UL}, new ulong[]{1UL, 0UL, 0UL}}, 10, new ushort[]{1});
                            s_50[0][0] &= var0;
                            var1 = var0;
                        }

                        var1 = (ushort)(var0 / (ulong)(s_5 | 1));
                        s_50[0][0] = (ushort)M77(new ulong[][]{new ulong[]{18446744073709551614UL}, new ulong[]{12717952360457800135UL, 18446744073709551614UL}, new ulong[]{1UL, 1323724307272949828UL, 0UL}, new ulong[]{1UL, 0UL}, new ulong[]{1UL, 0UL}, new ulong[]{1UL}, new ulong[]{18446744073709551615UL, 5678418257626285088UL, 18446744073709551615UL}, new ulong[]{0UL}, new ulong[]{1UL}, new ulong[]{1UL}}, (sbyte)M84(), new ushort[]{55308, 1, 31950, 40512, 31111, 1394});
                        s_rt.Checksum("c_463", var4);
                    }
                }

                M76(ref var1, s_78, s_72[0] <= (ushort)M88()) |= s_37--;
                s_rt.Checksum("c_464", var1);
            }
            else
            {
                {
                    s_32 = s_32;
                    var0 = s_50[0][0];
                    var0 = var0;
                    uint[][] var5 = new uint[][]{new uint[]{107984523U, 4294967294U, 1U, 3799507504U, 4294967295U, 0U, 2130398231U, 2823990187U, 0U}};
                    ushort var6 = var0;
                    int var7 = (int)(var5[0][0] ^ var6--);
                    s_rt.Checksum("c_465", var5[0][0]);
                    s_rt.Checksum("c_466", var6);
                    s_rt.Checksum("c_467", var7);
                }

                if (M75(ref s_55, (short)M87() > s_65, false, ref s_12, new bool[]{false, true, true, false, true, false, true, false, true, true}))
                {
                    int var8 = s_32[0, 0, 0];
                    ref sbyte var9 = ref s_56;
                    M83();
                    var9 = var9;
                    M73(var8, var9++, ref var9, true, (long)M83(), M88()) = s_10;
                    ulong[] var10 = new ulong[]{15778092188839117741UL, 10331334473614392193UL, 18446744073709551614UL, 14973649830102340673UL, 1UL, 18446744073709551614UL, 818578968558133657UL, 0UL, 1UL, 15306980762617023873UL};
                    var0 = var0;
                    if (s_61)
                    {
                        var8 = s_37;
                    }
                    else
                    {
                        bool var11 = false;
                        int var12 = var8;
                        s_rt.Checksum("c_468", var11);
                        s_rt.Checksum("c_469", var12);
                    }

                    long[] var13 = new long[]{9223372036854775807L, -2L, 1L, 1L, 0L};
                    uint[] var14 = new uint[]{10U, 3790920293U, 1458709766U, 0U, 0U, 1984161592U, 1671185116U, 1U};
                    try
                    {
                        M78();
                    }
                    finally
                    {
                        s_82 = 4820;
                        {
                            s_59[0] = 9223372036854775807L;
                            var10 = var10;
                            M73(0, -1, ref s_16[0, 0], true, var13[0], var14[0]);
                            ref int var15 = ref var8;
                            s_rt.Checksum("c_470", var15);
                        }
                    }

                    s_rt.Checksum("c_471", var8);
                    s_rt.Checksum("c_472", var9);
                    s_rt.Checksum("c_473", var10[0]);
                    s_rt.Checksum("c_474", var13[0]);
                    s_rt.Checksum("c_475", var14[0]);
                }
                else
                {
                    ulong var16 = (ulong)(s_30[0]-- ^ (sbyte)M72());
                    int var17 = s_21--;
                    s_rt.Checksum("c_476", var16);
                    s_rt.Checksum("c_477", var17);
                }
            }

            try
            {
                M76(ref s_82, s_2, true);
                s_23 = new bool[]{false, false, true, true, true, false, false, true};
                M82();
            }
            finally
            {
                {
                    if (s_61)
                    {
                        long var18 = s_29;
                        s_rt.Checksum("c_478", var18);
                    }

                    ulong var19 = 16819530361715984951UL;
                    ref int[, ] var20 = ref s_83;
                    ulong var21 = var19;
                    M85(ref s_51);
                    s_37 = (int)M72();
                    uint var22 = 1U;
                    long[][] var23 = new long[][]{new long[]{-2178892822121763534L, -6262903406858793872L, 0L, -9223372036854775807L}, new long[]{4791957849126527895L, 4398656973800038686L, 9223372036854775806L, -9223372036854775808L}, new long[]{0L, 1L, 1L, -10L}, new long[]{-9223372036854775808L, 1L}, new long[]{9223372036854775807L, 0L, 9223372036854775807L}};
                    s_78 = new sbyte[]{126, 1, -10};
                    s_84 = new long[]{-1L, -1129832372959387863L, 9223372036854775807L, -9223372036854775808L, 0L, -9223372036854775807L, 9223372036854775807L};
                    ulong var24 = var21;
                    s_rt.Checksum("c_479", var19);
                    s_rt.Checksum("c_480", var20[0, 0]);
                    s_rt.Checksum("c_481", var21);
                    s_rt.Checksum("c_482", var22);
                    s_rt.Checksum("c_483", var23[0][0]);
                    s_rt.Checksum("c_484", var24);
                }

                ulong[, ] var25 = new ulong[, ]{{1UL, 13019011886429721869UL, 7869956977206718133UL, 0UL, 18446744073709551615UL}, {0UL, 13406935657102287499UL, 18446744073709551615UL, 1124215741290740454UL, 4011577670390259948UL}, {17439553961487815336UL, 1UL, 4505865698866580321UL, 18446744073709551615UL, 16748919887243609476UL}, {0UL, 2UL, 12216558064142025236UL, 1UL, 421361296817289393UL}, {5689348271114896545UL, 12950402514180946680UL, 16367028517161626054UL, 5777093652747729344UL, 18446744073709551615UL}, {0UL, 18446744073709551615UL, 18446744073709551614UL, 8137911578315431162UL, 5311820081023965949UL}, {165754765391069859UL, 18446744073709551614UL, 10UL, 13244648845152057030UL, 18446744073709551615UL}};
                long var26 = s_59[0]++;
                s_rt.Checksum("c_485", var25[0, 0]);
                s_rt.Checksum("c_486", var26);
            }

            s_4[0][0] = s_12-- - s_8;
            s_58 |= s_25--;
            s_rt.Checksum("c_487", var0);
        }

        return s_44--;
    }

    static ushort[][] M71()
    {
        s_50[0][0] = 0;
        return s_50;
    }

    static ushort M72()
    {
        short[][][] var0 = new short[][][]{new short[][]{new short[]{-22525, -1, -4500}, new short[]{-8875, 3076, -15672}, new short[]{0, 32766, -8804, -8767, 13444, -15540}}, new short[][]{new short[]{18963}, new short[]{3723, 2, 12941, 19788, 0, 11055}}};
        if (s_48)
        {
            int var1 = s_21++;
            var1 = var1;
            var1 = -10;
            if (s_7)
            {
                M73(0, s_2[0]--, ref s_78[0], s_55[0], s_6, M88());
                var0[0] = new short[][]{new short[]{-32768, 32767, -17242, 1}, new short[]{8568, 32766, 10574, 0, 0, 0, 20553}};
                s_38[0] = s_4[0];
                s_22[0][0] = s_43;
                var1 = var1;
            }
            else
            {
                var0[0][0] = var0[0][0];
                var0[0] = var0[0];
                var0[0][0] = var0[0][0];
            }

            s_25 = 143;
            var0[0] = M86(-1440769716, 65534, 18446744073709551614UL, 32767 | (uint)M79(s_26, new sbyte[]{0}, 1U), ref s_35, new sbyte[]{-108, 100, -100}, 4294967294U);
            var0[0][0][0] = var0[0][0][0];
            var0 = new short[][][]{new short[][]{new short[]{27267, -26818, 20725}, new short[]{8174, -32767, -24932}, new short[]{-20926, 32766}, new short[]{32545, -20666}, new short[]{702, -1092, 9175, 1}, new short[]{-25275}}, new short[][]{new short[]{11115, 22554, -11486}}, new short[][]{new short[]{-12400}}, new short[][]{new short[]{0, -17604, 30161, 0}, new short[]{32767, -32767, 31353, 32767}, new short[]{9288, 2}, new short[]{32766, 1454}}, new short[][]{new short[]{1, -23342, -32768, 1}, new short[]{-17550, 19366, -32768}, new short[]{2, 32767, 0, 32767}, new short[]{32767}, new short[]{0, 4428}, new short[]{30964}}, new short[][]{new short[]{31273, -1}, new short[]{32767, -2, -11992, -22188}, new short[]{10}, new short[]{10547, 20070, -32768, -9928}, new short[]{-27098, -6142, 26983}}, new short[][]{new short[]{25518, 1, 8931, 32767}, new short[]{-32768}, new short[]{-16408, 1, 32766}}, new short[][]{new short[]{1, 459}, new short[]{-6959, 1, 28360, -13657}, new short[]{-32768, 32767}, new short[]{-32768}, new short[]{15546}}, new short[][]{new short[]{14907, 2, -2}, new short[]{29567, 0}, new short[]{-24458, -29885, -32767}, new short[]{31001}, new short[]{0, -7281, 6820}}, new short[][]{new short[]{19773}, new short[]{0, 22083, 1}, new short[]{-20653, 1}, new short[]{22167, -6303}, new short[]{-32767, -32767, 0}, new short[]{32766, 2499}, new short[]{0, 0}}};
            var0 = var0;
            M76(ref s_50[0][0], new sbyte[]{2, 0, -127, -128, 1, -35, 127, 99, -113}, s_55[0]);
            try
            {
                var0 = var0;
            }
            finally
            {
                var0 = new short[][][]{new short[][]{new short[]{1}}, new short[][]{new short[]{-2}, new short[]{-1807}}, new short[][]{new short[]{-1702}, new short[]{1}}, new short[][]{new short[]{30158}, new short[]{-32443}, new short[]{-10}}, new short[][]{new short[]{-1}}, new short[][]{new short[]{32766}, new short[]{0}, new short[]{0}}, new short[][]{new short[]{-13487}}, new short[][]{new short[]{1}, new short[]{-1}, new short[]{0}}, new short[][]{new short[]{26191}, new short[]{-22364}}};
            }

            s_rt.Checksum("c_461", var1);
            return s_50[0][0]--;
        }

        var0 = var0;
        s_72[0] = s_16[0, 0];
        s_rt.Checksum("c_462", var0[0][0][0]);
        return (ushort)((ulong)(65535 / (byte)((byte)(var0[0][0][0] - s_68) | 1)) | s_51);
    }

    static ref ulong[] M73(int arg0, sbyte arg1, ref sbyte arg2, bool arg3, long arg4, uint arg5)
    {
        {
            if (arg3)
            {
                if (arg3)
                {
                    short var0 = s_54[0]--;
                    s_rt.Checksum("c_346", var0);
                }

                bool var1 = arg3;
                ulong var2 = 7731816568299975045UL;
                M74(ref s_9[0]);
                if (arg3)
                {
                    arg3 = arg3;
                    s_74 = s_54[0];
                }
                else
                {
                    M80(arg4++);
                    var2 = (ulong)((long)(9674 % (ushort)((ushort)(s_57 - (byte)M81()) | 1)) ^ s_50[0][0]);
                    int var3 = arg0;
                    var1 = false;
                    M78();
                    ushort var4 = 10677;
                    M76(ref var4, new sbyte[]{1, 37, 1, -128, 1}, M85(ref s_57)) = -1958897922;
                    ulong var5 = s_41;
                    s_rt.Checksum("c_418", var3);
                    s_rt.Checksum("c_419", var4);
                    s_rt.Checksum("c_420", var5);
                }

                s_rt.Checksum("c_421", var1);
                s_rt.Checksum("c_422", var2);
            }
            else
            {
                arg1 = (sbyte)M76(ref s_66, s_72, M76(ref s_71, s_2, s_36[0][0]) < s_19--);
                arg5 = 3762173660U;
                s_75 = s_30;
            }
        }

        sbyte[] var6 = new sbyte[]{83, 2, 1, 0, -127, 0, 118, -53, -40, 127};
        ref short var7 = ref s_54[0];
        var6 = new sbyte[]{68, -62, 47, 112};
        M86(arg0, 0, s_10[0], arg4, ref arg4, new sbyte[]{0, 1, -82}, arg5);
        M82() = (uint)M77(new ulong[][]{new ulong[]{4243510672054320913UL, 1UL, 0UL, 7470172993746202514UL, 8034701939916573486UL}, new ulong[]{15922323541900284477UL, 13579709618074211288UL, 412024011019601249UL, 1UL, 1UL}, new ulong[]{11225623764325307720UL, 0UL}, new ulong[]{0UL, 4251507856021102779UL, 0UL, 1UL, 18446744073709551614UL}, new ulong[]{18446744073709551614UL, 13295524757949047973UL, 1UL}, new ulong[]{0UL, 1UL, 17870506026229376508UL}, new ulong[]{9547919680104487218UL, 0UL, 2751600608620645290UL, 826425428225408521UL, 11987676524554470284UL}, new ulong[]{1UL, 18446744073709551615UL}, new ulong[]{112695361780868991UL}, new ulong[]{1UL, 1UL}}, arg1--, new ushort[]{49042, 65534, 65534, 1, 11624, 42180});
        M82() = 1U;
        if (arg3)
        {
            M83();
            if (M80(7573968938990664773L))
            {
                arg0 = arg0;
                uint[] var8 = s_1;
                s_rt.Checksum("c_450", var8[0]);
            }
        }

        bool var9 = M80(-9147339872171356967L);
        s_81 -= (uint)M74(ref arg1);
        M79((sbyte)(arg5 & arg0), new sbyte[]{52, 127, 84, -33, 47}, 4294967295U);
        s_rt.Checksum("c_451", arg0);
        s_rt.Checksum("c_452", arg1);
        s_rt.Checksum("c_453", arg2);
        s_rt.Checksum("c_454", arg3);
        s_rt.Checksum("c_455", arg4);
        s_rt.Checksum("c_456", arg5);
        s_rt.Checksum("c_457", var6[0]);
        s_rt.Checksum("c_458", var7);
        s_rt.Checksum("c_459", var9);
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
        if (arg0[0])
        {
            arg0 = arg0;
            arg0 = arg0;
            s_2[0] = (sbyte)(-10 - s_8++);
            if (arg1)
            {
                if (s_5 >= s_52[0]++)
                {
                    arg1 = arg2;
                    byte var0 = 1;
                    arg4[0] = true;
                    {
                        {
                            M76(ref s_50[0][0], new sbyte[]{0, 1, 1, -33, 127, -27, 0, -128}, false);
                            M78();
                            s_64 = new byte[]{10};
                        }

                        arg0[0] = false;
                        M77(new ulong[][]{new ulong[]{5621085677742063254UL}, new ulong[]{0UL, 1171278029292956117UL, 15207803337064566735UL}, new ulong[]{1753262037771134695UL, 1UL, 1UL}, new ulong[]{1UL}, new ulong[]{10992174190707407308UL, 0UL}}, 126, new ushort[]{1090, 14663, 51604, 64691, 0});
                        ushort var1 = 34230;
                        M78();
                        M78();
                        s_rt.Checksum("c_358", var1);
                    }

                    try
                    {
                        byte var2 = var0;
                        s_rt.Checksum("c_359", var2);
                    }
                    finally
                    {
                        ulong var3 = (ulong)M77(M78(), s_9[0], new ushort[]{1, 3062, 0, 35788, 23509, 48126, 44004, 8814, 1});
                        M77(new ulong[][]{new ulong[]{5879737676674622625UL, 1UL, 495273149181401503UL, 1UL, 18446744073709551614UL}, new ulong[]{0UL, 2UL}, new ulong[]{13793582676239250899UL, 18446744073709551614UL, 0UL, 0UL}, new ulong[]{5056721534509808488UL, 18446744073709551614UL, 1UL, 93041159801607704UL}, new ulong[]{1UL, 0UL, 0UL}, new ulong[]{0UL, 2700663513666935798UL, 15746709632658835052UL, 1UL}, new ulong[]{2795064980979881614UL, 1UL, 14723055999302161303UL, 1UL}, new ulong[]{0UL, 0UL}}, 0, s_3);
                        s_65 = 0;
                        ushort var4 = s_63;
                        {
                            s_66 = s_50[0][0];
                            var3 = s_51;
                            short var5 = s_28;
                            s_rt.Checksum("c_360", var5);
                        }

                        M76(ref var4, new sbyte[]{126, 1, -73, -123, 0}, s_55[0]) = 0;
                        if (arg0[0])
                        {
                            M76(ref var4, new sbyte[]{71, 62, 75, 1, -120, 0, -2, -51}, false) = M79((sbyte)(s_30[0]-- | arg3), new sbyte[]{0, 1, 35, 1, -124, 2, -53, -127, -1}, s_33++);
                            long var6 = (long)(s_50[0][0]++ % (ulong)(var3 | 1));
                            s_rt.Checksum("c_402", var6);
                        }
                        else
                        {
                            s_22[0][0] = 1U;
                        }

                        arg0[0] = arg2;
                        arg0 = new bool[]{true, true, true, false, true, false, true, true, true, true};
                        s_rt.Checksum("c_403", var3);
                        s_rt.Checksum("c_404", var4);
                    }

                    s_rt.Checksum("c_405", var0);
                }

                ref int var7 = ref s_21;
                s_rt.Checksum("c_406", var7);
            }
            else
            {
                arg0[0] = arg2;
                arg0[0] = arg4[0];
                M84();
                M80(s_27--);
                s_71 = (ushort)M83();
                arg0[0] = M80((long)M76(ref s_50[0][0], s_9, M85(ref s_5)));
                arg4 = arg0;
                return arg2;
            }

            if (s_7)
            {
                ushort var8 = (ushort)M76(ref s_66, new sbyte[]{1, -2, 125, 127, 50, 127}, arg2);
                arg2 = arg4[0];
                if (arg2)
                {
                    {
                        byte var9 = 1;
                        s_rt.Checksum("c_408", var9);
                    }
                }
                else
                {
                    M76(ref var8, new sbyte[]{1, -105, 1, 3, 0, 80, 120, 1, 60}, arg0[0]) = -1252395279;
                    s_69 = 42;
                    s_55[0] = arg4[0];
                }

                int var10 = arg3++;
                s_rt.Checksum("c_409", var8);
                s_rt.Checksum("c_410", var10);
            }

            arg4 = arg0;
        }
        else
        {
            M84();
            arg0 = new bool[]{true, true, false, false, false};
            s_72 = new sbyte[]{-16};
        }

        M84();
        M79(-124, new sbyte[]{82, 0, -128, -127, -37, 0, -92, 91, -103}, 104411687U);
        M80((long)M83());
        s_73 = s_59[0];
        arg1 = arg2;
        uint[] var11 = s_1;
        arg3 = (int)((byte)M82() & (uint)(6866055105043680031L * s_44--));
        s_rt.Checksum("c_411", arg0[0]);
        s_rt.Checksum("c_412", arg1);
        s_rt.Checksum("c_413", arg2);
        s_rt.Checksum("c_414", arg3);
        s_rt.Checksum("c_415", arg4[0]);
        s_rt.Checksum("c_416", var11[0]);
        return arg2;
    }

    static ref int M76(ref ushort arg0, sbyte[] arg1, bool arg2)
    {
        M77(new ulong[][]{new ulong[]{18446744073709551614UL, 1UL}, new ulong[]{15690541342769143181UL, 17398716044925779772UL, 18446744073709551614UL}, new ulong[]{1UL, 1UL}, new ulong[]{12234101859255155577UL, 7146523035169648723UL}}, -127, new ushort[]{32558, 1, 50193, 1, 2, 49502, 3585, 1, 0, 45353});
        if (arg2)
        {
            {
                M77(M78(), s_26, new ushort[]{53413, 41112, 0, 0, 1, 65534, 29114, 0});
                uint var0 = (uint)((sbyte)M77(new ulong[][]{new ulong[]{1UL, 1UL, 10693355332976915320UL, 17412061499159957493UL, 0UL, 11348041743089135588UL, 18446744073709551614UL}, new ulong[]{13387153710988798828UL, 1UL, 3538027604935874308UL, 1UL}}, (sbyte)((ulong)(24 * (uint)M77(new ulong[][]{new ulong[]{4782453672055088607UL}, new ulong[]{8278111705394285194UL}}, arg1[0], new ushort[]{34915, 60307})) & 0UL), new ushort[]{0, 1, 1, 1, 1, 13670, 35393, 14785}) ^ (long)(s_5 * s_20[0]));
                short var1 = s_30[0];
                s_rt.Checksum("c_352", var0);
                s_rt.Checksum("c_353", var1);
            }
        }

        s_62 = 176;
        {
            s_54 = new short[]{17171, 13604};
            s_1 = new uint[]{3334331488U, 1426777118U, 3844801307U, 4294967294U, 1U};
            {
                arg1[0] = (sbyte)(-1 * (short)M77(new ulong[][]{new ulong[]{7115460865201064081UL}, new ulong[]{7949022291154700520UL, 17932050784366288617UL, 1UL, 4871809452878930427UL}, new ulong[]{1451268352715309490UL, 14326210775805572974UL}, new ulong[]{1080372670444987834UL, 16328322298710680654UL, 1UL, 8433657926570905288UL}, new ulong[]{1UL, 0UL}, new ulong[]{0UL}, new ulong[]{11636931010829965068UL, 18446744073709551615UL, 0UL, 14416040590337491369UL}, new ulong[]{13639506028580773539UL, 1175522868954493985UL, 2UL, 0UL, 0UL, 1UL, 10126600105743262869UL}, new ulong[]{12855657691930593886UL, 18446744073709551615UL}}, arg1[0], s_50[0]));
                arg1 = arg1;
                s_63 = arg0--;
            }

            arg1[0] = -45;
            arg1[0] = arg1[0];
            try
            {
                if (s_48)
                {
                    int var2 = s_37--;
                    s_rt.Checksum("c_354", var2);
                }
                else
                {
                    arg1 = arg1;
                    M78();
                }
            }
            finally
            {
                arg1[0] = s_56;
            }
        }

        s_rt.Checksum("c_355", arg0);
        s_rt.Checksum("c_356", arg1[0]);
        s_rt.Checksum("c_357", arg2);
        return ref s_4[0][0];
    }

    static long M77(ulong[][] arg0, sbyte arg1, ushort[] arg2)
    {
        if ((sbyte)(s_19-- % (long)(s_59[0]-- | 1)) == arg2[0])
        {
            arg0[0] = new ulong[]{1UL, 429455367443649211UL, 18446744073709551614UL, 2UL};
        }
        else
        {
            {
                try
                {
                    bool var0 = false;
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
        uint var0 = 0U;
        s_rt.Checksum("c_351", var0);
        return new ulong[][]{new ulong[]{14512988967422436812UL, 11722771867974734503UL, 18446744073709551615UL, 12976961772911120469UL}, new ulong[]{1UL, 2503323721261690782UL, 1UL, 13142816938197938376UL, 18446744073709551614UL, 18446744073709551615UL}, new ulong[]{9389184152153229465UL, 1UL, 18446744073709551614UL}, new ulong[]{16760042309932406064UL, 0UL, 1UL}, new ulong[]{1UL, 1UL, 0UL, 1342838692465107UL, 1928393630231800427UL}, new ulong[]{11529851535409344551UL, 17612538283661160513UL, 0UL, 18446744073709551615UL, 1UL, 18446744073709551614UL, 3089073821720654974UL}, new ulong[]{1UL, 18446744073709551615UL, 16380860044031446442UL, 0UL, 116036276351870827UL, 11627908559790285408UL, 1UL}};
    }

    static int M79(sbyte arg0, sbyte[] arg1, uint arg2)
    {
        if (true & ((byte)(s_3[0]++ | arg2) != 65534))
        {
            arg2 = arg2++;
        }

        bool var0 = M80(-9223372036854775807L);
        arg1 = arg1;
        if (s_55[0])
        {
            long[] var1 = new long[]{1L, 748189618062369328L, 1L, -9223372036854775808L};
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
        uint var0 = 4294967295U;
        ulong[][][] var1 = new ulong[][][]{new ulong[][]{new ulong[]{10635741721305595627UL, 1UL, 1UL, 7723546937833852134UL}}, new ulong[][]{new ulong[]{0UL, 18446744073709551615UL, 2097255010348731977UL, 17196064712237990722UL, 18446744073709551614UL, 5543780225214284799UL, 8015725465774018329UL}, new ulong[]{4250408319873309239UL, 18446744073709551615UL, 14682203718542178504UL, 18446744073709551614UL}}, new ulong[][]{new ulong[]{2592471316962209237UL, 1UL, 18446744073709551615UL}}, new ulong[][]{new ulong[]{0UL, 489285985875127993UL, 4163653733987278078UL, 11835252408794549963UL, 17945111968195278341UL, 16270670976471125534UL, 0UL, 0UL}}};
        var1[0] = var1[0];
        if (s_61)
        {
            ref long var2 = ref arg0;
            ulong var3 = var1[0][0][0];
            long var4 = var0 + var2;
            ulong[] var5 = var1[0][0];
            byte var6 = 237;
            sbyte var7 = s_9[0];
            s_rt.Checksum("c_361", var2);
            s_rt.Checksum("c_362", var3);
            s_rt.Checksum("c_363", var4);
            s_rt.Checksum("c_364", var5[0]);
            s_rt.Checksum("c_365", var6);
            s_rt.Checksum("c_366", var7);
        }
        else
        {
            M81();
            var1[0][0][0] = var1[0][0][0];
            M83();
            {
                var1[0] = var1[0];
                bool var8 = false;
                ulong var9 = 1UL;
                if (var8)
                {
                    ulong var10 = (ulong)((int)(-556769901 ^ s_6) % (long)(arg0 | 1));
                    s_rt.Checksum("c_390", var10);
                }

                s_rt.Checksum("c_391", var8);
                s_rt.Checksum("c_392", var9);
            }

            if (s_23[0])
            {
                long var11 = arg0;
                if (s_36[0][0])
                {
                    arg0 = arg0;
                }
                else
                {
                    {
                        M81();
                    }
                }

                var1 = new ulong[][][]{new ulong[][]{new ulong[]{8563979961761737911UL, 4742886755911542877UL}, new ulong[]{6632010320610704910UL, 1UL, 17377571756170330518UL, 5987007972576325201UL}, new ulong[]{13109414946575889500UL}, new ulong[]{11937346524127793344UL, 15969885708093443819UL, 3567092502992667172UL, 6604339633471045730UL}, new ulong[]{18446744073709551614UL}, new ulong[]{3878148229634765536UL}, new ulong[]{9757084458031670985UL, 12654741971531004013UL, 4156413271783773855UL, 2UL, 655222304387521048UL, 17943933171355419149UL}, new ulong[]{10UL, 0UL, 1UL}}};
                s_rt.Checksum("c_393", var11);
            }
            else
            {
                var1[0][0][0] = 395465175537695761UL;
                M83();
                arg0 = -9223372036854775807L;
                M82();
                var1[0][0][0] = s_5;
            }

            var0 = s_68;
            M82();
            var1[0][0][0] = var1[0][0][0];
            return true;
        }

        s_rt.Checksum("c_394", arg0);
        s_rt.Checksum("c_395", var0);
        s_rt.Checksum("c_396", var1[0][0][0]);
        return true;
    }

    static long M81()
    {
        try
        {
            s_8 += (short)((short)(s_62 * (short)((ushort)(0 - s_14[0]--) * s_50[0][0]--)) & 1);
        }
        finally
        {
            {
                s_30 = s_30;
                ulong var0 = (ulong)(-73 / (sbyte)(s_16[0, 0]++ | 1));
                M82();
                s_rt.Checksum("c_369", var0);
            }
        }

        long var1 = 3088148697491962091L;
        if (s_46)
        {
            if (s_7)
            {
                ref bool var2 = ref s_48;
                var1 = var1;
                s_50[0][0] = 0;
                var1 = (long)M84();
                M83();
                s_rt.Checksum("c_370", var2);
            }
            else
            {
                M84();
                if (s_55[0])
                {
                    M83();
                }
                else
                {
                    short var3 = s_24;
                    var1 = (long)(s_33 + s_43);
                    int var4 = s_37;
                    var3 = var3;
                    s_3[0] = 0;
                    long var5 = (long)M84();
                    s_43 = M82();
                    s_rt.Checksum("c_371", var3);
                    s_rt.Checksum("c_372", var4);
                    s_rt.Checksum("c_373", var5);
                }

                ref long var6 = ref var1;
                s_rt.Checksum("c_374", var6);
            }

            M82() = 1U;
            s_68 = 4294967294U;
            s_24 = (short)M84();
            try
            {
                M82();
            }
            finally
            {
                var1 &= 9223372036854775807L;
                if (s_15++ == 2539948294U)
                {
                    if (s_36[0][0])
                    {
                        uint[, ] var7 = new uint[, ]{{1U, 4256839128U, 3476032174U}};
                        bool var8 = s_7;
                        s_rt.Checksum("c_375", var7[0, 0]);
                        s_rt.Checksum("c_376", var8);
                    }

                    var1 = 10L;
                }
                else
                {
                    uint[] var9 = new uint[]{4195846735U, 2263869289U, 3640318133U, 4294967294U, 1U, 0U, 1190475448U, 4294967294U};
                    M83();
                    var9[0]++;
                    s_rt.Checksum("c_377", var9[0]);
                }
            }

            ushort var10 = (ushort)M84();
            s_rt.Checksum("c_378", var10);
        }
        else
        {
            try
            {
                {
                    byte[][] var11 = new byte[][]{new byte[]{254}, new byte[]{219}};
                    s_rt.Checksum("c_379", var11[0][0]);
                }
            }
            finally
            {
                long var12 = var1++;
                try
                {
                    var1 = (long)M82();
                }
                finally
                {
                    M83();
                }

                var1 = (long)M82();
                s_rt.Checksum("c_380", var12);
            }

            if (s_61)
            {
                s_42 = (short)(s_19 * (uint)(s_49-- ^ -2105704503));
                if (s_41 < 1)
                {
                    s_39 = s_39;
                    var1 = 7991231942559546856L;
                    ++s_69;
                    ulong var13 = (ulong)(s_1[0] / (ushort)(s_17-- | 1));
                    int var14 = 2;
                    short var15 = (short)M84();
                    s_rt.Checksum("c_381", var13);
                    s_rt.Checksum("c_382", var14);
                    s_rt.Checksum("c_383", var15);
                }
                else
                {
                    short var16 = -18079;
                    if (true && s_48)
                    {
                        var1 = var1;
                        bool var17 = true;
                        byte[] var18 = new byte[]{2};
                        var16 = var16;
                        s_rt.Checksum("c_384", var17);
                        s_rt.Checksum("c_385", var18[0]);
                    }
                    else
                    {
                        s_70 = (ulong)M84();
                    }

                    s_rt.Checksum("c_386", var16);
                }

                M84();
            }
        }

        var1 = s_29;
        ushort var19 = (ushort)((byte)(var1 & s_27) | 2132440270U);
        byte var20 = 18;
        s_rt.Checksum("c_387", var1);
        s_rt.Checksum("c_388", var19);
        s_rt.Checksum("c_389", var20);
        return var1;
    }

    static ref uint M82()
    {
        M83();
        s_18 *= (byte)M84();
        s_64[0] = 100;
        {
            s_62 = (byte)M84();
        }

        uint var0 = s_67--;
        var0 = var0--;
        var0 = var0;
        s_rt.Checksum("c_368", var0);
        return ref s_33;
    }

    static uint M83()
    {
        if (s_46)
        {
            M84();
            uint var0 = 228150594U;
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
        {
            M87();
        }

        if (s_55[0])
        {
            arg3 = arg0 - s_29++;
            if (s_39)
            {
                ushort var0 = arg1;
                {
                    ulong var1 = arg2;
                    s_rt.Checksum("c_423", var1);
                }

                s_rt.Checksum("c_424", var0);
            }

            arg5[0] = (sbyte)M87();
            arg0 = arg0;
        }
        else
        {
            if (s_14[0] != arg3)
            {
                arg3 = arg3--;
                arg5 = arg5;
                ushort[][][] var2 = new ushort[][][]{new ushort[][]{new ushort[]{0, 1, 0, 3054, 25348}}};
                arg0 = -10;
                s_rt.Checksum("c_425", var2[0][0][0]);
            }
            else
            {
                try
                {
                    arg5[0] += (sbyte)M87();
                    s_46 = s_46;
                    ushort var3 = s_3[0];
                    s_rt.Checksum("c_426", var3);
                }
                finally
                {
                    s_69 = (sbyte)(-1 / (byte)(s_15-- | 1));
                    s_49 = s_20[0];
                    arg2 = 4133594121121780354UL;
                    try
                    {
                        if (s_48)
                        {
                            arg1 = s_17;
                            ulong var4 = arg2;
                            s_rt.Checksum("c_427", var4);
                        }
                    }
                    finally
                    {
                        arg3 = s_6;
                    }
                }

                arg2 = arg2;
                s_1[0] = 2799076913U;
                s_3 = new ushort[]{60332, 10, 399, 15886, 2, 10673, 1, 16825, 1};
                arg5[0] = arg5[0];
                arg4 = arg3;
            }

            arg3 ^= 2911244023753485134L;
        }

        {
            arg3 = arg4--;
        }

        if (arg2 > arg2)
        {
            arg4 = s_40[0][0, 0]--;
            arg5 = new sbyte[]{126, -119, -57, 1, 49, -127, -128};
            arg0 = arg0;
            M87();
            {
                if (s_55[0])
                {
                    arg2 = s_47;
                    int[, ] var5 = new int[, ]{{-1, 0, 1, 1, 10, 0, 1535855136}, {-1865455824, 0, 1, 1, -2147483648, -2147483647, 1}};
                    s_rt.Checksum("c_428", var5[0, 0]);
                }
            }
        }
        else
        {
            if (arg0 < arg4)
            {
                s_76 = (ushort)(arg1 + (byte)M87());
            }
            else
            {
                if (s_7)
                {
                    s_8 = -7485;
                }
            }

            arg1++;
            M87();
            {
                if (s_46)
                {
                    try
                    {
                        ushort var6 = arg1++;
                        s_rt.Checksum("c_429", var6);
                    }
                    finally
                    {
                        arg5[0] = s_9[0];
                    }
                }

                if (s_36[0][0])
                {
                    sbyte var7 = arg5[0]++;
                    s_rt.Checksum("c_430", var7);
                }

                if (s_39)
                {
                    if (s_39)
                    {
                        sbyte var8 = arg5[0];
                        s_50[0] = new ushort[]{65535, 65534, 40471, 50213, 2651, 47708};
                        s_rt.Checksum("c_431", var8);
                        return new short[][]{new short[]{-32444, -20301, 3302, 0, -32767}, new short[]{9804, 0}};
                    }
                    else
                    {
                        M87();
                        ulong var9 = arg2;
                        if (385443134877993331L < arg1)
                        {
                            s_45[0, 0][0] = new byte[][, ]{new byte[, ]{{117, 126, 110, 131, 1, 86, 132, 178, 254, 254}}, new byte[, ]{{1, 95, 99, 121, 149, 1, 158, 0, 213, 239}}, new byte[, ]{{115, 36, 151, 137, 223, 1, 0, 190, 149, 70}}, new byte[, ]{{226, 1, 2, 0, 1, 1, 82, 8, 160, 35}}, new byte[, ]{{92, 195, 1, 254, 7, 253, 255, 254, 2, 2}, {1, 21, 0, 109, 1, 254, 1, 62, 0, 0}}, new byte[, ]{{1, 0, 223, 191, 26, 0, 1, 48, 137, 1}, {1, 51, 255, 73, 144, 1, 139, 253, 28, 0}}, new byte[, ]{{62, 98, 255, 255, 254, 0, 229, 159, 114, 1}}, new byte[, ]{{220, 255, 193, 1, 254, 1, 10, 1, 1, 1}}, new byte[, ]{{254, 131, 1, 115, 1, 254, 1, 254, 10, 1}}, new byte[, ]{{91, 1, 219, 254, 145, 112, 1, 5, 0, 123}}};
                        }
                        else
                        {
                            arg3 = s_44;
                            if (s_39)
                            {
                                short var10 = (short)M87();
                                arg6 = arg6;
                                s_rt.Checksum("c_432", var10);
                            }

                            {
                                try
                                {
                                    M87();
                                    M87();
                                }
                                finally
                                {
                                    s_77 = (long)M87();
                                }

                                arg5 = s_72;
                                s_47 += 2546063643722984211UL;
                                ushort var11 = s_17;
                                sbyte[] var12 = arg5;
                                sbyte[] var13 = arg5;
                                s_rt.Checksum("c_433", var11);
                                s_rt.Checksum("c_434", var12[0]);
                                s_rt.Checksum("c_435", var13[0]);
                            }

                            bool var14 = s_61;
                            M87();
                            s_78 = arg5;
                            s_rt.Checksum("c_436", var14);
                        }

                        arg6 = M87();
                        s_rt.Checksum("c_437", var9);
                    }

                    long var15 = arg4;
                    arg5[0] = -8;
                    s_rt.Checksum("c_438", var15);
                }

                if (1 < (byte)M87())
                {
                    arg1 = arg1;
                }
            }

            arg6 = arg6;
            if (s_48)
            {
                arg6 = M87();
                arg1 = arg1;
            }

            M87();
            s_79 = arg3;
            try
            {
                {
                    arg4 = ref arg4;
                }
            }
            finally
            {
                arg6 = (uint)(arg3++ - 59);
                uint var16 = M87();
                s_rt.Checksum("c_439", var16);
            }

            arg4 = 0L;
            M87();
            M87();
            if (s_53[0, 0, 0])
            {
                ref ulong[] var17 = ref s_10;
                sbyte var18 = (sbyte)(-96 & arg6);
                ref short var19 = ref s_54[0];
                M87();
                s_80 = 1;
                M87();
                s_rt.Checksum("c_440", var17[0]);
                s_rt.Checksum("c_441", var18);
                s_rt.Checksum("c_442", var19);
            }
        }

        arg5[0] = arg5[0];
        s_rt.Checksum("c_443", arg0);
        s_rt.Checksum("c_444", arg1);
        s_rt.Checksum("c_445", arg2);
        s_rt.Checksum("c_446", arg3);
        s_rt.Checksum("c_447", arg4);
        s_rt.Checksum("c_448", arg5[0]);
        s_rt.Checksum("c_449", arg6);
        return new short[][]{new short[]{-6907, -4600, 13194}, new short[]{-10, 0, 14662}, new short[]{-2}, new short[]{-32767}, new short[]{2, 1627, 0, -23255}};
    }

    static uint M87()
    {
        s_25 = s_15++;
        s_45 = s_45;
        return 0U;
    }

    static uint M88()
    {
        sbyte var0 = s_16[0, 0]++;
        s_rt.Checksum("c_460", var0);
        return 0U;
    }

    static ulong M89(int arg0)
    {
        bool var0 = false;
        s_rt.Checksum("c_513", arg0);
        s_rt.Checksum("c_514", var0);
        return s_57;
    }

    static bool[] M90(ref uint arg0, ref sbyte arg1, ref ushort arg2)
    {
        long var0 = s_77--;
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
        s_42 = s_54[0];
        s_72 = s_2;
        {
            M94();
        }

        M94();
        try
        {
            if (s_36[0][0])
            {
                s_84[0] = -5083683065808085193L;
                ulong var0 = (ulong)(s_105++ / (uint)((uint)(s_30[0] - (byte)M95()) | 1));
                ref short[] var1 = ref s_75;
                var0 = s_70;
                ref ulong var2 = ref var0;
                s_43 = (uint)M95();
                ulong var3 = var2;
                M95();
                ref short var4 = ref var1[0];
                s_rt.Checksum("c_587", var0);
                s_rt.Checksum("c_588", var1[0]);
                s_rt.Checksum("c_589", var2);
                s_rt.Checksum("c_590", var3);
                s_rt.Checksum("c_591", var4);
            }
            else
            {
                M94();
                ref long var5 = ref s_77;
                short var6 = 0;
                short var7 = var6;
                var5 = (long)M94();
                var7 = (short)M94();
                var6 = var6;
                var6 |= (short)(0 ^ s_32[0, 0, 0]++);
                int[] var8 = new int[]{-599439334, 0, 8885877, -10};
                s_rt.Checksum("c_592", var5);
                s_rt.Checksum("c_593", var6);
                s_rt.Checksum("c_594", var7);
                s_rt.Checksum("c_595", var8[0]);
            }
        }
        finally
        {
            if (s_87)
            {
                s_81 = s_13[0, 0]--;
                try
                {
                    s_18 = M94();
                }
                finally
                {
                    s_52 = new byte[]{255, 0, 254, 255, 254, 4, 11, 2, 116, 164};
                }

                s_4[0][0] = (int)M95();
                s_23[0] = true;
            }
        }

        if (s_46)
        {
            M95();
            s_28 = s_8;
            if (s_7)
            {
                s_86[0] = s_86[0];
            }
            else
            {
                return s_30[0]--;
            }

            s_79 = M95();
            s_12 |= s_106[0][0]++;
            {
                s_30 = new short[]{1, 32767, 1, -21897, -30264, 14511};
            }
        }
        else
        {
            short var9 = -11751;
            if (s_61)
            {
                var9 = var9;
            }

            s_rt.Checksum("c_596", var9);
        }

        long[][] var10 = new long[][]{new long[]{-1804320204239990116L, -4264586771466435926L, 2L, -7702201302103527220L}, new long[]{4781452352609459574L, 9223372036854775806L, 389754763352629804L, 1568203199872872652L, -1L}, new long[]{-3066690003156906801L}, new long[]{-5609213770648575758L, 1L, 7908913239385814582L, -2429844479631565224L, -927861393819127281L}, new long[]{-6070551445227531191L, 3029677483310564480L, -9223372036854775808L}, new long[]{-4894443611945476743L, -6975860629182862530L, 9223372036854775807L, 1508581265852148942L, -1L}, new long[]{6356501024021376889L}, new long[]{-1L, -9223372036854775808L, -2801762192325122101L}, new long[]{9223372036854775806L, -2823443675423125832L, -1523596525408431918L, -346849017519105219L, 1L}};
        ulong[] var11 = new ulong[]{0UL, 7129407728525251333UL, 14591426676812996608UL, 0UL, 10UL, 13273875952317092802UL, 0UL, 902729150452946996UL, 1UL};
        if (s_48)
        {
            if (0 != var10[0][0]--)
            {
                short var12 = 30830;
                s_rt.Checksum("c_597", var12);
            }
            else
            {
                {
                    var11[0] = var11[0];
                    M95();
                    ushort var13 = 63907;
                    s_rt.Checksum("c_598", var13);
                }

                s_98 = new ulong[, ]{{2148393808839811230UL, 18446744073709551614UL, 18446744073709551614UL, 1035937708940777714UL}, {13697918961974466736UL, 11717846184693299554UL, 13722819525325355624UL, 2247746308099097884UL}, {10540283444403706436UL, 6757036407142238732UL, 0UL, 14024437829224048179UL}, {18446744073709551614UL, 7914211132217839488UL, 16578461659949134774UL, 14465337582326980242UL}, {18398725352080975022UL, 1UL, 5239883403862014198UL, 1752025111320733992UL}, {9959953734142250987UL, 1UL, 1574892175629230185UL, 1UL}, {7393781407573073074UL, 0UL, 2101682042345731621UL, 11022580352752585431UL}, {1824182079748711329UL, 2902325436838597654UL, 12573675114391428985UL, 14997318824786508195UL}, {18446744073709551615UL, 18446744073709551615UL, 15210814936127060849UL, 8987087947043417829UL}};
            }
        }

        {
            var10 = var10;
            var11 = s_10;
            {
                sbyte var14 = (sbyte)((uint)((sbyte)M94() + s_92) ^ -1189791304);
                s_rt.Checksum("c_599", var14);
            }

            s_107 = 0U;
        }

        s_rt.Checksum("c_600", var10[0][0]);
        s_rt.Checksum("c_601", var11[0]);
        return 17624;
    }

    static byte M94()
    {
        s_45[0, 0][0][0] = new byte[, ]{{209, 255, 204, 234}, {255, 0, 234, 97}, {1, 0, 245, 254}, {4, 0, 254, 255}, {254, 57, 203, 71}, {204, 0, 0, 217}, {0, 0, 2, 46}, {10, 0, 254, 123}, {108, 240, 39, 0}};
        ++s_64[0];
        s_18 -= 1;
        if (s_48)
        {
            {
                s_38 = s_38;
                M95();
                s_22[0] = new uint[]{247198394U, 4294967294U, 1712706215U, 0U, 3736337520U, 4294967294U, 2553807794U, 2607168429U, 10U, 1818355958U};
                {
                    s_59[0] = s_59[0];
                    s_103 = s_15;
                    sbyte var0 = 0;
                    short[] var1 = new short[]{10, -21449, 1, 0, 27036, 2, 1};
                    s_rt.Checksum("c_579", var0);
                    s_rt.Checksum("c_580", var1[0]);
                }

                s_104 = false;
                if (s_46)
                {
                    M95();
                    int var2 = s_32[0, 0, 0];
                    var2 = var2++;
                    M95();
                    if (141 == (sbyte)M95())
                    {
                        int var3 = -285794022;
                        s_rt.Checksum("c_581", var3);
                    }
                    else
                    {
                        var2 = (byte)M95() | s_82;
                        ulong var4 = 1UL;
                        s_rt.Checksum("c_582", var4);
                    }

                    if (s_23[0])
                    {
                        return 1;
                    }

                    ref byte var5 = ref s_52[0];
                    var5 = ref s_65;
                    var5++;
                    M95();
                    s_rt.Checksum("c_583", var2);
                    s_rt.Checksum("c_584", var5);
                }

                s_86[0] = new ulong[][][]{new ulong[][]{new ulong[]{0UL}, new ulong[]{1UL, 1UL}}, new ulong[][]{new ulong[]{0UL, 5634140004105367914UL, 10620243800356882977UL, 1UL, 18446744073709551615UL}, new ulong[]{1UL, 5164040578854752900UL, 1UL, 0UL, 0UL}, new ulong[]{0UL, 12624659312759357922UL, 18446744073709551615UL, 18446744073709551614UL}, new ulong[]{1UL, 6727381546140989700UL, 18446744073709551615UL, 16325816462182620072UL}}, new ulong[][]{new ulong[]{18446744073709551615UL, 10UL, 687431553578071704UL, 18446744073709551614UL, 13528895368061407839UL}, new ulong[]{15065950285103811339UL, 0UL, 6404199475866518018UL, 18446744073709551615UL, 18205619113900242636UL}, new ulong[]{17279035542307619701UL}}, new ulong[][]{new ulong[]{1UL}, new ulong[]{1UL}, new ulong[]{15853517358074210031UL, 0UL, 0UL, 13999341601005782613UL, 1UL, 1UL}, new ulong[]{17501378482835405867UL}, new ulong[]{0UL, 18446744073709551615UL, 1UL, 1386431846617364482UL, 18446744073709551614UL, 0UL}, new ulong[]{1UL, 1UL, 9690654636511729754UL, 8693816827084428426UL, 1UL}, new ulong[]{0UL, 1UL, 2522771143594610929UL, 9579106491310643535UL, 14927614996448803636UL, 18446744073709551615UL}}, new ulong[][]{new ulong[]{1518653880104063235UL, 1UL}, new ulong[]{3080123564853631838UL}, new ulong[]{5226505629115637373UL, 1UL}, new ulong[]{5971360688258792577UL}, new ulong[]{18446744073709551615UL, 11492213492891671869UL}, new ulong[]{3193496018928735088UL, 6807800183465576032UL}, new ulong[]{8833060680029474689UL, 1UL}}, new ulong[][]{new ulong[]{352819187669396115UL, 1297012409357490592UL, 750916169115245274UL, 0UL, 18446744073709551615UL}, new ulong[]{1UL, 1UL, 14709881753837751353UL, 0UL, 1UL}, new ulong[]{4217436820375717523UL, 14990943481305847231UL, 1UL, 1UL, 0UL, 7742607927909304079UL}, new ulong[]{1956860606716357631UL}, new ulong[]{1290251422385763328UL}}};
                s_30 = s_54;
            }
        }
        else
        {
            s_56 = s_2[0];
            if (s_96++ == M95())
            {
                ulong var6 = (ulong)(s_84[0]++ % (int)(s_38[0][0] | 1));
                s_rt.Checksum("c_585", var6);
            }

            s_32[0, 0, 0] = -1665714944;
        }

        s_105 = s_102;
        s_106 = new int[][]{new int[]{-2147483647, 1687715841, 1661844263, 2147483646}, new int[]{-625160187, 2147483647}, new int[]{2147483647, -2, 2147483646, 1577425812, 0, -2147483647, -120016367, -10}};
        ref long var7 = ref s_95;
        s_rt.Checksum("c_586", var7);
        return (byte)(10830 * s_17++);
    }

    static long M95()
    {
        if (s_61)
        {
            s_72 = s_72;
            s_11 = s_44;
        }

        int var0 = s_12;
        s_rt.Checksum("c_578", var0);
        return s_14[0]++;
    }

    static int M96(short arg0)
    {
        ulong[] var0 = new ulong[]{0UL};
        {
            M97();
        }

        s_rt.Checksum("c_674", arg0);
        s_rt.Checksum("c_675", var0[0]);
        return M97();
    }

    static int M97()
    {
        if (s_53[0, 0, 0])
        {
            s_10 = s_86[0][0][0];
            s_114 = s_80;
        }
        else
        {
            s_119 = s_30[0];
            {
                {
                    s_110 = 197;
                    uint var0 = s_68--;
                    ulong[] var1 = M98();
                    ushort var2 = 65535;
                    var1 = new ulong[]{1945646037070402773UL, 2274636528124739144UL, 1UL, 8382604678762103937UL, 0UL, 3307613365432727840UL, 6118034745535210860UL, 18446744073709551615UL, 0UL, 0UL};
                    s_30[0] = 5600;
                    s_rt.Checksum("c_666", var0);
                    s_rt.Checksum("c_667", var1[0]);
                    s_rt.Checksum("c_668", var2);
                }

                ushort var3 = 1;
                s_rt.Checksum("c_669", var3);
            }

            s_124 = s_67--;
            s_48 ^= s_79 < s_119--;
            s_117 = new sbyte[][]{new sbyte[]{127, 127, -1}};
            s_104 = s_39;
        }

        {
            {
                bool var4 = true;
                if (var4)
                {
                    byte var5 = (byte)M99(-4387900683214534935L);
                    s_rt.Checksum("c_670", var5);
                }
                else
                {
                    bool var6 = var4;
                    s_rt.Checksum("c_671", var6);
                }

                s_rt.Checksum("c_672", var4);
            }

            s_71 = (ushort)M100();
            try
            {
                s_88 = s_85;
            }
            finally
            {
                short var7 = s_30[0];
                s_rt.Checksum("c_673", var7);
            }
        }

        s_84 = new long[]{1L};
        {
            s_50[0][0] = 6766;
        }

        return -1703225555;
    }

    static ulong[] M98()
    {
        M99(s_95--);
        s_87 = true;
        try
        {
            ulong var0 = s_90;
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
        if (s_36[0][0])
        {
            s_117 = new sbyte[][]{new sbyte[]{1, 0, 1, -1}, new sbyte[]{1, 0, 77, -98}, new sbyte[]{-56, -7}, new sbyte[]{-128, 0, 29}};
            if (s_39)
            {
                if (s_46)
                {
                    arg0 = s_40[0][0, 0];
                }
                else
                {
                    {
                        long[, ] var0 = new long[, ]{{9157029538085328477L, 9223372036854775806L, 8429812067056106736L, -854370081890891623L}};
                        s_rt.Checksum("c_649", var0[0, 0]);
                    }
                }

                {
                    ref byte[][] var1 = ref s_120;
                    M100();
                    ref long var2 = ref s_11;
                    var1[0][0] = var1[0][0];
                    short var3 = s_28;
                    ushort var4 = (ushort)((ulong)M100() | s_63);
                    s_106[0][0] = -2147483648;
                    s_50[0] = s_3;
                    s_70 = (ulong)M100();
                    s_rt.Checksum("c_651", var1[0][0]);
                    s_rt.Checksum("c_652", var2);
                    s_rt.Checksum("c_653", var3);
                    s_rt.Checksum("c_654", var4);
                }

                int var5 = (int)M100();
                M100();
                M100();
                {
                    arg0 = arg0;
                    s_121 = new short[][][]{new short[][]{new short[]{8094, 14307}, new short[]{10, -10, -12874}, new short[]{-18415, 32767, -125}, new short[]{-32767, -32767, 1}}, new short[][]{new short[]{13106, -11296}}, new short[][]{new short[]{-2, 32767}, new short[]{-32768, 3401, -26214}, new short[]{32767, 1}, new short[]{7030, -2326, 0}, new short[]{-32767}}, new short[][]{new short[]{13290, 23470, -25916}, new short[]{1, 1}, new short[]{32766, 10, 1}}, new short[][]{new short[]{1, 1}, new short[]{-1, -11807}, new short[]{22955, -32768}, new short[]{-7571}, new short[]{-32768, 1}}, new short[][]{new short[]{-13140}, new short[]{-21018, -11170}}, new short[][]{new short[]{-1, -16617, -2}}, new short[][]{new short[]{-32767}, new short[]{4522}}, new short[][]{new short[]{12397}, new short[]{-32767, 23203, -32767}, new short[]{0}, new short[]{19182}}};
                    arg0 = 0L;
                }

                sbyte[] var6 = new sbyte[]{0, -6, 0, 10, 31, -47, 0, -128, 127, 126};
                s_rt.Checksum("c_655", var5);
                s_rt.Checksum("c_656", var6[0]);
            }
            else
            {
                try
                {
                    if (s_22[0][0] <= 126)
                    {
                        short var7 = s_113;
                        arg0 = arg0;
                        s_rt.Checksum("c_657", var7);
                    }
                }
                finally
                {
                    long[] var8 = new long[]{1L, 0L, 1244037781667181146L, 1L};
                    long var9 = arg0;
                    s_rt.Checksum("c_658", var8[0]);
                    s_rt.Checksum("c_659", var9);
                }
            }

            if (s_23[0])
            {
                if (s_104)
                {
                    M100();
                    M100();
                    arg0 = arg0;
                    arg0 = 6400336122145366696L;
                    bool var10 = s_89;
                    var10 = s_61;
                    s_122 = var10;
                    var10 = var10;
                    s_rt.Checksum("c_660", var10);
                }
                else
                {
                    if (s_104)
                    {
                        {
                            arg0 = arg0++;
                        }
                    }
                    else
                    {
                        uint[] var11 = s_1;
                        s_rt.Checksum("c_661", var11[0]);
                    }
                }
            }

            arg0 = arg0;
        }
        else
        {
            arg0 = -5177756250883205298L;
            arg0 = -1092723326963748808L;
        }

        M100();
        ref sbyte var12 = ref s_16[0, 0];
        s_rt.Checksum("c_662", arg0);
        s_rt.Checksum("c_663", var12);
        return arg0--;
    }

    static short M100()
    {
        sbyte[] var0 = s_78;
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