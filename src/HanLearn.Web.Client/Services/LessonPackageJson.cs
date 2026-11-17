using System.Text.Json;
using System.Text.Json.Serialization;

namespace HanLearn.Web.Client.Services;

public static class LessonPackageJson
{
    public static JsonSerializerOptions Options { get; } = new(JsonSerializerDefaults.Web)
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true
    };
}
