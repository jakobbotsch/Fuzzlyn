// Generated by Fuzzlyn v1.2 on 2021-08-08 12:45:56
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 8411197355305670643
// Reduced from 85.1 KiB to 0.2 KiB in 00:01:39
// Debug: Outputs 255
// Release: Outputs 0
public class Program
{
    static uint s_2 = 1;
    public static void Main()
    {
        byte vr2 = (byte)((-8959516268774278521L % s_2) - 1);
        System.Console.WriteLine(vr2);
    }
}
