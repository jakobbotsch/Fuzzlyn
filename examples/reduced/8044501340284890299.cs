// Generated by Fuzzlyn v1.1 on 2018-08-15 02:43:14
// Seed: 8044501340284890299
// Reduced from 18.1 KiB to 0.3 KiB
// Debug: Outputs 100
// Release: Outputs 2191141732
class C0
{
    public int F3;
    public C0(int f3)
    {
        F3 = f3;
    }
}

public class Program
{
    public static void Main()
    {
        C0 vr1 = new C0(-2103825564);
        vr1.F3 &= vr1.F3;
        var vr2 = (byte)vr1.F3;
        uint vr3 = vr2;
        System.Console.WriteLine(vr3);
    }
}
