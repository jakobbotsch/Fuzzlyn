// Generated by Fuzzlyn v1.1 on 2018-09-14 03:51:33
// Seed: 1272107748815178644
// Reduced from 141.5 KiB to 0.8 KiB in 00:03:31
// Debug: Outputs -1
// Release: Outputs 1685
class C0
{
    public bool F0;
    public ushort F2;
    public ulong F3;
    public int F4;
    public ulong F5;
}

public class Program
{
    static C0 s_5 = new C0();
    static C0 s_20 = new C0();
    static short[] s_34 = new short[]{-1};
    public static void Main()
    {
        M38(0);
    }

    static void M38(short arg0)
    {
        arg0 = s_34[0];
        try
        {
            ulong vr8 = s_20.F3;
        }
        finally
        {
            System.Console.WriteLine(s_5.F0);
            System.Console.WriteLine(s_5.F2);
            System.Console.WriteLine(s_5.F3);
            System.Console.WriteLine(s_5.F4);
            System.Console.WriteLine(s_5.F5);
        }

        arg0 |= arg0;
        arg0 %= -2554;
        System.Console.WriteLine(arg0);
    }
}