// Generated by Fuzzlyn v1.2 on 2021-08-14 10:25:14
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 3631548312549275267
// Reduced from 298.0 KiB to 0.2 KiB in 00:15:22
// Debug: Outputs 65534
// Release: Outputs 65535
public class Program
{
    static ulong s_2;
    static ushort s_3;
    public static void Main()
    {
        ushort vr2 = s_3--;
        s_3 <<= 1 + (-621158189 % (int)(s_2 | 1));
        System.Console.WriteLine(s_3);
    }
}
