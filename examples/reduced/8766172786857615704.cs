// Generated by Fuzzlyn v1.2 on 2021-08-16 19:01:43
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 8766172786857615704
// Reduced from 354.1 KiB to 0.2 KiB in 00:12:38
// Debug: Outputs 4294967295
// Release: Outputs 0
public class Program
{
    static byte[] s_49 = new byte[]{1};
    public static void Main()
    {
        long vr8 = (1143130283U % s_49[0]) - 1;
        System.Console.WriteLine(vr8);
    }
}
