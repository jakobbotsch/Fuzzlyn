// Generated by Fuzzlyn v1.2 on 2021-08-08 12:31:13
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 16542078182010762625
// Reduced from 175.2 KiB to 0.3 KiB in 00:04:08
// Debug: Outputs 1
// Release: Outputs 0
class C1
{
    public ulong F1;
    public short F5;
}

public class Program
{
    static C1 s_1 = new C1();
    public static void Main()
    {
        s_1.F1 = (ulong)((-6728994623740872510L % (s_1.F5 - 1)) + 1);
        System.Console.WriteLine(s_1.F1);
    }
}