// Generated by Fuzzlyn v1.1 on 2018-09-14 07:57:52
// Seed: 8827856523860363721
// Reduced from 68.8 KiB to 0.6 KiB in 00:00:32
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public uint F0;
    public ulong F5;
    public byte F6;
    public S0(uint f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public short F3;
    public ulong F4;
    public ushort F6;
}

public class Program
{
    static S1 s_7;
    public static void Main()
    {
        var vr3 = new S0(0);
        S0 vr6 = new S0(1);
        var vr5 = new S1();
        M10(vr3, M10(vr6, vr5));
    }

    static ref S1 M10(S0 arg0, S1 arg1)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_7;
    }
}