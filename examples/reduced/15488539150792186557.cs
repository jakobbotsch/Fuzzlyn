// Generated by Fuzzlyn v1.1 on 2018-09-13 00:39:33
// Seed: 15488539150792186557
// Reduced from 232.9 KiB to 0.9 KiB in 00:03:15
// Debug: Outputs 0
// Release: Outputs 1
struct S0
{
    public long F0;
    public ushort F1;
    public long F5;
    public S0(long f0): this()
    {
        F0 = f0;
    }
}

struct S2
{
}

class C0
{
    public bool F1;
}

struct S3
{
    public C0 F1;
    public S2 F2;
    public S3(C0 f1): this()
    {
        F1 = f1;
    }
}

class C1
{
    public S0 F0;
}

public class Program
{
    static C1 s_7;
    static S3[] s_14 = new S3[]{new S3(new C0())};
    static S3 s_22 = new S3(new C0());
    public static void Main()
    {
        S0 vr4 = default(S0);
        var vr3 = new S0(1);
        M38(vr4, M35(vr3));
    }

    static ref S3 M35(S0 arg0)
    {
        if (s_22.F1.F1)
        {
            s_7.F0.F1 = arg0.F1;
        }

        return ref s_14[0];
    }

    static void M38(S0 arg0, S3 arg1)
    {
        System.Console.WriteLine(arg0.F0);
    }
}
