// Generated by Fuzzlyn v1.2 on 2021-08-04 21:44:07
// Seed: 3460520141228393315
// Reduced from 247.8 KiB to 0.3 KiB in 00:09:48
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    static ushort[] s_41 = new ushort[]{1};
    public static void Main()
    {
        ushort[] vr2 = s_41;
        for (int vr3 = 0; vr3 < 2; vr3++)
        {
            vr2[0] = 0;
            ushort vr4 = vr2[0];
            System.Console.WriteLine(vr4);
        }
    }
}
