// Generated by Fuzzlyn v1.2 on 2021-08-13 15:52:30
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 14435946247301982678
// Reduced from 5.8 KiB to 0.3 KiB in 00:00:25
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static int s_1;
    static ulong s_2;
    public static void Main()
    {
        var vr1 = (7287837092596292327L % (M4(s_1) | 1)) + 1;
        System.Console.WriteLine(vr1);
    }

    static int M4(int arg0)
    {
        s_2 = (ulong)(2314386879613219497L + arg0);
        return 0;
    }
}