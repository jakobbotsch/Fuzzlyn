// Generated by Fuzzlyn v1.4 on 2021-08-24 20:42:44
// Run on .NET 7.0.0-dev on Arm Linux
// Seed: 2425790387740910338
// Reduced from 117.3 KiB to 7.5 KiB in 03:15:54
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

public struct S0 : I0, I1, I2
{
    public uint F0;
    public short F1;
    public byte F2;
    public short F3;
    public long F4;
    public ulong F5;
    public long F6;
    public int F7;
    public S0(short f3, long f4, ulong f5, long f6, int f7, short f8, short f9): this()
    {
    }

    public uint M1(I0 arg0)
    {
        return default(uint);
    }

    public I2 M2(long arg0, sbyte arg1, I0 arg2, I0 arg3, bool arg4)
    {
        return default(I2);
    }

    public I0 M6(sbyte arg0)
    {
        return default(I0);
    }

    public bool M10(short arg0, ulong arg1, I1 arg2)
    {
        return default(bool);
    }

    public byte M13(ushort arg0, I2 arg1)
    {
        return default(byte);
    }
}

public class Program
{
    public static I0 s_1;
    public static ushort s_2;
    public static short s_3;
    public static S0[] s_9;
    public static sbyte s_19;
    public static S0 s_25;
    public static void Main()
    {
        M0();
    }

    public static void M0()
    {
        I2 var0 = new S0(0, -9223372036854775808L, 14312857717048609427UL, 0, 0, 0, 0);
        var vr13 = new S0(1, -6391590654836547132L, 4666612278786757708UL, 0, 0, -1, 1);
        var vr19 = new S0(0, 6898553285467262561L, 131734785590485623UL, 5288898601207750804L, 0, 0, 0);
        var vr20 = new S0(0, -8095114998905063012L, 0, 2022949083660874143L, 0, 0, 1);
        new S0(0, 0, 6133602473905991779UL, 1902351963994018489L, (byte)new S0(0, 9223372036854775806L, 0, 9223372036854775806L, 0, 0, 0).M1(vr20), (short)new S0(0, -9223372036854775808L, 9524427172881322135UL, 0, 0, 0, 0).M1(vr19), 0).M1(vr13);
        if ((-1 <= new S0(0, -2349273561649482569L, 17800534408846071305UL, 0, 0, -1, 0).M1(s_1)))
        {
            var vr10 = new S0(0, 0, 887500331074544200UL, 4556558033004394964L, 0, 0, 0);
            if (new S0(-1, -4551155033043337765L, 7383972011704512393UL, 6633328274638534404L, 0, 0, 0).M1(vr10) != 1)
            {
                try
                {
                }
                finally
                {
                    var vr1 = new S0(1, 0, 0, -4480222462938103866L, 0, 0, 1);
                    long var2 = new S0(0, 0, 9540478613041038774UL, 2252513706404175225L, 0, 0, 0).M1(vr1);
                    if (0 > new S0(0, 0, 0, 6140956955530468357L, 0, 0, 0).M1(s_1))
                    {
                        var2 = var2--;
                    }
                    else
                    {
                        s_3 *= s_3++;
                        s_1 = new S0(0, 5176087506096952659L, 11461617577481721555UL, 0, 0, 0, 0);
                    }

                    ulong var3 = 0 ^ ((byte)((s_2 ^ 0) % 4294967295U) / (ulong)((s_3 + 1U) | 1));
                    var vr8 = new S0(-1, 9223372036854775806L, 0, -9223372036854775807L, 0, 0, 0);
                    var vr11 = new S0(0, 0, 14985727016811092893UL, 9223372036854775806L, 0, 0, 0);
                    new S0(0, -2341469296880124460L, 14563461198387310051UL, 2325428301455646067L, 0, 1, 0).M2(var2, 0, vr8, vr11, false);
                    System.Console.WriteLine(var2);
                    System.Console.WriteLine(var3);
                }

                var vr0 = new S0(1, 2482770994021402396L, 0, 0, 0, 1, 0);
                new S0(0, 0, 0, 0, 0, 0, 0).M1(vr0);
            }
            else
            {
                if (false)
                {
                    I2 var1;
                }
                else
                {
                    byte var6 = (byte)new S0(0, -9223372036854775808L, 14236535231537759337UL, 0, 0, 0, 0).M1(s_1);
                    System.Console.WriteLine(var6);
                }

                var vr4 = new S0(0, 0, 14894651909452435012UL, -1528350077544881297L, 1, 0, 0);
                long var7 = new S0(0, 1317919240104586897L, 0, 0, 0, 1, 0).M1(vr4);
                var vr27 = new S0(1, 0, 17582398497803071544UL, 1521077293939017647L, 0, 0, 0);
                new S0(0, -1466142002240353608L, 0, 0, 0, 1, 0).M2(-704074944992737165L, 0, s_1, vr27, true);
                System.Console.WriteLine(var7);
                var vr29 = new S0(0, 0, 0, 5749910552054149840L, 0, 0, 0);
                var vr30 = new S0(0, 0, 0, 9217312872395738230L, 0, 0, 0);
                var vr31 = new S0(0, 0, 0, 0, 0, 1, 0);
                new S0(0, 0, 0, -9223372036854775808L, 0, 0, 0).M2(1227915332452455356L, (sbyte)(new S0(0, 0, 12023082600337108054UL, 0, 2147483647, -17074, 0).M1(vr30) % (byte)(16002601795139282582UL % (new S0(-32768, -9223372036854775808L, 0, -4382600539410913146L, -934646950, 0, 0).M1(vr31) | new S0(0, 0, 6505178839094446722UL, -2778711700779156298L, 0, 0, 0).M1(new S0(0, 0, 6763925149678574796UL, 0, 0, 0, 0))))), vr29, new S0(0, 0, 0, 0, 0, 1, 0), true);
            }

            var vr12 = new S0(1, 0, 0, 0, 0, 0, 0);
            var vr15 = new S0(0, 0, 4758824757678515579UL, 0, 1, 1, 0);
            var vr28 = (sbyte)new S0(0, 0, 0, 0, 0, 0, 0).M1(s_1);
            new S0(0, 0, 0, 9223372036854775807L, 0, 0, 0).M2(0, vr28, vr12, vr15, false);
            ulong[][][] var8 = new ulong[][][]{new ulong[][]{new ulong[]{1}, new ulong[]{0, 1, 1}, new ulong[]{3781026370680827921UL}}, new ulong[][]{new ulong[]{0}}, new ulong[][]{new ulong[]{0}}, new ulong[][]{new ulong[]{1}, new ulong[]{0}, new ulong[]{0}, new ulong[]{0}, new ulong[]{0}, new ulong[]{0}}, new ulong[][]{new ulong[]{1}, new ulong[]{0}, new ulong[]{0}, new ulong[]{1}, new ulong[]{0}, new ulong[]{0}, new ulong[]{0}}, new ulong[][]{new ulong[]{0}, new ulong[]{1}, new ulong[]{0}, new ulong[]{1, 1}, new ulong[]{1}, new ulong[]{1}, new ulong[]{1}}, new ulong[][]{new ulong[]{0, 0, 1}, new ulong[]{0, 1}, new ulong[]{1}, new ulong[]{0}, new ulong[]{1}}, new ulong[][]{new ulong[]{0}, new ulong[]{0, 1, 1}, new ulong[]{0}, new ulong[]{1}, new ulong[]{0}, new ulong[]{1, 1, 1}}};
            System.Console.WriteLine(var8[0][0][0]);
        }
        else
        {
            if (s_2 < 0)
            {
                sbyte vr21 = default(sbyte);
                sbyte var9 = vr21;
                sbyte vr22 = default(sbyte);
                var vr2 = vr22;
                var vr5 = new S0(0, 0, 0, 0, 0, 0, 1);
                M5().M2(0, vr2, s_1, s_1, 1 == (long)M3());
                I2 var10;
                M3();
                var vr9 = new S0(0, -3518860063830351318L, 0, 0, 0, 0, 0);
                var vr16 = s_25.F3;
                var vr3 = new S0(0, 0, 0, 0, 0, 0, 0).M10(vr16, 0, vr9);
                var vr6 = s_9[0].M6(var9);
                sbyte vr23 = default(sbyte);
                var vr18 = vr23;
                new S0(1, -9223372036854775807L, 0, 0, 0, 0, 0).M2(vr18, var9, vr6, s_9[0], vr3);
                System.Console.WriteLine(var9);
            }

            var vr7 = new S0(0, 0, 0, 0, 0, 0, 0);
            S0 vr25;
            var vr14 = new byte[]{1, 1, 1, 0, 0, 0, 1};
            ushort vr24 = s_2;
            sbyte var11 = (sbyte)vr24;
            new S0(0, 0, 0, 2939112257920804166L, 0, 0, 0).M13(0, M5());
            System.Console.WriteLine(var11);
            var vr17 = new S0(0, 0, 0, 0, 0, 0, 0);
            S0 vr26;
            System.Console.WriteLine(s_19);
            M14(0, new ushort[]{0});
        }
    }

    public static sbyte M3()
    {
        return default(sbyte);
    }

    public static void M14(long arg0, ushort[] arg1)
    {
    }

    public static S0 M5()
    {
        return default(S0);
    }
}