// Generated by Fuzzlyn v1.1 on 2018-09-20 02:04:28
// Seed: 15089251446983773610
// Reduced from 217.3 KiB to 0.7 KiB in 00:02:35
// Debug: Outputs 0
// Release: Outputs 140312589596840
class C0
{
}

struct S1
{
    public int F0;
    public C0 F1;
    public ushort F3;
    public int F4;
    public S1(C0 f1): this()
    {
        F1 = f1;
    }
}

struct S2
{
    public long F0;
    public long F1;
    public short F2;
}

public class Program
{
    static S2 s_4;
    public static void Main()
    {
        var vr2 = new S2();
        var vr3 = new S1(new C0());
        M48(vr2, M4(0, vr3));
    }

    static ref S2 M4(uint arg0, S1 arg1)
    {
        System.Console.WriteLine(arg0);
        return ref s_4;
    }

    static void M48(S2 arg0, S2 arg1)
    {
        System.Console.WriteLine(arg0.F0);
    }
}