using Newtonsoft.Json;

namespace Pharmacy_Project.Infrastructure
{
    public static class SessionExtensions
    {
        //Запись параметризованного объекта  в сессию
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        //Считывание параметризованного объекта из сессии
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
   

    }
}
