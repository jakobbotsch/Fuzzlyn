// Generated by Fuzzlyn v1.2 on 2021-08-04 21:20:41
// Seed: 5423671877622016395
// Reduced from 56.8 KiB to 0.3 KiB in 00:01:32
// Debug: Outputs False
// Release: Outputs True
public class Program
{
    public static void Main()
    {
        var vr2 = new bool[][]{new bool[]{true}};
        var vr4 = vr2[0];
        for (int vr6 = 0; vr6 < 2; vr6++)
        {
            vr4[0] = false;
            bool vr7 = vr4[0];
            System.Console.WriteLine(vr7);
        }
    }
}
