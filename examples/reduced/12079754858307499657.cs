// Generated by Fuzzlyn v1.2 on 2021-08-11 01:09:30
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 12079754858307499657
// Reduced from 115.1 KiB to 0.3 KiB in 00:02:31
// Debug: Outputs -1
// Release: Outputs 0
struct S0
{
    public byte F4;
    public S0(byte f4): this()
    {
        F4 = f4;
    }
}

public class Program
{
    static S0 s_39 = new S0(1);
    public static void Main()
    {
        var vr1 = (-646858131 % s_39.F4) - 1;
        System.Console.WriteLine(vr1);
    }
}
