// Generated by Fuzzlyn v1.2 on 2021-08-14 06:23:32
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 14399691767471441077
// Reduced from 156.0 KiB to 0.4 KiB in 00:05:11
// Debug: Outputs 1
// Release: Outputs 0
class C0
{
    public uint F2;
}

class C1
{
    public C0 F4;
    public C1(C0 f4)
    {
        F4 = f4;
    }
}

public class Program
{
    static C1 s_7 = new C1(new C0());
    static ulong s_11;
    public static void Main()
    {
        s_11 = (ulong)(-5708267589046400072L % (s_7.F4.F2 | 1)) + 1;
        System.Console.WriteLine(s_11);
    }
}