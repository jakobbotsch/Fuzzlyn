// Generated by Fuzzlyn v1.1 on 2018-11-26 12:03:55
// Seed: 3136324617406220750
// Reduced from 439.7 KiB to 0.6 KiB in 00:09:23
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public long F0;
    public short F1;
    public long F2;
    public S0(long f0): this()
    {
        F0 = f0;
    }
}

struct S3
{
}

public class Program
{
    static S3 s_42;
    static S0[, ] s_65 = new S0[, ]{{new S0(0)}};
    public static void Main()
    {
        var vr6 = new S0(s_65[0, 0].F2);
        var vr7 = new S0(1);
        S3 vr10;
        S3 vr9 = M39(vr6, M39(vr7, vr10));
    }

    static ref S3 M39(S0 arg0, S3 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_42;
    }
}
