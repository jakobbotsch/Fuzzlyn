// Generated by Fuzzlyn on 2018-07-20 06:26:53
// Seed: 6989889131740643060
// Reduced from 299.1 KiB to 0.8 KiB
// Debug: Prints 1 line(s)
// Release: Prints 0 line(s)
struct S0
{
    public sbyte F0;
    public sbyte F2;
    public ulong F3;
    public byte F5;
    public bool F6;
    public S0(bool f6): this()
    {
        F6 = f6;
    }
}

struct S1
{
    public byte F3;
    public S0 F6;
    public S1(S0 f6): this()
    {
        F6 = f6;
    }
}

struct S2
{
    public S1 F0;
    public S2(S1 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static S2 s_69;
    public static void Main()
    {
        var vr13 = s_69.F0;
        S2 vr24 = new S2(new S1(new S0(true)));
        bool vr25 = vr24.F0.F6.F6;
        if (vr25)
        {
            var vr26 = vr13.F3;
            System.Console.WriteLine(vr26);
        }
        else
        {
        }
    }
}
