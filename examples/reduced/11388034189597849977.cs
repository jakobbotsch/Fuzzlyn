// Generated by Fuzzlyn v1.2 on 2021-08-16 10:08:11
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 11388034189597849977
// Reduced from 52.1 KiB to 0.2 KiB in 00:01:43
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        byte[] vr0 = new byte[]{1};
        short vr1 = (short)(1 + (1606794491 % vr0[0]));
        System.Console.WriteLine(vr1);
    }
}
