// Generated by Fuzzlyn on 2018-07-18 02:04:17
// Seed: 11207695228190915331
// Reduced from 56.9 KiB to 0.3 KiB
// Debug: Outputs 65534
// Release: Outputs -2
struct S0
{
    public byte F1;
}

public class Program
{
    public static void Main()
    {
        var vr7 = new sbyte[][,, ]{new sbyte[,, ]{{{-2}}}};
        S0[] vr10 = new S0[]{new S0()};
        char vr11 = (char)(vr10[0].F1 | vr7[0][0, 0, 0]);
        System.Console.WriteLine((int)vr11);
    }
}
