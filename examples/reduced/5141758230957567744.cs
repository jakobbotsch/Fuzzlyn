// Generated by Fuzzlyn v1.1 on 2018-08-14 15:53:21
// Seed: 5141758230957567744
// Reduced from 8.3 KiB to 0.3 KiB
// Debug: Outputs -254
// Release: Outputs 2
public class Program
{
    public static void Main()
    {
        byte[,, ] vr14 = new byte[,, ]{{{255}}};
        uint vr15 = (uint)(1UL + (uint)(-1 * vr14[0, 0, 0]));
        short vr8 = (short)vr15;
        System.Console.WriteLine(vr8);
    }
}