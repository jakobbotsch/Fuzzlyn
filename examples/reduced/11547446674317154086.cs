// Generated by Fuzzlyn v1.1 on 2018-11-10 02:28:50
// Seed: 11547446674317154086
// Reduced from 457.9 KiB to 0.7 KiB in 00:04:41
// Debug: Outputs 0
// Release: Outputs 4
class C0
{
    public sbyte F1;
    public ushort F4;
}

public class Program
{
    static C0 s_53;
    static C0 s_106 = new C0();
    static ushort[] s_112 = new ushort[]{0};
    public static void Main()
    {
        byte vr2 = 211;
        var vr1 = (sbyte)vr2;
        M107(vr1);
    }

    static void M107(sbyte arg0)
    {
        if (arg0 != s_112[0])
        {
            arg0 = s_106.F1;
            arg0 /= (-110 | 1);
        }
        else
        {
            try
            {
                System.Console.WriteLine(s_53.F4);
            }
            finally
            {
                arg0 %= 0;
            }
        }

        System.Console.WriteLine(arg0);
    }
}