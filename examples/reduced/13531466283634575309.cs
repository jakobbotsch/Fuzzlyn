// Generated by Fuzzlyn v1.2 on 2021-08-14 03:15:47
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 13531466283634575309
// Reduced from 101.0 KiB to 0.3 KiB in 00:02:41
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static ushort s_5;
    static short s_9;
    public static void Main()
    {
        for (int vr1 = 0; vr1 < 2; vr1++)
        {
            System.Console.WriteLine(s_9);
            s_9 = (short)(1 + (1 % ((s_5 & 0) | 1)));
        }
    }
}