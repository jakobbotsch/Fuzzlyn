// Generated by Fuzzlyn on 2018-06-28 02:58:06
// Seed: 14980028664254605908
// Reduced from 212.8 KiB to 0.3 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
public class Program
{
    static long[][, ] s_1 = new long[][, ]{new long[, ]{{0}}};
    static char[] s_60 = new char[]{'a'};
    public static void Main()
    {
        short vr28 = default(short);
        sbyte vr29 = (sbyte)(0 % (4294967294U + (vr28 * s_1[0][0, 0])));
        char vr30 = s_60[0];
    }
}
