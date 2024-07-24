using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics;

namespace Fuzzlyn.ExecutionServer;

public enum Extension
{
    AllSupported,
    VectorT,
    Vector64,
    Vector128,
    Vector256,
    Vector512,
    ArmAdvSimd,
    ArmAdvSimdArm64,
    ArmAes,
    ArmAesArm64,
    ArmArmBase,
    ArmArmBaseArm64,
    ArmCrc32,
    ArmCrc32Arm64,
    ArmDp,
    ArmDpArm64,
    ArmRdm,
    ArmRdmArm64,
    ArmSha1,
    ArmSha1Arm64,
    ArmSha256,
    ArmSha256Arm64,
    ArmSve,
    ArmSveArm64,
    X86X86Base,
    X86X86BaseX64,
    X86Aes,
    X86AesX64,
    X86Avx,
    X86AvxX64,
    X86Avx2,
    X86Avx2X64,
    X86Avx10v1,
    X86Avx10v1X64,
    X86Avx10v1V512,
    X86Avx512BW,
    X86Avx512BWVL,
    X86Avx512BWX64,
    X86Avx512CD,
    X86Avx512CDVL,
    X86Avx512CDX64,
    X86Avx512DQ,
    X86Avx512DQVL,
    X86Avx512DQX64,
    X86Avx512F,
    X86Avx512FVL,
    X86Avx512FX64,
    X86Avx512Vbmi,
    X86Avx512VbmiVL,
    X86Avx512VbmiX64,
    X86AvxVnni,
    X86AvxVnniX64,
    X86Bmi1,
    X86Bmi1X64,
    X86Bmi2,
    X86Bmi2X64,
    X86Fma,
    X86FmaX64,
    X86Lzcnt,
    X86LzcntX64,
    X86Pclmulqdq,
    X86PclmulqdqX64,
    X86Popcnt,
    X86PopcntX64,
    X86Sse,
    X86SseX64,
    X86Sse2,
    X86Sse2X64,
    X86Sse3,
    X86Sse3X64,
    X86Sse41,
    X86Sse41X64,
    X86Sse42,
    X86Sse42X64,
    X86Ssse3,
    X86Ssse3X64,
    X86X86Serialize,
    X86X86SerializeX64,
}

public static class ExtensionHelpers
{
    public static Extension[] GetSupportedExtensions()
    {
        List<Extension> extensions = [];
        Assembly spc = typeof(System.Runtime.Intrinsics.X86.Sse).Assembly;
        if (Vector.IsHardwareAccelerated) extensions.Add(Extension.VectorT);
        if (Vector64.IsHardwareAccelerated) extensions.Add(Extension.Vector64);
        if (Vector128.IsHardwareAccelerated) extensions.Add(Extension.Vector128);
        if (Vector256.IsHardwareAccelerated) extensions.Add(Extension.Vector256);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Vector512")?.GetProperty("IsHardwareAccelerated", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.Vector512);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.AdvSimd")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmAdvSimd);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.AdvSimd+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmAdvSimdArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Aes")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmAes);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Aes+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmAesArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.ArmBase")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmArmBase);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.ArmBase+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmArmBaseArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Crc32")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmCrc32);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Crc32+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmCrc32Arm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Dp")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmDp);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Dp+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmDpArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Rdm")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmRdm);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Rdm+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmRdmArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sha1")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSha1);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sha1+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSha1Arm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sha256")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSha256);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sha256+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSha256Arm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sve")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSve);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.Arm.Sve+Arm64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.ArmSveArm64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.X86Base")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86X86Base);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.X86Base+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86X86BaseX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Aes")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Aes);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Aes+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86AesX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86AvxX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx2")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx2);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx2+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx2X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx10v1")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx10v1);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx10v1+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx10v1X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx10v1+V512")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx10v1V512);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512BW")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512BW);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512BW+VL")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512BWVL);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512BW+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512BWX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512CD")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512CD);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512CD+VL")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512CDVL);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512CD+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512CDX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512DQ")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512DQ);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512DQ+VL")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512DQVL);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512DQ+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512DQX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512F")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512F);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512F+VL")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512FVL);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512F+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512FX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512Vbmi")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512Vbmi);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512Vbmi+VL")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512VbmiVL);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Avx512Vbmi+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Avx512VbmiX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.AvxVnni")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86AvxVnni);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.AvxVnni+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86AvxVnniX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Bmi1")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Bmi1);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Bmi1+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Bmi1X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Bmi2")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Bmi2);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Bmi2+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Bmi2X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Fma")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Fma);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Fma+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86FmaX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Lzcnt")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Lzcnt);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Lzcnt+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86LzcntX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Pclmulqdq")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Pclmulqdq);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Pclmulqdq+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86PclmulqdqX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Popcnt")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Popcnt);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Popcnt+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86PopcntX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86SseX64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse2")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse2);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse2+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse2X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse3")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse3);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse3+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse3X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse41")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse41);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse41+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse41X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse42")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse42);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Sse42+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Sse42X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Ssse3")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Ssse3);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.Ssse3+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86Ssse3X64);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.X86Serialize")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86X86Serialize);
        if ((bool?)spc.GetType("System.Runtime.Intrinsics.X86.X86Serialize+X64")?.GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false) extensions.Add(Extension.X86X86SerializeX64);
        return extensions.ToArray();
    }

    public static Extension? GetBaseExtension(Extension ext)
        => ext switch
        {
            Extension.ArmAdvSimd => Extension.ArmArmBase,
            Extension.ArmAdvSimdArm64 => Extension.ArmArmBaseArm64,
            Extension.ArmAes => Extension.ArmArmBase,
            Extension.ArmAesArm64 => Extension.ArmArmBaseArm64,
            Extension.ArmCrc32 => Extension.ArmArmBase,
            Extension.ArmCrc32Arm64 => Extension.ArmArmBaseArm64,
            Extension.ArmDp => Extension.ArmAdvSimd,
            Extension.ArmDpArm64 => Extension.ArmAdvSimdArm64,
            Extension.ArmRdm => Extension.ArmAdvSimd,
            Extension.ArmRdmArm64 => Extension.ArmAdvSimdArm64,
            Extension.ArmSha1 => Extension.ArmArmBase,
            Extension.ArmSha1Arm64 => Extension.ArmArmBaseArm64,
            Extension.ArmSha256 => Extension.ArmArmBase,
            Extension.ArmSha256Arm64 => Extension.ArmArmBaseArm64,
            Extension.ArmSve => Extension.ArmAdvSimd,
            Extension.ArmSveArm64 => Extension.ArmAdvSimdArm64,
            Extension.X86Aes => Extension.X86Sse2,
            Extension.X86AesX64 => Extension.X86Sse2X64,
            Extension.X86Avx => Extension.X86Sse42,
            Extension.X86AvxX64 => Extension.X86Sse42X64,
            Extension.X86Avx2 => Extension.X86Avx,
            Extension.X86Avx2X64 => Extension.X86AvxX64,
            Extension.X86Avx10v1 => Extension.X86Avx2,
            Extension.X86Avx10v1X64 => Extension.X86Avx2X64,
            Extension.X86Avx10v1V512 => Extension.X86Avx512BW,
            Extension.X86Avx512BW => Extension.X86Avx512F,
            Extension.X86Avx512BWVL => Extension.X86Avx512FVL,
            Extension.X86Avx512BWX64 => Extension.X86Avx512FX64,
            Extension.X86Avx512CD => Extension.X86Avx512F,
            Extension.X86Avx512CDVL => Extension.X86Avx512FVL,
            Extension.X86Avx512CDX64 => Extension.X86Avx512FX64,
            Extension.X86Avx512DQ => Extension.X86Avx512F,
            Extension.X86Avx512DQVL => Extension.X86Avx512FVL,
            Extension.X86Avx512DQX64 => Extension.X86Avx512FX64,
            Extension.X86Avx512F => Extension.X86Avx2,
            Extension.X86Avx512FX64 => Extension.X86Avx2X64,
            Extension.X86Avx512Vbmi => Extension.X86Avx512BW,
            Extension.X86Avx512VbmiVL => Extension.X86Avx512BWVL,
            Extension.X86Avx512VbmiX64 => Extension.X86Avx512BWX64,
            Extension.X86AvxVnni => Extension.X86Avx2,
            Extension.X86AvxVnniX64 => Extension.X86Avx2X64,
            Extension.X86Bmi1 => Extension.X86X86Base,
            Extension.X86Bmi1X64 => Extension.X86X86BaseX64,
            Extension.X86Bmi2 => Extension.X86X86Base,
            Extension.X86Bmi2X64 => Extension.X86X86BaseX64,
            Extension.X86Fma => Extension.X86Avx,
            Extension.X86FmaX64 => Extension.X86AvxX64,
            Extension.X86Lzcnt => Extension.X86X86Base,
            Extension.X86LzcntX64 => Extension.X86X86BaseX64,
            Extension.X86Pclmulqdq => Extension.X86Sse2,
            Extension.X86PclmulqdqX64 => Extension.X86Sse2X64,
            Extension.X86Popcnt => Extension.X86Sse42,
            Extension.X86PopcntX64 => Extension.X86Sse42X64,
            Extension.X86Sse => Extension.X86X86Base,
            Extension.X86SseX64 => Extension.X86X86BaseX64,
            Extension.X86Sse2 => Extension.X86Sse,
            Extension.X86Sse2X64 => Extension.X86SseX64,
            Extension.X86Sse3 => Extension.X86Sse2,
            Extension.X86Sse3X64 => Extension.X86Sse2X64,
            Extension.X86Sse41 => Extension.X86Ssse3,
            Extension.X86Sse41X64 => Extension.X86Ssse3X64,
            Extension.X86Sse42 => Extension.X86Sse41,
            Extension.X86Sse42X64 => Extension.X86Sse41X64,
            Extension.X86Ssse3 => Extension.X86Sse3,
            Extension.X86Ssse3X64 => Extension.X86Sse3X64,
            Extension.X86X86Serialize => Extension.X86X86Base,
            Extension.X86X86SerializeX64 => Extension.X86X86BaseX64,
            _ => null,
        };
}
