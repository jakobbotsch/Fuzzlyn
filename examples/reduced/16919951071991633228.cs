// Generated by Fuzzlyn v1.1 on 2018-09-06 09:49:34
// Seed: 16919951071991633228
// Reduced from 245.3 KiB to 0.6 KiB in 00:04:01
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
    public uint F0;
    public C0(uint f0)
    {
        F0 = f0;
    }
}

struct S0
{
    public C0 F0;
    public C0 F2;
    public int F7;
    public S0(C0 f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
}

public class Program
{
    static S1 s_12;
    public static void Main()
    {
        var vr6 = new S0(new C0(0));
        var vr7 = new S0(new C0(1));
        var vr8 = new S1();
        M11(vr6, M11(vr7, vr8));
    }

    static ref S1 M11(S0 arg0, S1 arg1)
    {
        System.Console.WriteLine(arg0.F0.F0);
        return ref s_12;
    }
}
