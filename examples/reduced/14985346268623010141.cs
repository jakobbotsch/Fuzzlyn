// Generated by Fuzzlyn v1.2 on 2021-08-13 12:57:02
// Run on .NET 6.0.0-dev on Arm64 Linux
// Seed: 14985346268623010141
// Reduced from 5.9 KiB to 0.3 KiB in 00:00:31
// Debug: Outputs 1
// Release: Outputs 0
public class Program
{
    public static void Main()
    {
        short vr7 = default(short);
        ulong vr3 = 1 + (5501276099597979163UL % (ulong)(1 | vr7));
        bool vr4 = M3(vr3);
    }

    static bool M3(ulong arg1)
    {
        System.Console.WriteLine(arg1);
        return true;
    }
}
