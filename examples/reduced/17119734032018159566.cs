// Generated by Fuzzlyn v1.1 on 2018-08-13 12:02:49
// Seed: 17119734032018159566
// Reduced from 22.0 KiB to 0.4 KiB
// Debug: Outputs 32768
// Release: Outputs 0
public class Program
{
    static ulong s_1;
    static int[] s_2 = new int[]{-2};
    public static void Main()
    {
        ushort vr2 = (ushort)((-1 * (byte)M1()) & M2());
        System.Console.WriteLine(vr2);
    }

    static ref int M1()
    {
        s_1 = 0;
        return ref s_2[0];
    }

    static long M2()
    {
        return -32768;
    }
}