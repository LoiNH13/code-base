using Domain.Core.Primitives;

namespace Application.Core.Extensions;

public static class CacheExtension
{
    public static string GetKey(this string id, Type type)
    {
        return type.ToString() + ":" + id;
    }

    public static string GetKey<T>(this T obj) where T : Entity
    {
        return obj.GetType().ToString() + ":" + obj.Id;
    }
}
