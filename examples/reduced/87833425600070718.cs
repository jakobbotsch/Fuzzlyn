// Generated by Fuzzlyn on 2018-07-02 20:01:16
// Seed: 87833425600070718
// Reduced from 5.4 KiB to 0.4 KiB
// Debug: Outputs 6
// Release: Outputs 0
public class Program
{
    static sbyte[, ] s_1 = new sbyte[, ]{{0}};
    static sbyte[] s_3 = new sbyte[]{-10};
    static ulong[] s_6 = new ulong[]{0};
    public static void Main()
    {
        s_1[0, 0] = s_3[0];
        short vr4 = (short)((ushort)(s_1[0, 0] | 'f') % s_1[0, 0]);
        s_6[0] = (ulong)vr4;
        System.Console.WriteLine(s_6[0]);
    }
}
