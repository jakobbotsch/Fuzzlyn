// Generated by Fuzzlyn v1.2 on 2021-08-06 06:14:43
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 18240974067614023462
// Reduced from 266.1 KiB to 0.3 KiB in 00:05:39
// Debug: Outputs 0
// Release: Outputs 1
class C0
{
    public uint F4;
    public C0(uint f4)
    {
        F4 = f4;
    }
}

public class Program
{
    public static void Main()
    {
        C0 vr2 = new C0(1);
        M23(vr2);
    }

    static void M23(C0 arg1)
    {
        arg1.F4 &= 0;
        System.Console.WriteLine(arg1.F4);
    }
}