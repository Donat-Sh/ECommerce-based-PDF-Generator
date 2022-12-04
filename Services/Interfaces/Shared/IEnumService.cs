using System;

namespace Services.Interfaces.Shared
{
    public interface IEnumService
    {
        T GetEnumValueStringParam<T>(string str) where T : struct, IConvertible;

        T GetEnumValueIntParam<T>(int intValue) where T : struct, IConvertible;
    }
}
