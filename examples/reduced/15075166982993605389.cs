// Generated by Fuzzlyn v1.1 on 2018-08-19 03:57:18
// Seed: 15075166982993605389
// Reduced from 427.3 KiB to 0.8 KiB
// Debug: Prints 1 line(s)
// Release: Prints 2 line(s)
struct S0
{
    public uint F0;
    public byte F2;
    public S0(byte f2): this()
    {
        F2 = f2;
    }
}

public class Program
{
    static short[] s_6 = new short[]{0};
    static S0 s_7;
    static bool s_28;
    static S0[] s_48 = new S0[]{new S0(0)};
    public static void Main()
    {
        M40(ref s_28, M37()) = s_48[0];
        byte vr28 = s_7.F2;
        bool vr29 = vr28 <= s_6[0];
        if (vr29)
        {
            ref short vr30 = ref s_6[0];
            System.Console.WriteLine(vr30);
        }
        else
        {
        }
    }

    static sbyte[] M37()
    {
        s_48 = new S0[]{new S0(1)};
        var vr15 = new sbyte[]{0};
        return vr15;
    }

    static ref S0 M40(ref bool arg0, sbyte[] arg1)
    {
        System.Console.WriteLine(arg0);
        return ref s_7;
    }
}