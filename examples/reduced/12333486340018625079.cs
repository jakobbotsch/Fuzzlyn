// Generated by Fuzzlyn v1.1 on 2018-09-01 00:45:35
// Seed: 12333486340018625079
// Reduced from 62.7 KiB to 0.5 KiB
// Debug: Outputs 680228681
// Release: Outputs 0
struct S7
{
    public uint F0;
    public sbyte F6;
    public S7(uint f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S7[][, ] s_1 = new S7[][, ]{new S7[, ]{{new S7(0)}}};
    public static void Main()
    {
        M3() = s_1[0][0, 0];
        System.Console.WriteLine(s_1[0][0, 0].F0);
    }

    static ref S7 M3()
    {
        s_1[0] = new S7[, ]{{new S7(680228681U)}};
        return ref s_1[0][0, 0];
    }
}