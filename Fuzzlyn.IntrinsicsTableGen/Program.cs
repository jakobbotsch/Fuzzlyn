using System.Runtime.Intrinsics.X86;

namespace Fuzzlyn.IntrinsicsTableGen;

internal class Program
{
    static void Main(string[] args)
    {
        Type[] exportedTypes = typeof(Sse).Assembly.GetExportedTypes();
        HashSet<string> namespaces = ["System.Runtime.Intrinsics.X86", "System.Runtime.Intrinsics.Arm"];
        Type[] intrinsicTypes =
            exportedTypes
            .Where(t => namespaces.Contains(t.Namespace!) && t.GetProperty("IsSupported") != null)
            .ToArray();

        List<(string ExtensionName, string? ParentExtensionName, string? CheckSupported)> extensions = new();
        extensions.Add(("AllSupported", null, null));
        extensions.Add(("VectorT", null, "Vector.IsHardwareAccelerated"));
        extensions.Add(("Vector64", null, "Vector64.IsHardwareAccelerated"));
        extensions.Add(("Vector128", null, "Vector128.IsHardwareAccelerated"));
        extensions.Add(("Vector256", null, "Vector256.IsHardwareAccelerated"));
        extensions.Add(("Vector512", null, "(bool?)spc.GetType(\"System.Runtime.Intrinsics.Vector512\")?.GetProperty(\"IsHardwareAccelerated\", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false"));

        string? ToExtensionName(string fullName)
        {
            // IsSupported is an infinite loop for this ISA, TODO: Fix
            if (fullName == "System.Runtime.Intrinsics.X86.Avx10v1+V512+X64")
            {
                return null;
            }

            foreach (string ns in namespaces)
            {
                if (fullName.StartsWith(ns))
                {
                    string extName = fullName[("System.Runtime.Intrinsics".Length + 1)..];
                    extName = extName.Replace(".", "").Replace("+", "");
                    return extName;
                }
            }

            return null;
        }

        foreach (Type t in intrinsicTypes)
        {
            string? extensionName = ToExtensionName(t.FullName!);
            if (extensionName == null)
                continue;

            string? baseExtensionName = ToExtensionName(t.BaseType!.FullName!);

            string checkIsSupported =
                $"(bool?)spc.GetType(\"{t.FullName}\")?.GetProperty(\"IsSupported\", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false";

            extensions.Add((extensionName, baseExtensionName, checkIsSupported));
        }

        {
            using StreamWriter sw = new StreamWriter(File.OpenWrite(args[0]));

            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Numerics;");
            sw.WriteLine("using System.Reflection;");
            sw.WriteLine("using System.Runtime.Intrinsics;");
            sw.WriteLine();
            sw.WriteLine("namespace Fuzzlyn.ExecutionServer;");
            sw.WriteLine();
            sw.WriteLine("public enum Extension");
            sw.WriteLine("{");

            foreach ((string ext, _, _) in extensions)
            {
                sw.WriteLine($"    {ext},");
            }

            sw.WriteLine("}");
            sw.WriteLine();

            sw.WriteLine("public static class ExtensionHelpers");
            sw.WriteLine("{");
            sw.WriteLine("    public static Extension[] GetSupportedExtensions()");
            sw.WriteLine("    {");
            sw.WriteLine("        List<Extension> extensions = [];");
            sw.WriteLine("        Assembly spc = typeof(System.Runtime.Intrinsics.X86.Sse).Assembly;");

            foreach ((string ext, _, string? check) in extensions)
            {
                if (check == null)
                    continue;

                sw.WriteLine($"        if ({check}) extensions.Add(Extension.{ext});", check, ext);
            }
            sw.WriteLine("        return extensions.ToArray();");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    public static Extension? GetBaseExtension(Extension ext)");
            sw.WriteLine("        => ext switch");
            sw.WriteLine("        {");

            foreach ((string ext, string? baseExt, _) in extensions)
            {
                if (baseExt == null)
                    continue;

                sw.WriteLine($"            Extension.{ext} => Extension.{baseExt},");
            }

            sw.WriteLine("            _ => null,");
            sw.WriteLine("        };");
            sw.WriteLine("}");
        }
    }
}
