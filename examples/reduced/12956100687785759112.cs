// Generated by Fuzzlyn on 2018-06-22 12:19:16
// Seed: 12956100687785759112
// Reduced from 111.7 KiB to 0.3 KiB
// Debug: Outputs 65480
// Release: Outputs -56
public class Program
{
    static sbyte s_1;
    static byte s_9;
    public static void Main()
    {
        s_1 += -56;
        char vr51 = (char)M20();
        System.Console.WriteLine((int)vr51);
    }

    static ushort M20()
    {
        return (ushort)(s_9 | s_1);
    }
}
