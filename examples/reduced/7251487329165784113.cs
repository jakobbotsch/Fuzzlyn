// Generated by Fuzzlyn v1.1 on 2018-08-15 03:48:39
// Seed: 7251487329165784113
// Reduced from 66.0 KiB to 0.3 KiB
// Debug: Outputs 32766
// Release: Outputs -2
class C0
{
    public short F1;
    public C0(short f1)
    {
        F1 = f1;
    }
}

public class Program
{
    static short s_6;
    public static void Main()
    {
        C0 vr7 = new C0(32766);
        sbyte vr8 = (sbyte)(0 / (sbyte)vr7.F1);
        s_6 = vr7.F1;
        System.Console.WriteLine(s_6);
    }
}