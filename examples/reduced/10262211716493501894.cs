// Generated by Fuzzlyn v1.2 on 2021-08-06 00:15:43
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 10262211716493501894
// Reduced from 46.3 KiB to 0.2 KiB in 00:00:44
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    public static void Main()
    {
        byte vr0 = default(byte);
        uint[] vr1 = new uint[]{1};
        vr1[0] &= vr0;
        System.Console.WriteLine(vr1[0]);
    }
}
