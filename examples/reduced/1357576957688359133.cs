// Generated by Fuzzlyn v1.4 on 2021-08-27 00:26:15
// Run on .NET 7.0.0-dev on Arm Linux
// Seed: 1357576957688359133
// Reduced from 250.8 KiB to 20.4 KiB in 14:01:18
// Crashes the runtime
public interface I0
{
}

public interface I1
{
}

public interface I2
{
}

public interface I3
{
}

public class C0
{
    public bool F0;
    public ulong F1;
    public sbyte F2;
    public short F3;
    public long F4;
    public ushort F5;
    public int F6;
    public int F7;
    public bool F8;
    public ushort F9;
    public C0(bool f0, ulong f1, sbyte f2, short f3, long f4, ushort f5, int f6, bool f8)
    {
    }
}

public struct S0
{
    public short F0;
    public C0 F1;
    public ulong F2;
    public byte F3;
    public long F4;
    public S0(short f0, C0 f1, ulong f2, byte f3, long f4): this()
    {
        F1 = f1;
    }
}

public class C1
{
    public C0 F0;
    public int F1;
    public ushort F2;
    public ushort F3;
    public sbyte F4;
    public short F5;
    public sbyte F6;
    public uint F7;
    public C1(C0 f0)
    {
        F0 = f0;
    }
}

public struct S1
{
    public S1(uint f0): this()
    {
    }
}

public class C2 : I0
{
    public S1 F2;
    public C2(S1 f2, ulong f4)
    {
    }
}

public class C3 : I0
{
    public ulong F0;
    public int F1;
    public uint F2;
    public bool F3;
    public uint F4;
    public C1 F5;
    public C3(ulong f0, C1 f5)
    {
        F5 = f5;
    }
}

public class C4
{
    public C2 F5;
    public C4(C2 f5)
    {
        F5 = f5;
    }
}

public class C5 : I3
{
    public S0 F0;
    public C5(S0 f0, byte f1, C0 f2, bool f3, long f4, short f5)
    {
    }
}

public class C6
{
    public ushort F0;
    public S0 F1;
    public byte F2;
    public long F3;
    public C6(S0 f1, long f3)
    {
        F1 = f1;
    }
}

public struct S2 : I1
{
    public sbyte F0;
}

public class Program
{
    public static int s_1;
    public static int[] s_2 = new int[]{0};
    public static C3[, ] s_4 = new C3[, ]{{new C3(0, new C1(new C0(true, 0, 0, 0, 0, 0, 0, false)))}};
    public static C2 s_5;
    public static C4[, ] s_7 = new C4[, ]{{new C4(new C2(new S1(0), 0))}};
    public static ulong s_36;
    public static I1 s_38;
    public static void Main()
    {
        M0();
    }

    public static void M0()
    {
        short var0 = 0;
        long var1 = 8919936813394142355L;
        if (var1 >= 0)
        {
            long var3 = var1;
            System.Console.WriteLine(true);
            System.Console.WriteLine(var3);
            System.Console.WriteLine(29888);
        }

        s_1 = s_1++;
        var vr10 = new C5[]{new C5(new S0(0, new C0(true, 0, 63, 21759, -3435316687531662524L, 53698, 1, false), 0, 158, 8217199046273857570L), 0, new C0(false, 0, 1, 0, 0, 0, 0, false), true, -10L, -32768), new C5(new S0(-8635, new C0(false, 0, -10, 23944, -9223372036854775807L, 1, 219950919, false), 7181421418345184275UL, 58, -9223372036854775807L), 104, new C0(true, 7999659046805277730UL, 51, -32767, 2904996852574619615L, 0, 2147483646, true), false, -7432806038433249047L, 0), new C5(new S0(-30226, new C0(false, 13023831782274416264UL, 0, -32768, -7702851836718029895L, 36055, 1, false), 0, 0, 0), 0, new C0(false, 0, 0, -26249, 0, 62531, 0, true), false, 0, -5601), new C5(new S0(1, new C0(false, 0, 0, 32766, -2173858623870065904L, 0, 635322703, false), 10282306968105895983UL, 0, 7074230165866769711L), 0, new C0(false, 7953269047624843366UL, 0, -24934, 0, 0, 0, true), false, 9223372036854775807L, 0), new C5(new S0(0, new C0(true, 10285997668837669036UL, 0, -22422, 0, 0, 0, false), 0, 0, 0), 1, new C0(false, 13714680512310551722UL, 0, 28617, 4449994968051528052L, 42826, -2147483647, false), false, -6657250949125787291L, 0), new C5(new S0(4458, new C0(false, 0, 1, 0, 2658527197282495044L, 0, 0, false), 0, 1, 3267800089665444813L), 0, new C0(true, 0, 0, 0, 6698451500599632156L, 1, 0, false), false, 0, 0), new C5(new S0(0, new C0(false, 0, 1, 0, -9021304808048196679L, 0, 0, false), 10812913055481374074UL, 0, 7126173780996687094L), 0, new C0(true, 1491251480615320577UL, 0, 0, 4509395488687536237L, 0, 0, true), true, -3462454146322496388L, 1), new C5(new S0(0, new C0(false, 12974099833135094384UL, 0, 1, 57596481157179654L, 1, 0, true), 5277146187158448554UL, 1, 5237624385057612045L), 0, new C0(true, 5362321680828807528UL, 0, 0, 7277796332736805447L, 0, 0, false), true, 4557821514362984380L, 0)};
        var vr12 = new C2(new S1(0), 0);
        var vr15 = new C5(new S0(1, new C0(true, 5397628846420268040UL, 0, 0, 0, 0, 0, true), 0, 1, -4717557763976505714L), 1, new C0(true, 0, 0, 0, -9223372036854775808L, 0, 0, true), false, 9223372036854775806L, 0);
        var vr0 = M2(vr10, vr12);
        C6 vr18;
        if (s_2[0]++ != 1)
        {
            var vr5 = var0--;
            C3[, ] vr19;
            uint vr21;
            var vr2 = new C5(new S0(0, new C0(false, 16029842430425901260UL, 0, 1, 9223372036854775806L, 1, 0, true), 0, 0, 7165734296024093807L), 0, new C0(false, 16541390048119829480UL, 1, 0, 7525569264963914515L, 0, 1, false), false, -4729335382065920179L, 0);
            M2(new C5[]{new C5(new S0(0, new C0(true, 0, -127, 32766, 4053717418090700197L, 38904, 16836044, false), 10963718895599001408UL, 0, 4725108666085819312L), 1, new C0(false, 0, 0, 15239, -10L, 0, 1988245528, true), true, -8069679482167286470L, 0), new C5(new S0(1, new C0(true, 0, 12, -8984, 1117968801330767191L, 46340, 0, true), 11159930234110517675UL, 0, -1444881995916422838L), 0, new C0(false, 5273081854003747965UL, 0, -32768, -9223372036854775807L, 44260, -2147483647, true), false, 4893682902725395247L, 0), new C5(new S0(1295, new C0(true, 0, 75, -32767, -6740527155714796968L, 20679, 683828438, true), 0, 0, 4614077971168881761L), 141, new C0(false, 18446744073709551614UL, 0, 0, 0, 43063, 1, false), false, 0, 0), new C5(new S0(0, new C0(false, 16795873516586956201UL, 1, -10, 2395730612026707194L, 1, -1, false), 0, 210, 826766094393227726L), 0, new C0(false, 0, 0, 0, 9223372036854775806L, 48389, 1393355699, false), false, 0, 0), new C5(new S0(0, new C0(false, 0, 0, 0, 0, 1, 0, false), 11674986617868095095UL, 0, 598337193470750911L), 1, new C0(true, 0, 0, 0, 2364854475536099814L, 0, 0, false), false, 777125080532227462L, 0)}, s_4[0, 0]);
        }

        C1 var6 = s_4[0, 0].F5;
        S2 var7 = new S2();
        C6 var8 = new C6(new S0(0, new C0(false, 0, 1, 0, 0, 1, 0, false), 0, 0, -153755193325483564L), -310369028636573640L);
        bool[][] var9 = new bool[][]{new bool[]{true}, new bool[]{true}, new bool[]{false}};
        S0[] var11 = new S0[]{new S0(0, new C0(true, 0, 0, 1, 6533520229148384991L, 1, 0, true), 14794779213505343840UL, 0, 0), new S0(0, new C0(false, 0, 0, 0, 0, 0, -1, false), 0, 1, -5996667622774456244L), new S0(0, new C0(true, 8900108835072316163UL, 0, 0, 0, 1, 0, true), 1437645327715396914UL, 0, -2905374372288371523L)};
        try
        {
            C1 var12 = s_4[0, 0].F5;
            System.Console.WriteLine(var12.F0.F0);
            System.Console.WriteLine(var12.F0.F1);
            System.Console.WriteLine(var12.F0.F2);
            System.Console.WriteLine(var12.F0.F3);
            System.Console.WriteLine(var12.F0.F4);
            System.Console.WriteLine(var12.F0.F5);
            System.Console.WriteLine(var12.F0.F6);
            System.Console.WriteLine(var12.F0.F7);
            System.Console.WriteLine(var12.F0.F8);
            System.Console.WriteLine(var12.F0.F9);
            System.Console.WriteLine(var12.F1);
            System.Console.WriteLine(var12.F2);
            System.Console.WriteLine(var12.F3);
            System.Console.WriteLine(var12.F4);
            System.Console.WriteLine(var12.F5);
            System.Console.WriteLine(var12.F6);
            System.Console.WriteLine(var12.F7);
        }
        finally
        {
            bool var13 = var11[0].F1.F7 == var11[0].F1.F4;
            System.Console.WriteLine(var13);
        }

        if (var6.F0.F0)
        {
            if (var6.F3 > 0)
            {
                C6[] var14 = new C6[]{new C6(new S0(0, new C0(true, 4894015676058405489UL, 0, 0, 6417588827056933881L, 0, 0, true), 15350192434468783472UL, 0, 1988304643189712055L), 140566211796407586L), new C6(new S0(0, new C0(false, 5908031262712740518UL, 1, 0, -648766417699693982L, 0, 0, false), 10960650043286698976UL, 0, -9223372036854775807L), -9223372036854775808L)};
                var vr4 = new C5(new S0(0, new C0(true, 2750234928069453689UL, 0, 0, 3820996752235749531L, 0, 0, true), 15199397645613503477UL, 0, 8672943078840880622L), 0, new C0(true, 0, 0, 0, 4866679947239757738L, 1, 0, false), true, -9223372036854775808L, 0);
                var vr8 = new C5[]{new C5(new S0(0, new C0(true, 15133144080400128440UL, 0, 31699, 1538380587194657559L, 1, 662936229, true), 14906870071616449904UL, 1, 6110373961553497137L), 254, new C0(false, 0, 55, 0, -2104255245253989200L, 11395, 2147483647, true), false, 0, 32766), new C5(new S0(0, new C0(true, 7765994549044017869UL, 0, 22542, -6030479334757144518L, 64372, -1210105491, false), 0, 1, 0), 0, new C0(false, 0, 0, -1, -9223372036854775808L, 31378, -305655386, true), false, 0, -32767), new C5(new S0(3874, new C0(false, 5029936125424068815UL, 1, -32767, -9223372036854775807L, 1, -657481317, false), 0, 1, 0), 1, new C0(false, 0, 0, 9188, 0, 0, 799307734, true), false, -9223372036854775808L, 0), new C5(new S0(0, new C0(false, 12559545677546952544UL, 0, 0, -1696914628700079928L, 0, -1714463296, true), 5234526917267710959UL, 0, -7642062724820266101L), 239, new C0(true, 0, 127, 0, -5495855022626404799L, 24029, 0, true), false, 5997108192590684384L, 6932), new C5(new S0(0, new C0(true, 0, 0, 19101, 2520918358024915815L, 0, -2, false), 5703041058379788016UL, 255, -1050011665348429887L), 194, new C0(false, 14225883205866090323UL, 1, 0, -1599763582107411218L, 0, -86779039, false), true, 6569939753788295700L, 0), new C5(new S0(0, new C0(true, 0, 0, 0, 0, 14880, 0, false), 15581517724098750260UL, 0, -8215579036745581172L), 192, new C0(false, 5964319847563562745UL, 0, -4672, 9223372036854775806L, 59699, -1582863597, true), true, 0, -32768), new C5(new S0(-32768, new C0(true, 10UL, 0, 32766, 6123781815464638194L, 65535, 0, false), 66695547534991797UL, 0, -9223372036854775807L), 1, new C0(true, 10774081305214863083UL, 0, 31837, -2L, 65534, -1764124864, true), false, 3149272785506912537L, 20752), new C5(new S0(0, new C0(false, 0, 0, 32767, -556277430020632720L, 10, -1457823047, false), 0, 0, -5715998439953689784L), 0, new C0(false, 0, 0, 0, -9223372036854775808L, 0, 0, false), true, 0, 0)};
                var vr39 = new C2(new S1(0), 5521595931677535281UL);
                I3 var15 = new C5(new S0(0, new C0(true, 3663961422805957103UL, 1, 1, 0, 0, 0, false), 0, 0, 465141842590077362L), 1, new C0(false, 11586632640779453664UL, 0, 1, -2993874752498878744L, 1, 0, false), true, 0, 0);
                I0 var16 = new C3(0, new C1(new C0(false, 10007381610074320180UL, 0, 0, -6014985482023908058L, 0, 0, false)));
                System.Console.WriteLine(var14[0].F0);
                System.Console.WriteLine(var14[0].F1.F0);
                System.Console.WriteLine(var14[0].F1.F1.F0);
                System.Console.WriteLine(var14[0].F1.F1.F1);
                System.Console.WriteLine(var14[0].F1.F1.F2);
                System.Console.WriteLine(var14[0].F1.F1.F3);
                System.Console.WriteLine(var14[0].F1.F1.F4);
                System.Console.WriteLine(var14[0].F1.F1.F5);
                System.Console.WriteLine(var14[0].F1.F1.F6);
                System.Console.WriteLine(var14[0].F1.F1.F7);
                System.Console.WriteLine(var14[0].F1.F1.F8);
                System.Console.WriteLine(var14[0].F1.F1.F9);
                System.Console.WriteLine(var14[0].F1.F2);
                System.Console.WriteLine(var14[0].F1.F3);
                System.Console.WriteLine(var14[0].F1.F4);
                System.Console.WriteLine(var14[0].F2);
                System.Console.WriteLine(var14[0].F3);
            }
            else
            {
                var8 = new C6(new S0(0, new C0(true, 0, 0, 0, 9223372036854775806L, 0, 0, false), 1412354934975776121UL, 0, 0), 0);
            }

            var8.F1.F4 = var8.F1.F4;
            var vr6 = var11[0];
            var vr13 = new C3(0, new C1(new C0(true, 16749527976113429918UL, 0, -1, 9223372036854775806L, 1, 0, false)));
            byte vr33;
        }
        else
        {
            var6 = new C1(new C0(false, 3989898733176259111UL, 1, 1, 9223372036854775807L, 0, 0, false));
        }

        C3 vr22;
        bool vr23;
        if (s_4[0, 0].F3)
        {
            var vr36 = new C5(new S0(1, new C0(true, 0, 0, 0, 0, 0, 0, true), 0, 0, 7963009663236657707L), 0, new C0(true, 2540243349022899834UL, 0, 0, -6080465653248237613L, 0, 0, true), false, 9223372036854775807L, 0);
            var vr37 = new S0[]{new S0(-2651, new C0(false, 8245635401275165724UL, 0, 32767, 3164752781006485570L, 1, 0, true), 4609613345138652614UL, 0, 8304823563617862348L), new S0(-8006, new C0(false, 16473080754261074754UL, 0, 32766, -8573132344986000820L, 4267, 2147483646, true), 8344797040724521149UL, 255, -188026686534671392L), new S0(32766, new C0(false, 0, 0, 32766, -250294014535786787L, 1, -381465296, true), 0, 211, -8882331798182947270L), new S0(0, new C0(false, 10136569390937555793UL, 0, 0, 0, 0, 0, false), 0, 0, 9223372036854775806L), new S0(-1, new C0(true, 83062140151256367UL, 0, 1, 0, 0, 0, false), 0, 1, -9223372036854775808L), new S0(0, new C0(false, 0, 0, 0, 9223372036854775806L, 0, 1, true), 0, 0, 0), new S0(0, new C0(false, 0, 0, 0, 176123487617774988L, 0, 0, false), 0, 0, 1323297810010522401L)};
            var vr1 = new C2(new S1(0), 0);
            var vr9 = new S0(0, new C0(true, 0, 0, 0, -9223372036854775808L, 0, 0, true), 10371918983186821393UL, 0, -7174399543039360649L);
            var vr11 = new S1(0);
            S0 vr29;
            I2 vr31;
            var vr38 = new C5[]{new C5(new S0(0, new C0(false, 13055129541247195840UL, 0, 0, 0, 0, 0, true), 0, 1, -9223372036854775808L), 0, new C0(false, 0, 0, 0, 0, 1, 0, false), false, 0, 1), new C5(new S0(1, new C0(true, 0, 0, 14167, 6992070087080365475L, 0, 0, false), 0, 0, -270843735412468942L), 0, new C0(false, 12883278351710280106UL, 0, 0, -5617022281301806634L, 36584, 2147483646, true), false, 0, -32767), new C5(new S0(1, new C0(false, 0, 0, 0, 1248054777231105209L, 0, 0, false), 0, 0, 0), 0, new C0(false, 0, 0, 0, -1289618642555578051L, 1, 0, false), true, -9223372036854775807L, 0)};
            var vr40 = new C2(new S1(0), 9900178071431145388UL);
            C5[] vr43;
            I0 vr44;
            C0 vr45;
            var8.F1.F4 -= var8.F1.F4;
            sbyte var17 = var6.F0.F2--;
            M10();
            System.Console.WriteLine(var17);
        }
        else
        {
            s_36 = 0;
            C3 var18 = new C3(0, new C1(new C0(false, 0, 1, 0, -5324155547547061656L, 0, 0, false)));
            var vr3 = s_5;
            var vr41 = new C0[]{new C0(true, 12326878605085588398UL, 0, 1, 0, 1, 0, false), new C0(true, 17080607403351773482UL, 0, 1, 0, 0, 1, false), new C0(true, 0, 0, 0, 9223372036854775807L, 1, 0, false)};
            S1 vr46;
            var vr42 = vr46;
            M15(vr42, var18.F5.F0.F4++, ref s_7, ref s_7[0, 0].F5.F2, new uint[]{1, 1, 1, 0});
            var8.F1.F1.F5 = var8.F1.F1.F5--;
            long var19 = var11[0].F4;
            if (var6.F0.F0)
            {
                s_38 = var7;
                C3[, ] vr25;
                short vr28;
                short vr26;
                uint vr27 = default(uint);
                sbyte var20 = (sbyte)vr27;
                System.Console.WriteLine(var20);
            }

            var18.F0 = var8.F1.F1.F1--;
            var8.F1.F1.F1 = var11[0].F2;
            var18.F5.F0.F6 = s_4[0, 0].F5.F0.F6;
            System.Console.WriteLine(var18.F0);
            System.Console.WriteLine(var18.F1);
            System.Console.WriteLine(var18.F2);
            System.Console.WriteLine(var18.F3);
            System.Console.WriteLine(var18.F4);
            System.Console.WriteLine(var18.F5.F0.F0);
            System.Console.WriteLine(var18.F5.F0.F1);
            System.Console.WriteLine(var18.F5.F0.F2);
            System.Console.WriteLine(var18.F5.F0.F3);
            System.Console.WriteLine(var18.F5.F0.F4);
            System.Console.WriteLine(var18.F5.F0.F5);
            System.Console.WriteLine(var18.F5.F0.F6);
            System.Console.WriteLine(var18.F5.F0.F7);
            System.Console.WriteLine(var18.F5.F0.F8);
            System.Console.WriteLine(var18.F5.F0.F9);
            System.Console.WriteLine(var18.F5.F1);
            System.Console.WriteLine(var18.F5.F2);
            System.Console.WriteLine(var18.F5.F3);
            System.Console.WriteLine(var18.F5.F4);
            System.Console.WriteLine(var18.F5.F5);
            System.Console.WriteLine(var18.F5.F6);
            System.Console.WriteLine(var18.F5.F7);
            System.Console.WriteLine(var19);
        }

        System.Console.WriteLine(var0);
        System.Console.WriteLine(var1);
        System.Console.WriteLine(1);
        System.Console.WriteLine(var6.F0.F0);
        System.Console.WriteLine(var6.F0.F1);
        System.Console.WriteLine(var6.F0.F2);
        System.Console.WriteLine(var6.F0.F3);
        System.Console.WriteLine(var6.F0.F4);
        System.Console.WriteLine(var6.F0.F5);
        System.Console.WriteLine(var6.F0.F6);
        System.Console.WriteLine(var6.F0.F7);
        System.Console.WriteLine(var6.F0.F8);
        System.Console.WriteLine(var6.F0.F9);
        System.Console.WriteLine(var6.F1);
        System.Console.WriteLine(var6.F2);
        System.Console.WriteLine(var6.F3);
        System.Console.WriteLine(var6.F4);
        System.Console.WriteLine(var6.F5);
        System.Console.WriteLine(var6.F6);
        System.Console.WriteLine(var6.F7);
        System.Console.WriteLine(var7.F0);
        System.Console.WriteLine(var8.F0);
        System.Console.WriteLine(var8.F1.F0);
        System.Console.WriteLine(var8.F1.F1.F0);
        System.Console.WriteLine(var8.F1.F1.F1);
        System.Console.WriteLine(var8.F1.F1.F2);
        System.Console.WriteLine(var8.F1.F1.F3);
        System.Console.WriteLine(var8.F1.F1.F4);
        System.Console.WriteLine(var8.F1.F1.F5);
        System.Console.WriteLine(var8.F1.F1.F6);
        System.Console.WriteLine(var8.F1.F1.F7);
        System.Console.WriteLine(var8.F1.F1.F8);
        System.Console.WriteLine(var8.F1.F1.F9);
        System.Console.WriteLine(var8.F1.F2);
        System.Console.WriteLine(var8.F1.F3);
        System.Console.WriteLine(var8.F1.F4);
        System.Console.WriteLine(var8.F2);
        System.Console.WriteLine(var8.F3);
        System.Console.WriteLine(var9[0][0]);
        System.Console.WriteLine(var8.F0);
        System.Console.WriteLine(var8.F1.F0);
        System.Console.WriteLine(var8.F1.F1.F0);
        System.Console.WriteLine(var8.F1.F1.F1);
        System.Console.WriteLine(var8.F1.F1.F2);
        System.Console.WriteLine(var8.F1.F1.F3);
        System.Console.WriteLine(var8.F1.F1.F4);
        System.Console.WriteLine(var8.F1.F1.F5);
        System.Console.WriteLine(var8.F1.F1.F6);
        System.Console.WriteLine(var8.F1.F1.F7);
        System.Console.WriteLine(var8.F1.F1.F8);
        System.Console.WriteLine(var8.F1.F1.F9);
        System.Console.WriteLine(var8.F1.F2);
        System.Console.WriteLine(var8.F1.F3);
        System.Console.WriteLine(var8.F1.F4);
        System.Console.WriteLine(var8.F2);
        System.Console.WriteLine(var8.F3);
        System.Console.WriteLine(var11[0].F0);
        System.Console.WriteLine(var11[0].F1.F0);
        System.Console.WriteLine(var11[0].F1.F1);
        System.Console.WriteLine(var11[0].F1.F2);
        System.Console.WriteLine(var11[0].F1.F3);
        System.Console.WriteLine(var11[0].F1.F4);
        System.Console.WriteLine(var11[0].F1.F5);
        System.Console.WriteLine(var11[0].F1.F6);
        System.Console.WriteLine(var11[0].F1.F7);
        System.Console.WriteLine(var11[0].F1.F8);
        System.Console.WriteLine(var11[0].F1.F9);
        System.Console.WriteLine(var11[0].F2);
        System.Console.WriteLine(var11[0].F3);
        System.Console.WriteLine(var11[0].F4);
    }

    public static C0 M2(C5[] arg0, I0 arg1)
    {
        return arg0[0].F0.F1;
    }

    public static ulong M15(S1 arg1, long arg2, ref C4[, ] arg3, ref S1 arg4, uint[] arg5)
    {
        return default(ulong);
    }

    public static long M10()
    {
        return default(long);
    }
}
