// Generated by Fuzzlyn v1.2 on 2021-08-16 00:42:40
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16472625690252548984
// Reduced from 11.9 KiB to 0.3 KiB in 00:00:42
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static long s_1;
    public static void Main()
    {
        int vr3 = default(int);
        if (s_1 != 0)
        {
            var vr4 = vr3++;
        }

        var vr5 = 1 + (313715597 % (vr3 | 1));
        System.Console.WriteLine(vr5);
    }
}