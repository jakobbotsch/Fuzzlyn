// Generated by Fuzzlyn v1.2 on 2021-08-05 10:57:31
// Seed: 16402716452338772759
// Reduced from 14.9 KiB to 0.2 KiB in 00:00:30
// Debug: Outputs -1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        var vr4 = new ulong[]{0};
        short vr7 = (short)((2073906450 % (vr4[0] | 1)) - 1);
        System.Console.WriteLine(vr7);
    }
}