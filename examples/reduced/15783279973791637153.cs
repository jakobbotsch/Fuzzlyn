// Generated by Fuzzlyn on 2018-06-27 02:38:59
// Seed: 15783279973791637153
// Reduced from 22.4 KiB to 0.3 KiB
// Debug: Throws 'System.OverflowException'
// Release: Runs successfully
public class Program
{
    static bool s_3;
    public static void Main()
    {
        M8(-2, '=', s_3);
    }

    static bool M8(int arg3, char arg6, bool arg8)
    {
        arg6 = (char)(0 & (-2147483648 % (arg3 | 1)));
        System.Console.WriteLine((int)arg6);
        return arg8;
    }
}
