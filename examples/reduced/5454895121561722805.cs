// Generated by Fuzzlyn v1.1 on 2018-08-22 15:22:42
// Seed: 5454895121561722805
// Reduced from 38.5 KiB to 0.9 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
class C0
{
    public int F0;
    public sbyte F1;
    public uint F2;
    public long F5;
    public C0(sbyte f1)
    {
        F1 = f1;
    }
}

class C1
{
    public C0 F1;
    public bool F3;
    public C0 F7;
    public C1(bool f3, C0 f7)
    {
        F3 = f3;
        F7 = f7;
    }
}

class C2
{
    public ushort F0;
    public C1 F1;
}

public class Program
{
    static uint[] s_5 = new uint[]{0};
    public static void Main()
    {
        var vr8 = new C2();
        M1(vr8);
    }

    static uint M1(C2 arg1)
    {
        arg1.F1 = new C1(true, new C0(0));
        arg1.F0 = 0;
        ushort var2 = (ushort)(arg1.F1.F7.F0 | arg1.F1.F7.F1);
        ++arg1.F0;
        bool vr4 = arg1.F1.F3;
        if (vr4)
        {
            return s_5[0];
        }
        else
        {
        }

        System.Console.WriteLine(var2);
        return arg1.F1.F1.F2;
    }
}