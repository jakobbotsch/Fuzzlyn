// Generated by Fuzzlyn v1.2 on 2021-08-04 18:51:21
// Seed: 1685390688462258847
// Reduced from 126.7 KiB to 0.2 KiB in 00:02:49
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    static long s_3;
    public static void Main()
    {
        s_3 = ((1548298382 % (s_3 | 1)) + 1);
        System.Console.WriteLine(s_3);
    }
}