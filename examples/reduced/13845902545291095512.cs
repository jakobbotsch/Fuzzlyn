// Generated by Fuzzlyn on 2018-07-11 15:03:37
// Seed: 13845902545291095512
// Reduced from 16.7 KiB to 0.2 KiB
// Debug: Outputs 32769
// Release: Outputs -32767
public class Program
{
    static short s_1 = -32768;
    public static void Main()
    {
        char vr2 = (char)(1U ^ s_1);
        System.Console.WriteLine((int)vr2);
    }
}
