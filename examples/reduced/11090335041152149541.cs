// Generated by Fuzzlyn on 2018-06-19 02:24:26
// Seed: 11090335041152149541
// Reduced from 450.1 KiB to 0.6 KiB
// Debug: Outputs 90
// Release: Outputs 0
struct S0
{
    public char F0;
    public S0(char f0): this()
    {
        F0 = f0;
    }
}

struct S1
{
    public S0 F0;
    public S0 F1;
    public S1(S0 f1): this()
    {
        F1 = f1;
    }
}

class C1
{
    public S1 F3;
    public C1(S1 f3)
    {
        F3 = f3;
    }
}

public class Program
{
    static C1 s_6 = new C1(new S1(new S0('Z')));
    public static void Main()
    {
        s_6.F3.F0 = M34();
        System.Console.WriteLine((int)s_6.F3.F1.F0);
    }

    static S0 M34()
    {
        return s_6.F3.F1;
    }
}
