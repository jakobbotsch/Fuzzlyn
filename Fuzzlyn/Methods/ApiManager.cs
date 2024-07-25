using Fuzzlyn.ExecutionServer;
using Fuzzlyn.Types;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Fuzzlyn.Methods;

internal class ApiManager(Randomizer random)
{
    private readonly Randomizer _random = random;

    private readonly List<Api> _apis = new();
    private (string, Api[])[] _byName;
    private readonly Dictionary<FuzzType, List<Api>> _byCompatibleWithType = new();

    public void Initialize(TypeManager types, HashSet<Extension> extensions)
    {
        foreach (Extension ext in extensions)
        {
            Api[] apis = ApiTable.GetApis(ext);
            foreach (Api api in apis)
            {
                if (api.ReturnType != null && !types.IsSupportedType(api.ReturnType))
                    continue;

                if (!api.ParameterTypes.All(types.IsSupportedType))
                    continue;

                _apis.Add(api);
            }
        }

        _byName =
            _apis
            .GroupBy(api => $"{api.ClassName}.{api.MethodName}")
            .Select(g => (g.Key, g.ToArray()))
            .ToArray();

        List<Api> GetCompatible(FuzzType ft)
        {
            ref List<Api> list = ref CollectionsMarshal.GetValueRefOrAddDefault(_byCompatibleWithType, ft, out _);
            list ??= new List<Api>();
            return list;
        }

        foreach (Api api in _apis)
        {
            if (api.ReturnType == null)
                continue;

            GetCompatible(api.ReturnType).Add(api);

            if (api.ReturnType is PrimitiveType pt)
            {
                foreach (PrimitiveType otherPt in types.PrimitiveTypes)
                {
                    if (pt.IsCastableTo(otherPt))
                    {
                        GetCompatible(otherPt).Add(api);
                    }
                }
            }
        }
    }

    public bool Any()
    {
        return _apis.Any();
    }

    public Api PickRandomApi(FuzzType type)
    {
        if (type == null)
        {
            (string name, Api[] apis) = _random.NextElement(_byName);
            return _random.NextElement(apis);
        }

        {
            if (_byCompatibleWithType.TryGetValue(type, out List<Api> apis))
            {
                return _random.NextElement(apis);
            }
        }

        return null;
    }

    public IEnumerable<string> GetNamespaces()
    {
        return _apis.Select(a => a.NamespaceName).Distinct().OrderBy(n => n);
    }
}
