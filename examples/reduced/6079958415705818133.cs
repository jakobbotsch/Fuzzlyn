// Generated by Fuzzlyn v1.2 on 2021-08-16 12:32:46
// Run on .NET 6.0.0-dev on X64 Windows
// Seed: 6079958415705818133
// Reduced from 16.3 KiB to 0.6 KiB in 00:00:23
// Debug: Runs successfully
// Release: Throws 'System.NullReferenceException'
class C0
{
    public short F5;
}

struct S1
{
    public ulong F1;
    public uint F3;
    public byte F4;
    public C0 F5;
    public S1(C0 f5): this()
    {
        F5 = f5;
    }
}

struct S2
{
    public short F5;
    public S1 F6;
    public S2(S1 f6): this()
    {
        F6 = f6;
    }
}

struct S3
{
    public S2 F0;
    public S3(S2 f0): this()
    {
        F0 = f0;
    }
}

public class Program
{
    public static void Main()
    {
        S3 vr0 = new S3(new S2(new S1(new C0())));
        ref short vr1 = ref vr0.F0.F6.F5.F5;
        System.Console.WriteLine(vr1);
    }
}