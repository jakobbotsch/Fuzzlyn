// Generated by Fuzzlyn on 2018-07-11 21:18:50
// Seed: 15519275228784390555
// Reduced from 15.6 KiB to 0.3 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
public class Program
{
    static short[,, ] s_1 = new short[,, ]{{{0}}};
    public static void Main()
    {
        M2();
    }

    static uint M2()
    {
        uint var2 = (uint)((s_1[0, 0, 0] & 0) + s_1[0, 0, 0]);
        return var2;
    }
}
