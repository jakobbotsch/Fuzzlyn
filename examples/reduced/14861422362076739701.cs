// Generated by Fuzzlyn v1.1 on 2018-11-13 13:11:15
// Seed: 14861422362076739701
// Reduced from 121.0 KiB to 0.5 KiB in 00:02:04
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
}

struct S0
{
    public C0 F0;
    public ushort F1;
    public ulong F3;
    public S0(ushort f1): this()
    {
        F1 = f1;
    }
}

public class Program
{
    static S0 s_9;
    public static void Main()
    {
        var vr5 = new S0(0);
        var vr6 = new S0(1);
        M22(vr5, M22(vr6, s_9));
    }

    static ref S0 M22(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F1);
        return ref s_9;
    }
}
