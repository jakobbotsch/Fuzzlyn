// Generated by Fuzzlyn v1.2 on 2021-08-04 20:29:23
// Seed: 17184170195959599793
// Reduced from 81.6 KiB to 0.3 KiB in 00:02:41
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    public static void Main()
    {
        ulong[] vr3 = new ulong[]{1};
        for (int vr4 = 0; vr4 < 2; vr4++)
        {
            vr3[0] = 0;
            ulong vr5 = vr3[0];
            System.Console.WriteLine(vr5);
        }
    }
}