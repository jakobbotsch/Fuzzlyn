// Generated by Fuzzlyn v1.1 on 2018-12-04 20:40:48
// Seed: 11820741494813836003
// Reduced from 196.0 KiB to 0.6 KiB in 00:02:36
// Debug: Outputs 1
// Release: Outputs 0
struct S0
{
    public ushort F0;
    public long F4;
    public ulong F6;
    public S0(ushort f0): this()
    {
        F0 = f0;
    }
}

class C0
{
    public S0 F0;
}

public class Program
{
    static bool s_10;
    static C0 s_28 = new C0();
    static byte s_40;
    public static void Main()
    {
        var vr3 = new S0(1);
        S0 vr6 = default(S0);
        bool vr5 = M35(vr3, M48(vr6));
    }

    static bool M35(S0 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return s_10;
    }

    static ref S0 M48(S0 arg0)
    {
        byte var0 = s_40++;
        return ref s_28.F0;
    }
}