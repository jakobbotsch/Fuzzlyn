// Generated by Fuzzlyn v1.1 on 2018-08-15 15:11:39
// Seed: 11262275059310587756
// Reduced from 76.0 KiB to 0.3 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
struct S0
{
    public ushort F2;
    public ulong F8;
}

public class Program
{
    static S0[,, ] s_1 = new S0[,, ]{{{new S0()}}};
    static ushort s_9;
    public static void Main()
    {
        short vr18 = (short)(0 & s_1[0, 0, 0].F8);
        s_9 = s_1[0, 0, 0].F2;
        System.Console.WriteLine(vr18);
    }
}