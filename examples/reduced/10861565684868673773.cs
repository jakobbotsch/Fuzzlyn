// Generated by Fuzzlyn on 2018-07-08 17:23:11
// Seed: 10861565684868673773
// Reduced from 47.2 KiB to 0.4 KiB
// Debug: Outputs 65490
// Release: Outputs -46
class C0
{
    public sbyte F0;
    public C0(sbyte f0)
    {
        F0 = f0;
    }
}

public class Program
{
    static long[] s_3 = new long[]{0};
    static C0 s_7;
    public static void Main()
    {
        s_7 = new C0(-46);
        ushort vr26 = (ushort)(s_7.F0 ^ (char)(s_3[0] | 0));
        int vr23 = vr26;
        System.Console.WriteLine(vr23);
    }
}
