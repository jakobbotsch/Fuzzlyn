// Generated by Fuzzlyn on 2018-06-26 17:27:27
// Seed: 8844979751382829800
// Reduced from 198.7 KiB to 0.2 KiB
// Debug: Outputs 65535
// Release: Outputs -1
public class Program
{
    static sbyte s_25 = -113;
    static char[, ] s_64 = new char[, ]{{'r'}};
    public static void Main()
    {
        char vr4 = (char)(s_25 | s_64[0, 0]);
        System.Console.WriteLine((int)vr4);
    }
}
