// Generated by Fuzzlyn on 2018-06-19 04:11:29
// Seed: 2426599534545279952
// Reduced from 121.3 KiB to 0.3 KiB
// Debug: Prints 1 line(s)
// Release: Prints 0 line(s)
public class Program
{
    static short[] s_1;
    public static void Main()
    {
        s_1 = new short[]{-6890};
        bool vr67 = (char)(s_1[0] ^ 0U) > 0;
        if (vr67)
        {
            bool vr68 = default(bool);
            System.Console.WriteLine(vr68);
        }
        else
        {
        }
    }
}
