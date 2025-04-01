using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Persistence.Infrastructure;

public sealed class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            PropertyInfo? propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
            {
                var hasPrivateSetter = propertyInfo.GetSetMethod(true) != null;
                prop.Writable = hasPrivateSetter;
            }
        }

        return prop;
    }
}
