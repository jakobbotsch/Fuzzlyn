// Generated by Fuzzlyn v1.1 on 2018-11-20 04:43:53
// Seed: 15685013696755818937
// Reduced from 452.4 KiB to 0.7 KiB in 00:12:29
// Debug: Outputs 1
// Release: Outputs 0
class C1
{
    public int F2;
}

struct S0
{
}

class C2
{
    public C1 F5;
    public C2(C1 f5)
    {
        F5 = f5;
    }
}

struct S1
{
    public S0 F4;
    public int F7;
    public S1(int f7): this()
    {
        F7 = f7;
    }
}

public class Program
{
    static C2[] s_54 = new C2[]{new C2(new C1())};
    static S1 s_68;
    public static void Main()
    {
        var vr12 = new S1(1);
        var vr13 = new S1(s_54[0].F5.F2);
        var vr23 = new S1(0);
        M70(vr12, M70(vr13, vr23));
    }

    static ref S1 M70(S1 arg0, S1 arg1)
    {
        int var0 = arg0.F7;
        System.Console.WriteLine(var0);
        return ref s_68;
    }
}