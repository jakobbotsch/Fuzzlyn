// Generated by Fuzzlyn v1.2 on 2021-08-04 20:35:03
// Seed: 4660250179414463698
// Reduced from 266.2 KiB to 0.3 KiB in 00:11:24
// Debug: Outputs 0
// Release: Outputs 4294967295
public class Program
{
    static uint[] s_39 = new uint[]{0};
    static uint[] s_88 = new uint[]{0};
    public static void Main()
    {
        s_88[0] %= (s_39[0]-- | 1);
        s_39[0] &= 0;
        uint vr6 = s_39[0];
        System.Console.WriteLine(vr6);
    }
}
