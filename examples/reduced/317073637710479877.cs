// Generated by Fuzzlyn v1.2 on 2021-08-05 11:43:24
// Seed: 317073637710479877
// Reduced from 235.8 KiB to 0.3 KiB in 00:08:43
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    static sbyte[] s_6 = new sbyte[]{1};
    public static void Main()
    {
        ref sbyte vr0 = ref s_6[0];
        for (int vr1 = 0; vr1 < 2; vr1++)
        {
            vr0 = 0;
            vr0 = vr0;
        }

        System.Console.WriteLine(s_6[0]);
    }
}