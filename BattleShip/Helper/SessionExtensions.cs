using BattleShip.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BattleShip.Helper
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetMatrixAsJson(this ISession session, string key, Players value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetMatrixFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}