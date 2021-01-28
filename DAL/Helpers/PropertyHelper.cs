using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static class PropertyHelper
    {
        public static Type GetUnderlyingPropertyType(Type propertyType)
        {
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return propertyType.GetGenericArguments()[0];
            else
                return propertyType;
        }
    }
}
