// Generated by Fuzzlyn v1.2 on 2021-08-07 07:06:42
// Run on .NET 6.0.0-dev on Arm Linux
// Seed: 4425066345618598239
// Reduced from 54.9 KiB to 0.5 KiB in 00:01:35
// Debug: Outputs 0
// Release: Outputs -228261888
struct S0
{
    public uint F0;
    public short F1;
    public int F2;
}

public class Program
{
    static byte s_3;
    static S0 s_7;
    static bool[, ] s_23 = new bool[, ]{{true}};
    public static void Main()
    {
        uint vr4 = default(uint);
        var vr7 = M13();
        M21(ref s_23[0, 0], vr4, s_3, vr7);
    }

    static S0 M13()
    {
        return s_7;
    }

    static void M21(ref bool arg0, uint arg1, byte arg2, S0 arg3)
    {
        System.Console.WriteLine(arg3.F2);
    }
}
