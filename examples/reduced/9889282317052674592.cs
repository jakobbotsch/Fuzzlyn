// Generated by Fuzzlyn v1.2 on 2021-08-07 09:38:42
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9889282317052674592
// Reduced from 128.8 KiB to 0.2 KiB in 00:03:29
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        ulong[][] vr5 = new ulong[][]{new ulong[]{0}};
        short vr6 = (short)(1 + (15281526207195395160UL % (vr5[0][0] | 1)));
        System.Console.WriteLine(vr6);
    }
}