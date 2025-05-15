using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics;
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

        List<(string ExtensionName, string? ParentExtensionName, string? CheckSupported, List<Api> APIs)> extensions = new();
        extensions.Add(("Default", null, null, []));
        extensions.Add(("Async", null, null, []));
        extensions.Add(("RuntimeAsync", null, null, []));
        extensions.Add(("VectorT", null, "Vector.IsHardwareAccelerated", []));
        extensions.Add(("Vector64", null, "Vector64.IsHardwareAccelerated", []));
        extensions.Add(("Vector128", null, "Vector128.IsHardwareAccelerated", []));
        extensions.Add(("Vector256", null, "Vector256.IsHardwareAccelerated", []));
        extensions.Add(("Vector512", null, "(bool?)spc.GetType(\"System.Runtime.Intrinsics.Vector512\")?.GetProperty(\"IsHardwareAccelerated\", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false", []));

        string? ToExtensionName(string fullName)
        {
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

        foreach (Type t in intrinsicTypes.OrderBy(t => t.FullName))
        {
            string? extensionName = ToExtensionName(t.FullName!);
            if (extensionName == null)
                continue;

            string? baseExtensionName = ToExtensionName(t.BaseType!.FullName!);

            string checkIsSupported =
                $"(bool?)spc.GetType(\"{t.FullName}\")?.GetProperty(\"IsSupported\", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ?? false";

            string namespaceName = t.Namespace!;

            string className = t.Name;
            Type? parentType = t.DeclaringType;
            while (parentType != null)
            {
                className = $"{parentType.Name}.{t.Name}";
                parentType = parentType.DeclaringType;
            }

            List<Api> apis = new();

            foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.Static).OrderBy(t => t.Name).ThenBy(MethodSignature))
            {
                if (mi.Name == "get_IsSupported")
                {
                    continue;
                }

                if (className == "Sve")
                {
                    if (mi.Name.StartsWith("Scatter") || mi.Name.StartsWith("GatherVector"))
                    {
                        // These APIs take memory addresses as Vector<ulong>, which
                        // would AV.
                        continue;
                    }

                    if (mi.Name.StartsWith("GetFfr") || mi.Name.StartsWith("SetFfr"))
                    {
                        // These APIs deal with processor state that opts may
                        // affect by design.
                        continue;
                    }
                }

                string? returnFuzzType = ToFuzzType(mi.ReturnType);
                if (returnFuzzType == null)
                    continue;

                List<string> paramFuzzTypes = new();
                List<string?> paramMetadata = new();
                bool createdAll = true;
                foreach (ParameterInfo parameter in mi.GetParameters())
                {
                    if (parameter.HasDefaultValue)
                    {
                        continue;
                    }

                    ConstantExpectedAttribute? cns = parameter.GetCustomAttribute<ConstantExpectedAttribute>();
                    if (cns != null)
                    {
                        paramMetadata.Add($"new ParameterMetadata(true, {cns.Min?.ToString() ?? "null"}, {cns.Max?.ToString() ?? "null"})");
                    }
                    else
                    {
                        paramMetadata.Add("null");
                    }

                    string? paramFuzzType = ToFuzzType(parameter.ParameterType);
                    if (paramFuzzType == null)
                    {
                        createdAll = false;
                        break;
                    }

                    paramFuzzTypes.Add(paramFuzzType);
                }

                if (!createdAll)
                {
                    continue;
                }

                string?[]? paramMetadataArr = null;
                if (paramMetadata.Any(pm => pm != "null"))
                {
                    paramMetadataArr = paramMetadata.ToArray();
                }

                apis.Add(new Api(namespaceName, className, mi.Name, returnFuzzType, paramFuzzTypes.ToArray(), paramMetadataArr));

                void Error(string err)
                {
                    Console.WriteLine($"Cannot add API '{MethodSignature(mi)}': {err}");
                }

                string? ToFuzzType(Type type)
                {
                    if (type.IsByRef)
                    {
                        Error("ByRef");
                        return null;
                    }

                    if (type == typeof(ulong))
                        return "new PrimitiveType(SyntaxKind.ULongKeyword)";
                    if (type == typeof(long))
                        return "new PrimitiveType(SyntaxKind.LongKeyword)";
                    if (type == typeof(uint))
                        return "new PrimitiveType(SyntaxKind.UIntKeyword)";
                    if (type == typeof(int))
                        return "new PrimitiveType(SyntaxKind.IntKeyword)";
                    if (type == typeof(ushort))
                        return "new PrimitiveType(SyntaxKind.UShortKeyword)";
                    if (type == typeof(short))
                        return "new PrimitiveType(SyntaxKind.ShortKeyword)";
                    if (type == typeof(byte))
                        return "new PrimitiveType(SyntaxKind.ByteKeyword)";
                    if (type == typeof(sbyte))
                        return "new PrimitiveType(SyntaxKind.SByteKeyword)";
                    if (type == typeof(float))
                        return "new PrimitiveType(SyntaxKind.FloatKeyword)";
                    if (type == typeof(double))
                        return "new PrimitiveType(SyntaxKind.DoubleKeyword)";
                    if (type == typeof(bool))
                        return "new PrimitiveType(SyntaxKind.BoolKeyword)";
                    if (type == typeof(void))
                        return "null";

                    if (type.IsConstructedGenericType)
                    {
                        Type baseType = type.GetGenericTypeDefinition();
                        string?[] argTypes = type.GetGenericArguments().Select(ToFuzzType).ToArray();
                        if (argTypes.Any(s => s == null))
                        {
                            return null;
                        }

                        if (baseType == typeof(Vector64<>))
                            return $"new VectorType(VectorTypeWidth.Width64, {argTypes[0]})";

                        if (baseType == typeof(Vector128<>))
                            return $"new VectorType(VectorTypeWidth.Width128, {argTypes[0]})";

                        if (baseType == typeof(Vector256<>))
                            return $"new VectorType(VectorTypeWidth.Width256, {argTypes[0]})";

                        if (baseType == typeof(Vector512<>))
                            return $"new VectorType(VectorTypeWidth.Width512, {argTypes[0]})";

                        if (baseType == typeof(Vector<>))
                            return $"new VectorType(VectorTypeWidth.WidthUnknown, {argTypes[0]})";

                        if (baseType == typeof(ValueTuple<,>))
                            return null;

                        if (baseType == typeof(ValueTuple<,,>))
                            return null;

                        if (baseType == typeof(ValueTuple<,,,>))
                            return null;

                        Error($"Unhandled generic type {TypeName(type)}");
                        return null;
                    }

                    if (type == typeof(IntPtr))
                        return null;

                    if (type == typeof(UIntPtr))
                        return null;

                    if (type.IsPointer)
                        return null;

                    switch (type.FullName)
                    {
                        case "System.Runtime.Intrinsics.X86.FloatRoundingMode":
                        case "System.Runtime.Intrinsics.X86.FloatComparisonMode":
                        case "System.Runtime.Intrinsics.Arm.SveMaskPattern":
                        case "System.Runtime.Intrinsics.Arm.SvePrefetchType":
                            return null;
                    }

                    Error($"Unhandled type {TypeName(type)}");
                    return null;
                }
            }

            if (apis.Count > 0)
            {
                extensions.Add((extensionName, baseExtensionName, checkIsSupported, apis));
            }
        }

        {
            using StreamWriter sw = new StreamWriter(File.Create(args[0]));

            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Numerics;");
            sw.WriteLine("using System.Reflection;");
            sw.WriteLine("using System.Runtime.Intrinsics;");
            sw.WriteLine();
            sw.WriteLine("namespace Fuzzlyn.ExecutionServer;");
            sw.WriteLine();
            sw.WriteLine("public enum Extension");
            sw.WriteLine("{");

            foreach ((string ext, _, _, _) in extensions)
            {
                sw.WriteLine($"    {ext},");
            }

            sw.WriteLine("}");
            sw.WriteLine();

            sw.WriteLine("public static class ExtensionHelpers");
            sw.WriteLine("{");
            sw.WriteLine("    public static Extension[] GetSupportedIntrinsicExtensions()");
            sw.WriteLine("    {");
            sw.WriteLine("        List<Extension> extensions = [];");
            sw.WriteLine("        Assembly spc = typeof(System.Runtime.Intrinsics.X86.Sse).Assembly;");

            foreach ((string ext, _, string? check, _) in extensions)
            {
                if (check == null)
                    continue;

                sw.WriteLine($"        if ({check}) extensions.Add(Extension.{ext});", check, ext);
            }
            sw.WriteLine("        return extensions.ToArray();");
            sw.WriteLine("    }");
            sw.WriteLine();
            sw.WriteLine("    public static Extension? GetBaseIntrinsicExtension(Extension ext)");
            sw.WriteLine("        => ext switch");
            sw.WriteLine("        {");

            foreach ((string ext, string? baseExt, _, _) in extensions)
            {
                if (baseExt == null)
                    continue;

                if (!extensions.Any(t => t.ExtensionName == baseExt))
                    continue;

                sw.WriteLine($"            Extension.{ext} => Extension.{baseExt},");
            }

            sw.WriteLine("            _ => null,");
            sw.WriteLine("        };");
            sw.WriteLine("}");
        }

        {
            using StreamWriter sw = new StreamWriter(File.Create(args[1]));
            sw.WriteLine("using Fuzzlyn.ExecutionServer;");
            sw.WriteLine("using Fuzzlyn.Types;");
            sw.WriteLine("using Microsoft.CodeAnalysis.CSharp;");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("");
            sw.WriteLine("namespace Fuzzlyn.Methods;");
            sw.WriteLine("");
            sw.WriteLine("public class ParameterMetadata(bool constantExpected, int? constantMin, int? constantMax)");
            sw.WriteLine("{");
            sw.WriteLine("    public bool ConstantExpected { get; } = constantExpected;");
            sw.WriteLine("    public int? ConstantMin { get; } = constantMin;");
            sw.WriteLine("    public int? ConstantMax { get; } = constantMax;");
            sw.WriteLine("}");
            sw.WriteLine("");
            sw.WriteLine("public class Api(string namespaceName, string className, string methodName, FuzzType returnType, FuzzType[] parameterTypes, ParameterMetadata[] parameterMetadata)");
            sw.WriteLine("{");
            sw.WriteLine("    public string NamespaceName { get; } = namespaceName;");
            sw.WriteLine("    public string ClassName { get; } = className;");
            sw.WriteLine("    public string MethodName { get; } = methodName;");
            sw.WriteLine("    public FuzzType ReturnType { get; } = returnType;");
            sw.WriteLine("    public FuzzType[] ParameterTypes { get; } = parameterTypes;");
            sw.WriteLine("    public ParameterMetadata[] ParameterMetadata { get; } = parameterMetadata;");
            sw.WriteLine("}");
            sw.WriteLine("");
            sw.WriteLine("public static class ApiTable");
            sw.WriteLine("{");
            sw.WriteLine("    public static Api[] GetApis(Extension extension) =>");
            sw.WriteLine("        extension switch");
            sw.WriteLine("        {");

            foreach ((string extName, _, _, List<Api>? apis) in extensions)
            {
                sw.WriteLine($"            Extension.{extName} => Get{extName}(),");
            }

            sw.WriteLine("            _ => throw new ArgumentOutOfRangeException(nameof(extension)),");

            sw.WriteLine("        };");
            sw.WriteLine("");

            int identifierIndex = 0;
            string IdentifierName(string value)
            {
                string id = $"s_t{identifierIndex++}";
                sw.WriteLine($"    private static readonly FuzzType {id} = {value};");
                return id;
            }

            Dictionary<string, string> memoizedTypes =
                extensions
                .SelectMany(e => e.APIs)
                .SelectMany(a => new string[] { a.ReturnType }.Concat(a.ParameterTypes))
                .GroupBy(t => t)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => IdentifierName(g.Key));

            foreach ((string extName, _, _, List<Api>? apis) in extensions)
            {
                sw.WriteLine("");
                sw.Write($"    private static Api[] Get{extName}() => ");

                if (apis.Count == 0)
                {
                    sw.WriteLine("[];");
                    continue;
                }

                sw.WriteLine("[");

                foreach (Api api in apis)
                {
                    string returnType = memoizedTypes[api.ReturnType];
                    string parameterTypes = string.Join(", ", api.ParameterTypes.Select(pt => memoizedTypes[pt]));

                    string parameterMetadata = api.ParameterMetadata == null ? "null" : $"[{string.Join(", ", api.ParameterMetadata)}]";
                    sw.WriteLine($"        new Api(\"{api.Namespace}\", \"{api.ClassName}\", \"{api.MethodName}\", {returnType}, [{parameterTypes}], {parameterMetadata}),");
                }

                sw.WriteLine("    ];");
            }

            sw.WriteLine("}");
        }
    }

    private static string TypeName(Type type)
    {
        if (type.IsConstructedGenericType)
        {
            return $"{type.GetGenericTypeDefinition().FullName}<{string.Join(", ", type.GetGenericArguments().Select(TypeName))}>";
        }

        return type.FullName;
    }

    private static string MethodSignature(MethodInfo mi)
    {
        return $"{TypeName(mi.ReturnType)} {TypeName(mi.DeclaringType!)}.{mi.Name}({string.Join(", ", mi.GetParameters().Select(pmi => TypeName(pmi.ParameterType)))})";
    }

    private class Api(string namespaceName, string className, string methodName, string returnType, string[] parameterTypes, string?[]? parameterMetadata)
    {
        public string Namespace { get; } = namespaceName;
        public string ClassName { get; } = className;
        public string MethodName { get; } = methodName;
        public string ReturnType { get; } = returnType;
        public string[] ParameterTypes { get; } = parameterTypes;
        public string?[]? ParameterMetadata { get; } = parameterMetadata;
    }
}
