// Generated by Fuzzlyn v1.1 on 2018-11-22 23:55:49
// Seed: 18371160421822365583
// Reduced from 372.1 KiB to 0.5 KiB in 00:11:18
// Debug: Outputs 0
// Release: Outputs 1
struct S1
{
    public sbyte F0;
    public ulong F3;
    public bool F7;
    public S1(sbyte f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S1 s_2;
    static S1 s_45 = new S1(1);
    static S1 s_71;
    public static void Main()
    {
        var vr11 = new S1(0);
        M91(s_2, M91(s_45, vr11));
    }

    static ref S1 M91(S1 arg0, S1 arg2)
    {
        System.Console.WriteLine(arg0.F0);
        return ref s_71;
    }
}
