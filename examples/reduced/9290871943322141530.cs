// Generated by Fuzzlyn v1.2 on 2021-08-05 20:29:49
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9290871943322141530
// Reduced from 7.2 KiB to 0.3 KiB in 00:00:21
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        int[,, ] vr0 = new int[,, ]{{{0}}};
        uint vr1 = 1 + (uint)(-1646913106 % M1(ref vr0[0, 0, 0]));
        System.Console.WriteLine(vr1);
    }

    static int M1(ref int arg0)
    {
        System.Console.WriteLine(arg0);
        return 1;
    }
}
