// Generated by Fuzzlyn on 2018-07-25 23:40:23
// Seed: 10869122147782230845
// Reduced from 145.5 KiB to 0.4 KiB
// Debug: Outputs 31309
// Release: Outputs 2143451725
struct S0
{
    public bool F0;
    public short F1;
    public sbyte F2;
    public int F3;
    public short F5;
    public S0(int f3): this()
    {
        F3 = f3;
    }
}

public class Program
{
    public static void Main()
    {
        S0 vr21;
        S0 vr23 = new S0(2143451725);
        ushort vr24 = (ushort)vr23.F3;
        vr21.F3 = vr24;
        System.Console.WriteLine(vr21.F3);
    }
}
