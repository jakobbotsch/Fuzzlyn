// Generated by Fuzzlyn v1.2 on 2021-08-16 03:09:29
// Run on .NET 6.0.0-dev on Arm Linux
// Seed: 14940251284053972228
// Reduced from 333.7 KiB to 5.3 KiB in 00:25:43
// Debug: Outputs 1
// Release: Outputs 0
class C0
{
    public ushort F0;
    public sbyte F1;
    public sbyte F2;
}

struct S0
{
    public int F0;
    public sbyte F1;
    public int F2;
    public S0(int f2): this()
    {
        F2 = f2;
    }
}

class C1
{
    public byte F0;
    public uint F1;
    public C0 F2;
    public byte F3;
    public C1(C0 f2)
    {
        F2 = f2;
    }
}

public class Program
{
    static ulong[] s_3;
    static bool s_7;
    static short s_10;
    static short[, ] s_19;
    static ushort[][] s_22 = new ushort[][]{new ushort[]{0}};
    static S0 s_34;
    static S0[] s_57 = new S0[]{new S0(-1)};
    static int[][][] s_62;
    static C1 s_63;
    static short s_74;
    static bool s_99;
    static C1 s_104 = new C1(new C0());
    static C1[] s_127 = new C1[]{new C1(new C0())};
    static ushort[][] s_131 = new ushort[][]{new ushort[]{0}};
    static ulong s_132;
    static C1[, ] s_136 = new C1[, ]{{new C1(new C0())}};
    static S0[][] s_143 = new S0[][]{new S0[]{new S0(1)}};
    static C1 s_152 = new C1(new C0());
    static long[] s_159 = new long[]{0};
    public static void Main()
    {
        var vr12 = s_152.F2.F1;
        sbyte vr14 = default(sbyte);
        M4(ref s_127[0].F3, vr12, vr14, ref s_159[0], ref s_3);
    }

    static void M4(ref byte arg0, sbyte arg1, int arg2, ref long arg3, ref ulong[] arg4)
    {
        ushort var15 = default(ushort);
        C1 var18 = default(C1);
        C1 var19 = default(C1);
        uint var20 = default(uint);
        sbyte var23 = default(sbyte);
        byte var32 = default(byte);
        C1 var38 = default(C1);
        uint var40 = default(uint);
        ulong var42 = default(ulong);
        if (M22())
        {
            long var16 = arg3;
            System.Console.WriteLine(var16);
            s_3[0] = (ulong)M13();
            System.Console.WriteLine(var19.F0);
            System.Console.WriteLine(var19.F1);
            System.Console.WriteLine(var19.F2.F0);
            System.Console.WriteLine(var19.F2.F1);
            System.Console.WriteLine(var20);
            bool var21 = s_7;
            M18() = s_19[0, 0]--;
            System.Console.WriteLine(var21);
            System.Console.WriteLine(var18.F2.F2);
            var vr3 = arg2--;
            ref ushort var22 = ref var15;
            System.Console.WriteLine(var22);
            System.Console.WriteLine(var23);
        }

        C0 var31 = new C0();
        if (M22())
        {
            bool[] vr16 = default(bool[]);
            sbyte[] vr17 = default(sbyte[]);
            var31 = new C0();
            vr16[0] |= vr16[0];
            System.Console.WriteLine(vr17[0]);
            System.Console.WriteLine(var32);
        }
        else
        {
            C1 var35 = new C1(new C0());
            System.Console.WriteLine(var35.F2.F0);
            System.Console.WriteLine(var35.F2.F1);
            System.Console.WriteLine(var35.F2.F2);
        }

        var vr2 = s_104.F1--;
        var vr6 = new C0[]{new C0(), new C0(), new C0(), new C0(), new C0(), new C0(), new C0(), new C0(), new C0(), new C0()};
        S0[] var37 = s_57;
        System.Console.WriteLine(var37[0].F0);
        if (s_99)
        {
            System.Console.WriteLine(var38.F0);
            System.Console.WriteLine(var38.F1);
            System.Console.WriteLine(var38.F2.F0);
            System.Console.WriteLine(var38.F2.F1);
            System.Console.WriteLine(var38.F2.F2);
        }
        else
        {
            short[] var39 = new short[]{1};
            M34(arg0, ref var39[0]);
            System.Console.WriteLine(var40);
        }

        if (M106())
        {
            uint vr18 = default(uint);
            System.Console.WriteLine(vr18);
            long vr19 = default(long);
            var vr4 = (byte)vr19;
            M34(vr4, ref s_74) = new S0(0);
            s_62[0] = new int[][]{new int[]{0, 0, 1, 0, 0, 0}, new int[]{1}, new int[]{1}, new int[]{1}, new int[]{1}, new int[]{1}, new int[]{0, 0, 1}, new int[]{1}, new int[]{-1}};
            System.Console.WriteLine(var42);
            sbyte var43 = s_63.F2.F1;
            System.Console.WriteLine(var43);
            arg4[0] = s_132++;
        }
        else
        {
            var vr8 = var31.F2++;
            M32();
        }

        var vr9 = M35();
        M68(vr9, M25(), ref s_131[0], var31.F0) = M68(M35(), M115(ref s_136[0, 0]), ref s_22[0], var15--);
    }

    static int M13()
    {
        return default(int);
    }

    static ref short M18()
    {
        return ref s_10;
    }

    static bool M22()
    {
        return default(bool);
    }

    static S0 M25()
    {
        return default(S0);
    }

    static S0 M32()
    {
        return default(S0);
    }

    static ref S0 M34(byte arg0, ref short arg1)
    {
        return ref s_34;
    }

    static S0 M35()
    {
        return default(S0);
    }

    static ref S0[] M68(S0 arg0, S0 arg1, ref ushort[] arg2, ushort arg3)
    {
        System.Console.WriteLine(arg1.F2);
        return ref s_57;
    }

    static bool M106()
    {
        return default(bool);
    }

    static S0 M115(ref C1 arg0)
    {
        arg0 = arg0;
        return s_143[0][0];
    }
}
