// Generated by Fuzzlyn v1.1 on 2018-08-20 10:38:57
// Seed: 12890997837479495448
// Reduced from 14.0 KiB to 0.3 KiB in 00:00:14
// Debug: Outputs 1
// Release: Outputs 32623
public class Program
{
    static uint s_1;
    public static void Main()
    {
        M4();
        System.Console.WriteLine(s_1);
    }

    static void M4()
    {
        s_1 = (ushort)((short)((0 % ((0 & (0 % (s_1 | (byte)(-1 | s_1)))) | 1)) | (s_1 + 1)) ^ s_1);
    }
}