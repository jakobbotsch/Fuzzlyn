// Generated by Fuzzlyn v1.2 on 2021-08-17 10:18:58
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 9259135214527548221
// Reduced from 38.7 KiB to 0.2 KiB in 00:01:06
// Debug: Outputs -1
// Release: Outputs 0
public class Program
{
    static short s_3 = 1;
    public static void Main()
    {
        long vr6 = (3216981444U % s_3) - 1;
        short vr5 = (short)vr6;
        System.Console.WriteLine(vr5);
    }
}
