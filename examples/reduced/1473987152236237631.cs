// Generated by Fuzzlyn v1.2 on 2021-08-05 20:56:25
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 1473987152236237631
// Reduced from 296.1 KiB to 0.8 KiB in 00:13:20
// Debug: Throws 'System.OverflowException'
// Release: Throws 'System.OverflowException'
struct S0
{
    public int F2;
    public S0(int f2): this()
    {
        F2 = f2;
    }
}

struct S1
{
    public long F0;
    public S0 F5;
    public S1(long f0, S0 f5): this()
    {
        F0 = f0;
        F5 = f5;
    }
}

public class Program
{
    static long s_12;
    static S1 s_14 = new S1(0, new S0(1));
    public static void Main()
    {
        var vr12 = new S1[]{new S1(9223372036854775807L, new S0(0))};
        var vr14 = vr12[0].F0;
        for (int vr16 = 0; vr16 < 2; vr16++)
        {
            var vr21 = s_12 % -1;
            var vr18 = vr14++;
            for (int vr27 = 0; vr27 < 2; vr27++)
            {
                var vr28 = s_14.F5.F2;
                System.Console.WriteLine(vr28);
                s_14.F5.F2 &= 0;
            }

            s_12 = vr14;
        }
    }
}