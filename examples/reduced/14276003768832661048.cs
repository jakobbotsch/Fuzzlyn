// Generated by Fuzzlyn on 2018-07-02 00:16:56
// Seed: 14276003768832661048
// Reduced from 12.0 KiB to 0.2 KiB
// Debug: Outputs 64265
// Release: Outputs -1271
public class Program
{
    public static void Main()
    {
        short[] vr4 = new short[]{-1272};
        ushort vr5 = (ushort)(1L ^ vr4[0]);
        int vr7 = vr5;
        System.Console.WriteLine(vr7);
    }
}
