// Generated by Fuzzlyn v1.2 on 2021-08-12 07:18:40
// Run on .NET 6.0.0-dev on Arm Linux
// Seed: 9882808384900634345
// Reduced from 192.8 KiB to 0.5 KiB in 00:06:36
// Debug: Outputs 0
// Release: Outputs -404382976
struct S0
{
    public uint F0;
    public sbyte F1;
    public int F2;
    public S0(uint f0, sbyte f1, int f2): this()
    {
    }
}

public class Program
{
    static ushort s_53;
    public static void Main()
    {
        M67(0, s_53, M29());
    }

    static S0 M29()
    {
        var vr0 = new int[, ]{{0}};
        return new S0(0, 0, 1);
    }

    static void M67(ulong arg0, ushort arg1, S0 arg2)
    {
        System.Console.WriteLine(arg2.F2);
    }
}
