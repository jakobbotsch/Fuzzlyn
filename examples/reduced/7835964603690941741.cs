// Generated by Fuzzlyn v1.2 on 2021-08-14 17:04:20
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 7835964603690941741
// Reduced from 10.9 KiB to 0.2 KiB in 00:00:39
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static short[] s_5 = new short[]{1};
    public static void Main()
    {
        var vr2 = 1 + (-1588244268 % s_5[0]);
        System.Console.WriteLine(vr2);
    }
}
