// Generated by Fuzzlyn v1.4 on 2021-08-28 12:29:09
// Run on .NET 7.0.0-dev on Arm Linux
// Seed: 15889283875292071862
// Reduced from 78.4 KiB to 8.8 KiB in 02:54:04
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

public struct S0 : I0, I1, I2, I3
{
    public ulong F0;
    public ulong F1;
    public bool F2;
    public long F3;
    public uint F4;
    public S0(ulong f0, ulong f1, long f3, uint f4): this()
    {
    }

    public int M15()
    {
        return default(int);
    }
}

public class Program
{
    public static S0 s_3;
    public static S0 s_4;
    public static S0[, ] s_5 = new S0[, ]{{new S0(0, 0, 0, 0)}};
    public static I0 s_6;
    public static sbyte s_7;
    public static I1 s_8;
    public static byte[, ] s_9 = new byte[, ]{{1}};
    public static ushort[][] s_10;
    public static byte[] s_11 = new byte[]{0};
    public static ulong s_13;
    public static S0 s_14;
    public static byte[] s_15;
    public static I2 s_16;
    public static void Main()
    {
        M14();
    }

    public static I0 M14()
    {
        s_6 = new S0(0, s_3.F0, -7928442604973172007L, (uint)new S0(0, 6812687432325591708UL, 0, 0).M15());
        s_4.M15();
        s_8 = new S0(9538848029400295514UL, 7564843844071551006UL, -5428149909560405391L, 0);
        s_11[0] = s_9[0, 0];
        I3 var0 = new S0(s_4.F1--, 0, 0, s_3.F4--);
        var0 = new S0(17418481601746229097UL, 12512133667148973708UL, 2859325281914849042L, 0);
        s_11 = new byte[]{0, 0, 1, 1, 0, 1};
        {
            I3 var1;
            s_4.M15();
            if (s_4.F2)
            {
                s_13 = 0;
                s_5[0, 0].M15();
            }
            else
            {
                s_14 = new S0(0, 0, 0, 0);
                new S0(0, 8382270162341325426UL, 9223372036854775807L, 0).M15();
                new S0(0, 0, 0, 0).M15();
                bool var2 = false;
                try
                {
                    int[] var3 = new int[]{0, 0, 0, 0, 0, 1, 0, 0, 1, 1};
                    bool var4 = s_14.F2;
                    System.Console.WriteLine(var3[0]);
                    System.Console.WriteLine(var4);
                }
                finally
                {
                    var0 = new S0(11020118840339705505UL, 792863336585610162UL, 6683099622121810934L, 0);
                    System.Console.WriteLine(s_7);
                }

                new S0(13629699179791827010UL, 5442936696311289862UL, -6563777928104828983L, 0).M15();
                I2 var6 = new S0(0, 13328264191308455081UL, 4585112736274238832L, 0);
                System.Console.WriteLine(var2);
            }
        }

        {
            var0 = new S0(11549265282630416242UL, 0, 9223372036854775806L, 0);
            S0 var7 = s_3;
            uint[] var8 = new uint[]{1, 1, 0, 1};
            S0[][][] var9 = new S0[][][]{new S0[][]{new S0[]{new S0(0, 5374796497494308005UL, 6663001862592741363L, 0), new S0(0, 7680373789078340620UL, 9223372036854775807L, 0), new S0(0, 9442905824658491577UL, 9223372036854775807L, 0), new S0(0, 13127517202645338452UL, -1930939874973955771L, 0), new S0(12170910030636407409UL, 12710790495084943318UL, 9223372036854775807L, 0)}, new S0[]{new S0(0, 0, 0, 0)}, new S0[]{new S0(18422112907524981693UL, 0, 894847700834267508L, 0), new S0(0, 0, 0, 0), new S0(1749654629645891728UL, 0, 0, 0), new S0(5437056582042944177UL, 8712206898421432308UL, -7134872908576782690L, 0), new S0(15158202549603643256UL, 8575935512949885400UL, 0, 0)}, new S0[]{new S0(1963232191327269262UL, 0, 687382135526947908L, 0)}}, new S0[][]{new S0[]{new S0(3357756575054536497UL, 0, 3840959127891282234L, 0)}}};
            var9 = new S0[][][]{new S0[][]{new S0[]{new S0(16956715561374251091UL, 829545532667518883UL, 5053584415662934563L, 0), new S0(17399137823674302885UL, 0, -267925680541275303L, 0), new S0(2909315774735679552UL, 5940586454542265371UL, 0, 0), new S0(13367743140404943265UL, 0, 8346084553793747335L, 0), new S0(0, 0, 0, 0)}, new S0[]{new S0(0, 5654316055503214303UL, 0, 0)}}, new S0[][]{new S0[]{new S0(15355830300183396495UL, 12315523213072330192UL, 9223372036854775807L, 0), new S0(11571693803862979421UL, 13805445193589778557UL, 0, 0), new S0(0, 14377294207772225690UL, -8671650166703636812L, 0)}, new S0[]{new S0(17513345307194209626UL, 0, 9223372036854775806L, 0), new S0(11005728142163943614UL, 0, 0, 0), new S0(0, 0, 0, 0)}}, new S0[][]{new S0[]{new S0(0, 0, 9223372036854775806L, 0)}, new S0[]{new S0(0, 0, -9223372036854775808L, 0), new S0(724279048175297220UL, 10138798426405315247UL, 0, 0), new S0(14261856391679616335UL, 0, 0, 0), new S0(12401473700308735963UL, 7255719663427542047UL, 5010396340872198462L, 0), new S0(0, 0, -6385494288299951223L, 0), new S0(0, 0, -9223372036854775807L, 0), new S0(0, 0, 0, 0)}}, new S0[][]{new S0[]{new S0(6245940070824684536UL, 17487337534773871518UL, 7052601797448086429L, 0)}}};
            System.Console.WriteLine(var7.F0);
            System.Console.WriteLine(var7.F1);
            System.Console.WriteLine(var7.F2);
            System.Console.WriteLine(var7.F3);
            System.Console.WriteLine(var7.F4);
            System.Console.WriteLine(var8[0]);
            System.Console.WriteLine(var9[0][0][0].F0);
            System.Console.WriteLine(var9[0][0][0].F1);
            System.Console.WriteLine(var9[0][0][0].F2);
            System.Console.WriteLine(var9[0][0][0].F3);
            System.Console.WriteLine(var9[0][0][0].F4);
        }

        new S0(6276995497846191191UL, 0, 4833839016696894835L, 0).M15();
        var0 = new S0(0, 0, -9140283113668277769L, 0);
        byte var10 = s_9[0, 0];
        sbyte var11 = s_7;
        System.Console.WriteLine(var11);
        long var12 = 5801674631558509730L;
        System.Console.WriteLine(var10);
        System.Console.WriteLine(var12);
        I3 var13;
        var0 = new S0(0, 8694146964802240446UL, 0, 0);
        if ((1UL | (byte)s_5[0, 0].F3) >= 0)
        {
            new S0(0, 0, -6484935229006817580L, 0).M15();
            s_11 = new byte[]{1, 1, 1, 0};
        }
        else
        {
            new S0(0, 2201032677919936308UL, 5148658066479479569L, 0).M15();
        }

        if (0 >= s_9[0, 0])
        {
            int var14 = 0;
            s_13 = s_5[0, 0].F1--;
            System.Console.WriteLine(var14);
            s_10[0] = M16(ref s_16, (byte)(0 % s_5[0, 0].F0));
            if (s_14.F2)
            {
                return new S0(1654894312554006656UL, 3821492497870702941UL, 4508004255433647119L, 0);
            }
            else
            {
                s_10 = new ushort[][]{new ushort[]{0}, new ushort[]{1, 1, 1}, new ushort[]{0}, new ushort[]{1, 0, 1, 1}, new ushort[]{1, 1, 1, 0}, new ushort[]{0, 1, 1, 1}, new ushort[]{0}, new ushort[]{0}, new ushort[]{0}, new ushort[]{1, 0, 1, 1}};
                s_11[0] /= s_11[0]++;
                if (s_5[0, 0].F2)
                {
                    new S0(0, 56190313856932025UL, 0, 0).M15();
                }

                s_5[0, 0].F4 = 0;
                S0 var15 = new S0(1595955422612649403UL, 0, 1861962061724987786L, 0);
                System.Console.WriteLine(var15.F0);
                System.Console.WriteLine(var15.F1);
                System.Console.WriteLine(var15.F2);
                System.Console.WriteLine(var15.F3);
                System.Console.WriteLine(var15.F4);
            }

            s_14.F1 = s_13--;
            byte[] var16 = s_15;
            s_8 = new S0(6553831563823174981UL, 6678828042103759091UL, -9223372036854775808L, 0);
            var vr0 = s_9[0, 0];
            I2 vr1;
            ushort[] vr3;
            System.Console.WriteLine(var16[0]);
            new S0(s_14.F1++, 14253684715596741577UL, 1686783923399728859L, 0).M15();
        }

        if (s_3.F2)
        {
            s_8 = new S0(0, 2274693241367688902UL, -9223372036854775807L, 0);
        }

        s_8 = new S0(6102213536264673477UL, 0, 0, 0);
        byte vr5 = s_9[0, 0];
        ushort[] vr6;
        if (s_3.F2)
        {
            if (s_4.F2)
            {
                new S0(8945219192274712152UL, 0, -1289964484640808066L, 0).M15();
            }
            else
            {
                s_6 = new S0(0, 0, -6361349532255947517L, 0);
                s_5[0, 0].M15();
                long var17 = -6860165314769799803L;
                S0 var18 = new S0(4538087410031365220UL, 0, 0, 0);
                I0 var19;
                System.Console.WriteLine(var17);
                System.Console.WriteLine(var18.F0);
                System.Console.WriteLine(var18.F1);
                System.Console.WriteLine(var18.F2);
                System.Console.WriteLine(var18.F3);
                System.Console.WriteLine(var18.F4);
            }

            s_14.F1 = 0;
        }

        return s_5[0, 0];
    }

    public static ushort[] M16(ref I2 arg0, byte arg1)
    {
        return default(ushort[]);
    }
}
