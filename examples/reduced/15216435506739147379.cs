// Generated by Fuzzlyn v1.1 on 2018-09-05 06:49:19
// Seed: 15216435506739147379
// Reduced from 99.7 KiB to 0.5 KiB in 00:01:59
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public short F0;
    public ulong F2;
    public ulong F3;
    public S0(short f0): this()
    {
        F0 = f0;
    }
}

struct S2
{
}

public class Program
{
    static S2 s_1;
    public static void Main()
    {
        S0 vr7 = default(S0);
        S0 vr11 = new S0(1);
        var vr9 = new S2();
        M14(vr11, M14(vr7, vr9));
    }

    static ref S2 M14(S0 arg0, S2 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_1;
    }
}
