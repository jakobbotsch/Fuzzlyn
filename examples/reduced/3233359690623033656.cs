// Generated by Fuzzlyn v1.2 on 2021-08-08 22:26:43
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 3233359690623033656
// Reduced from 123.3 KiB to 0.2 KiB in 00:02:23
// Debug: Prints 0 line(s)
// Release: Prints 1 line(s)
public class Program
{
    static uint s_33 = 1;
    public static void Main()
    {
        if ((ushort)(-1 * (1 + (663919880 % s_33))) <= 1)
        {
            System.Console.WriteLine(0);
        }
    }
}