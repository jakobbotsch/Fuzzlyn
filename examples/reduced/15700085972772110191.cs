// Generated by Fuzzlyn v1.1 on 2018-09-15 10:33:28
// Seed: 15700085972772110191
// Reduced from 260.7 KiB to 1.2 KiB in 00:06:55
// Debug: Outputs 1
// Release: Outputs -4
class C0
{
    public short F0;
    public byte F3;
    public ulong F4;
    public bool F6;
    public C0(ulong f4, bool f6)
    {
        F4 = f4;
        F6 = f6;
    }
}

public class Program
{
    static short s_6;
    static C0 s_18 = new C0(0, true);
    static short s_20;
    static C0 s_41 = new C0(0, true);
    static short s_80 = -13034;
    static C0 s_124 = new C0(0, false);
    public static void Main()
    {
        s_124.F0 = s_80;
        s_18.F0 = s_124.F0;
        s_6 = s_18.F0;
        sbyte vr1 = (sbyte)M92(s_6, s_20);
    }

    static short M92(short arg0, short arg1)
    {
        arg1 = arg0;
        short var1 = arg1;
        var1 /= (short)(var1 | 1);
        try
        {
            bool var2 = s_41.F6;
        }
        finally
        {
            C0 var3 = new C0(0, true);
            ulong var4 = var3.F4;
            C0 var5 = var3;
            System.Console.WriteLine(var3.F4);
            System.Console.WriteLine(var5.F0);
            System.Console.WriteLine(var5.F4);
        }

        System.Console.WriteLine(arg1);
        System.Console.WriteLine(var1);
        byte vr2 = s_41.F3;
        return vr2;
    }
}