// Generated by Fuzzlyn v1.2 on 2021-08-05 22:39:57
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 4732196159424028943
// Reduced from 7.0 KiB to 0.2 KiB in 00:00:27
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static int s_2;
    public static void Main()
    {
        ulong vr0 = 1 + (11579860015122929513UL % (uint)(s_2 | 1));
        System.Console.WriteLine(vr0);
    }
}
