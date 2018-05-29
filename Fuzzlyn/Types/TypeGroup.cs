using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Types
{
    internal struct TypeGroup
    {
        private readonly object _obj;

        private TypeGroup(TypeGroupKind kind, object obj)
        {
            Kind = kind;
            _obj = obj;
        }

        public TypeGroupKind Kind { get; }
        public FuzzType SingleType
            => Kind == TypeGroupKind.Single
               ? (FuzzType)_obj
               : throw new InvalidOperationException("Cannot retrieve single type from TypeGroup of kind " + Kind);

        public IReadOnlyList<FuzzType> MultipleTypes
            => Kind == TypeGroupKind.Multiple
               ? (FuzzType[])_obj
               : throw new InvalidOperationException("Cannot retrieve multiple type from TypeGroup of kind " + Kind);

        public bool Contains(FuzzType type)
        {
            switch (Kind)
            {
                case TypeGroupKind.Single:
                    return type.Equals(_obj);
                case TypeGroupKind.Multiple:
                    return ((FuzzType[])_obj).Any(ft => type.Equals(ft));
                case TypeGroupKind.Integral:
                    return type is PrimitiveType pt && pt.Info.IsIntegral;
                case TypeGroupKind.Any:
                    return true;
                default:
                    return false;
            }
        }

        public static TypeGroup Single(FuzzType type)
            => new TypeGroup(TypeGroupKind.Single, type);

        public static TypeGroup Integral => new TypeGroup(TypeGroupKind.Integral, null);

        public static TypeGroup Multiple(params FuzzType[] types)
            => new TypeGroup(TypeGroupKind.Multiple, (FuzzType[])types.Clone());

        public static TypeGroup Any => new TypeGroup(TypeGroupKind.Any, null);
    }

    internal enum TypeGroupKind
    {
        None,
        Single,
        Multiple,
        Integral,
        Any,
    }
}
