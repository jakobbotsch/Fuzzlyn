// Generated by Fuzzlyn v1.1 on 2018-10-01 20:43:14
// Seed: 8415796585718811153
// Reduced from 113.3 KiB to 0.6 KiB in 00:02:15
// Debug: Outputs 0
// Release: Outputs 1
public class Program
{
    static bool s_8;
    static long s_9;
    static ulong s_20 = 1;
    static short s_21;
    public static void Main()
    {
        var vr13 = s_21++;
        M38();
    }

    static void M38()
    {
        ulong var6 = M41();
        sbyte var7 = (sbyte)((long)(var6 + (uint)(0 % ((0 & (M40() | s_21)) | 1))) / s_21);
        System.Console.WriteLine(var7);
    }

    static ushort M40()
    {
        var vr3 = s_21++;
        return 0;
    }

    static ref ulong M41()
    {
        long var9 = s_9;
        s_8 = true;
        return ref s_20;
    }
}
