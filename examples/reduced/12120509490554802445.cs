// Generated by Fuzzlyn v1.1 on 2018-08-28 05:33:31
// Seed: 12120509490554802445
// Reduced from 99.2 KiB to 0.9 KiB in 00:01:57
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public bool F0;
    public int F1;
    public uint F2;
    public int F3;
    public ushort F6;
}

class C2
{
    public ushort F0;
    public ushort F5;
    public C2(ushort f0)
    {
        F0 = f0;
    }
}

struct S2
{
    public long F0;
    public C2 F1;
    public byte F3;
    public S2(C2 f1): this()
    {
        F1 = f1;
    }
}

public class Program
{
    static S2[] s_9 = new S2[]{new S2(new C2(0))};
    static S0 s_13;
    public static void Main()
    {
        var vr3 = s_9[0];
        var vr4 = new S2(new C2(1));
        var vr5 = new S0();
        M10(vr3, M10(vr4, vr5));
    }

    static ref S0 M10(S2 arg0, S0 arg1)
    {
        arg1.F6 = arg0.F1.F5;
        short var1 = (short)arg1.F6;
        arg0.F1.F5 = arg0.F1.F0;
        System.Console.WriteLine(var1);
        return ref s_13;
    }
}