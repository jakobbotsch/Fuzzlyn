// Generated by Fuzzlyn v1.1 on 2018-09-15 23:11:53
// Seed: 7038235050314823943
// Reduced from 220.1 KiB to 0.6 KiB in 00:02:46
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public byte F0;
    public ulong F4;
    public ushort F5;
    public S0(byte f0): this()
    {
        F0 = f0;
    }
}

class C0
{
    public S0 F3;
    public C0(S0 f3)
    {
        F3 = f3;
    }
}

public class Program
{
    static C0 s_6 = new C0(new S0(0));
    static C0 s_19 = new C0(new S0(1));
    public static void Main()
    {
        var vr6 = s_6.F3;
        var vr7 = s_19.F3;
        var vr8 = new S0(0);
        var vr9 = M23(vr6, M23(vr7, vr8));
    }

    static ref S0 M23(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_6.F3;
    }
}
