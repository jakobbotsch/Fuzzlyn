// Generated by Fuzzlyn v1.1 on 2018-08-31 16:09:03
// Seed: 11017809633372167540
// Reduced from 1.0 KiB to 0.2 KiB
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
public class Program
{
    public static void Main()
    {
        ushort[][,, ] vr0 = new ushort[][,, ]{new ushort[,, ]{{{0}}}};
        short vr1 = (short)((0 & vr0[0][0, 0, 0]) / (vr0[0][0, 0, 0] | 1));
    }
}