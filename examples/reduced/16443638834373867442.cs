// Generated by Fuzzlyn on 2018-06-28 00:00:18
// Seed: 16443638834373867442
// Reduced from 229.6 KiB to 0.4 KiB
// Debug: Outputs 0
// Release: Outputs 127
struct S0
{
    public byte F0;
    public bool F1;
    public sbyte F2;
    public sbyte F3;
    public bool F4;
}

struct S1
{
    public S0 F1;
    public sbyte F2;
}

public class Program
{
    static S1 s_30;
    public static void Main()
    {
        s_30.F1 = M12();
        System.Console.WriteLine(s_30.F2);
    }

    static S0 M12()
    {
        return new S0();
    }
}
