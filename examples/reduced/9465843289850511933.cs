// Generated by Fuzzlyn on 2018-07-15 17:51:40
// Seed: 9465843289850511933
// Reduced from 54.1 KiB to 0.2 KiB
// Debug: Outputs 1791
// Release: Outputs 255
public class Program
{
    public static void Main()
    {
        var vr4 = new byte[]{255};
        ulong vr9 = (ushort)(1609 | vr4[0]);
        System.Console.WriteLine(vr9);
    }
}
