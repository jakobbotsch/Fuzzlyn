// Generated by Fuzzlyn v1.1 on 2018-12-17 04:23:13
// Seed: 9156574721659778278
// Reduced from 75.1 KiB to 0.6 KiB in 00:01:12
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
}

struct S1
{
    public uint F0;
    public S1(uint f0): this()
    {
        F0 = f0;
    }
}

struct S2
{
    public bool F2;
    public C0 F6;
    public S1 F7;
    public S2(S1 f7): this()
    {
        F7 = f7;
    }
}

public class Program
{
    static S2 s_2 = new S2(new S1(1));
    public static void Main()
    {
        S2 vr7 = new S2(new S1(0));
        S2 vr9 = default(S2);
        M5(vr9, M5(s_2, vr7));
    }

    static ref S2 M5(S2 arg0, S2 arg1)
    {
        S1 var1 = arg0.F7;
        System.Console.WriteLine(var1.F0);
        return ref s_2;
    }
}