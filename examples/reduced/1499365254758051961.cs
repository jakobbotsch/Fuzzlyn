// Generated by Fuzzlyn on 2018-07-24 01:40:45
// Seed: 1499365254758051961
// Reduced from 3.4 KiB to 0.3 KiB
// Debug: Outputs 65408
// Release: Outputs -128
public class Program
{
    static sbyte s_1 = 127;
    public static void Main()
    {
        char vr1 = (char)((0 & (0 / M2())) | s_1);
        System.Console.WriteLine((int)vr1);
    }

    static short M2()
    {
        int vr0 = s_1++;
        return (short)vr0;
    }
}
