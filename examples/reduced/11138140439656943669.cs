// Generated by Fuzzlyn on 2018-06-23 16:27:33
// Seed: 11138140439656943669
// Reduced from 28.8 KiB to 0.2 KiB
// Debug: Outputs 65534
// Release: Outputs 4294967294
public class Program
{
    static short s_1;
    public static void Main()
    {
        s_1 = -2;
        var vr33 = (char)(s_1 | 0U);
        ulong vr34 = vr33;
        System.Console.WriteLine(vr34);
    }
}
