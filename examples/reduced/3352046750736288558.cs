// Generated by Fuzzlyn on 2018-07-06 19:24:04
// Seed: 3352046750736288558
// Reduced from 2.2 KiB to 0.4 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
public class Program
{
    static long[, ] s_1 = new long[, ]{{0}};
    public static void Main()
    {
        M0();
    }

    static void M0()
    {
        var vr2 = s_1[0, 0];
        M1(vr2, (int)(0 & s_1[0, 0]));
    }

    static ulong[] M1(long arg0, int arg1)
    {
        return new ulong[]{0, 0, 0, 0, 18446744073709551615UL, 2UL, 0, 0, 0};
    }
}
