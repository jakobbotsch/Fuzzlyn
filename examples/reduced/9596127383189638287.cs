// Generated by Fuzzlyn v1.1 on 2018-09-01 14:31:29
// Seed: 9596127383189638287
// Reduced from 9.9 KiB to 0.3 KiB
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public short F0;
}

public class Program
{
    static S0 s_1;
    public static void Main()
    {
        ushort[] vr1 = new ushort[]{65535};
        vr1[0] = (byte)(vr1[0] / 65535);
        s_1.F0 = (short)(0 % (vr1[0] | 1));
        System.Console.WriteLine(s_1.F0);
    }
}