using Services.Interfaces.Shared;
using System;

namespace Services.Shared.EnumServices
{
    public class EnumService : IEnumService
    {
        public T GetEnumValueStringParam<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            
            if (!enumType.IsEnum)
                throw new Exception("T must be an Enumeration type.");

            return Enum.TryParse(str, true, out T val) ? val : default;
        }

        public T GetEnumValueIntParam<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new Exception("T must be an Enumeration type.");

            return (T)Enum.ToObject(enumType, intValue);
        }
    }
}
