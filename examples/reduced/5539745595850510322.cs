// Generated by Fuzzlyn v1.1 on 2018-09-19 04:40:35
// Seed: 5539745595850510322
// Reduced from 122.5 KiB to 1.2 KiB in 00:02:40
// Debug: Outputs 0
// Release: Outputs 2
struct S0
{
    public int F0;
    public S0(int f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    static bool[] s_26 = new bool[]{false};
    public static void Main()
    {
        var vr5 = new S0(1);
        var vr10 = (short)vr5.F0;
        M24(vr10, ref s_26);
    }

    static void M24(short arg0, ref bool[] arg1)
    {
        byte[] var13 = default(byte[]);
        bool var15 = default(bool);
        byte var16 = default(byte);
        if (0 != arg0--)
        {
            --arg0;
            arg0 /= 32766;
        }

        try
        {
            M26();
        }
        finally
        {
            if (s_26[0])
            {
                if (arg1[0])
                {
                    bool var14 = arg1[0];
                    System.Console.WriteLine(var13[0]);
                    System.Console.WriteLine(var14);
                }
                else
                {
                    System.Console.WriteLine(var15);
                }
            }

            System.Console.WriteLine(var16);
        }

        System.Console.WriteLine(arg0);
    }

    static ulong M26()
    {
        return 0;
    }
}