using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace HeatGamesWeb.Extensions
{
    public static class SessionExtensions
    {
        // Записва обект в сесията като го превръща в JSON текст
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Чете JSON текст от сесията и го връща като обект
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}