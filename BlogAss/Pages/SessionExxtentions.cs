using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogAss.Pages
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);
            if (serializedValue == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
    }
}
