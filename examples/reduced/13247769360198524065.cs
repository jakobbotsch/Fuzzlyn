// Generated by Fuzzlyn v1.1 on 2018-08-31 20:29:05
// Seed: 13247769360198524065
// Reduced from 39.1 KiB to 0.2 KiB
// Debug: Outputs -18432
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        short[] vr15 = new short[]{18580};
        vr15[0] = (short)((byte)vr15[0] - vr15[0]);
        System.Console.WriteLine(vr15[0]);
    }
}