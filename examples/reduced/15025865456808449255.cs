// Generated by Fuzzlyn v1.2 on 2021-08-12 22:48:03
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 15025865456808449255
// Reduced from 10.4 KiB to 0.2 KiB in 00:00:27
// Debug: Prints 1 line(s)
// Release: Prints 0 line(s)
public class Program
{
    static uint s_2;
    public static void Main()
    {
        if ((0 < (1 + (1726105602 % (s_2 | 1)))))
        {
            byte[] vr0 = new byte[]{0};
            System.Console.WriteLine(vr0[0]);
        }
    }
}