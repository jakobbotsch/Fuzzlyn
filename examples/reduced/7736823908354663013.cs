// Generated by Fuzzlyn v1.1 on 2018-08-27 02:45:02
// Seed: 7736823908354663013
// Reduced from 28.7 KiB to 0.3 KiB
// Debug: Outputs 65534
// Release: Outputs 4294967294
public class Program
{
    static ulong s_3 = 18446744073709551614UL;
    static uint s_10;
    public static void Main()
    {
        ref uint vr1 = ref s_10;
        vr1 = M4();
        System.Console.WriteLine(s_10);
    }

    static ushort M4()
    {
        return (ushort)(sbyte)s_3;
    }
}
