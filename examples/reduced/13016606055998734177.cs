// Generated by Fuzzlyn on 2018-06-23 12:55:13
// Seed: 13016606055998734177
// Reduced from 12.5 KiB to 0.2 KiB
// Debug: Outputs 65453
// Release: Outputs 4294967213
public class Program
{
    static sbyte s_1 = -83;
    static uint s_3;
    public static void Main()
    {
        byte vr0 = default(byte);
        s_3 = (ushort)(s_1 ^ vr0);
        System.Console.WriteLine(s_3);
    }
}
