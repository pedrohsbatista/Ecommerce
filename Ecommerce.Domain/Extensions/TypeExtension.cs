using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Domain.Extensions
{
    public static class TypeExtensions
    {
        private static readonly List<Type> PrimitivesTypes = new List<Type>
        {
            typeof(string),
            typeof(DateTimeOffset)
        };

        public static bool IsPrimitive(this Type type)
        {
            return PrimitivesTypes.Any(x => x == type);
        }
    }
}
