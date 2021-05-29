using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace ShoppingCart.Helpers
{
    public static class SessionHelper
    {
        public static void SetObject<T>(this ISession session, string key, T value) where T : class, new()
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
          
        }
    }
}
