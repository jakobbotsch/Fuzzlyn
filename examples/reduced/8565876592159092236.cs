// Generated by Fuzzlyn v1.2 on 2021-08-05 11:36:50
// Seed: 8565876592159092236
// Reduced from 76.9 KiB to 0.3 KiB in 00:02:45
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
    public uint F2;
    public C0(uint f2)
    {
        F2 = f2;
    }
}

public class Program
{
    static C0 s_7 = new C0(1);
    public static void Main()
    {
        s_7.F2 *= 0;
        uint vr1 = s_7.F2;
        ushort vr0 = (ushort)vr1;
        System.Console.WriteLine(vr0);
    }
}