// Generated by Fuzzlyn v1.1 on 2018-09-02 18:44:17
// Seed: 2178238112698366247
// Reduced from 166.0 KiB to 0.8 KiB in 00:03:01
// Debug: Outputs 1
// Release: Outputs 0
class C0
{
}

struct S0
{
    public sbyte F0;
    public ushort F2;
    public int F5;
    public C0 F6;
    public short F7;
}

struct S1
{
    public int F0;
    public bool F1;
    public uint F2;
    public long F3;
    public S1(int f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S0[][] s_18 = new S0[][]{new S0[]{new S0()}};
    public static void Main()
    {
        var vr8 = new S1(0);
        var vr9 = new S1[][]{new S1[]{new S1(1)}};
        var vr13 = vr9[0][0];
        M29(vr13, M9(vr8));
    }

    static ref S0 M9(S1 arg1)
    {
        C0[] var1 = new C0[]{new C0()};
        return ref s_18[0][0];
    }

    static void M29(S1 arg0, S0 arg1)
    {
        System.Console.WriteLine(arg0.F0);
    }
}