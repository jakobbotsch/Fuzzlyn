// Generated by Fuzzlyn v1.2 on 2021-08-13 17:04:06
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 13760376918325955224
// Reduced from 347.7 KiB to 0.3 KiB in 00:12:45
// Debug: Outputs 1
// Release: Outputs 0
class C0
{
    public uint F8;
}

public class Program
{
    static C0 s_75 = new C0();
    public static void Main()
    {
        ushort vr3 = (ushort)((11110298490187410843UL % (s_75.F8 | 1)) + 1);
        System.Console.WriteLine(vr3);
    }
}
