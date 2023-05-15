using System.Text.Json;

namespace BE_001.Web.Helpers;

public static class SessionHelpers
{
    public static void AddJson<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetJson<T>(this ISession session, string key)
    {
        var stored = session.GetString(key);

        return stored is null
            ? default
            : JsonSerializer.Deserialize<T>(stored);
    }
}