// Generated by Fuzzlyn v1.2 on 2021-08-17 12:34:51
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 8917851824322723842
// Reduced from 140.3 KiB to 0.2 KiB in 00:02:58
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static short s_10;
    public static void Main()
    {
        long vr3 = 1 + (long)(12522826740649602702UL % ((uint)s_10 | 1));
        System.Console.WriteLine(vr3);
    }
}