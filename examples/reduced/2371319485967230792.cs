// Generated by Fuzzlyn v1.1 on 2018-10-05 07:36:34
// Seed: 2371319485967230792
// Reduced from 93.9 KiB to 0.4 KiB in 00:01:00
// Debug: Outputs -1
// Release: Outputs 32767
public class Program
{
    static byte s_2;
    public static void Main()
    {
        M22(0, ref s_2);
    }

    static byte M22(short arg0, ref byte arg1)
    {
        try
        {
            return arg1;
        }
        finally
        {
            short var0 = arg0--;
            var0 = arg0;
            var0 >>= 1;
            System.Console.WriteLine(var0);
        }
    }
}