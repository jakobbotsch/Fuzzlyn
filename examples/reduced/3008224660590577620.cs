// Generated by Fuzzlyn v1.2 on 2021-08-04 19:11:23
// Seed: 3008224660590577620
// Reduced from 147.1 KiB to 0.3 KiB in 00:02:45
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        var vr1 = new short[]{0};
        for (int vr3 = 0; vr3 < 2; vr3++)
        {
            vr1[0] = 1;
            vr1[0] = vr1[0];
        }

        System.Console.WriteLine(vr1[0]);
    }
}