// Generated by Fuzzlyn v1.2 on 2021-08-05 17:30:22
// Run on Arm64 Linux, .NET 6.0.0-preview.6.21352.12
// Seed: 17640904714299455878
// Reduced from 407.7 KiB to 0.3 KiB in 00:18:50
// Debug: Outputs 0
// Release: Outputs 1
struct S1
{
    public int F8;
    public S1(int f8): this()
    {
        F8 = f8;
    }
}

public class Program
{
    static S1[] s_1 = new S1[]{new S1(1)};
    public static void Main()
    {
        int vr6 = default(int);
        s_1[0].F8 *= vr6;
        System.Console.WriteLine(s_1[0].F8);
    }
}