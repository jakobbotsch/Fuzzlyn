// Generated by Fuzzlyn v1.1 on 2018-08-07 02:28:42
// Seed: 5619617346986407349
// Reduced from 32.3 KiB to 0.2 KiB
// Debug: Outputs 4294928244
// Release: Outputs 26484
public class Program
{
    public static void Main()
    {
        var vr21 = new ushort[, ]{{39052}};
        var vr24 = (long)(vr21[0, 0] * 4294967295U);
        System.Console.WriteLine(vr24);
    }
}