// Generated by Fuzzlyn v1.1 on 2018-08-23 06:00:45
// Seed: 15200404013590325207
// Reduced from 8.7 KiB to 0.2 KiB
// Debug: Outputs 4
// Release: Outputs -262140
public class Program
{
    public static void Main()
    {
        ushort[] vr17 = new ushort[]{65534};
        int vr18 = (byte)(vr17[0] * vr17[0]);
        System.Console.WriteLine(vr18);
    }
}