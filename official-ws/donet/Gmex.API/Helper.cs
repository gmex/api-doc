using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Gmex.API
{
    public static class Helper
    {
        /// <summary>
        /// Convert a date time object to Unix time representation.
        /// </summary>
        /// <param name="datetime">The datetime object to convert to Unix time stamp.</param>
        /// <returns>Returns a numerical representation (Unix time) of the DateTime object.</returns>
        public static long ConvertToUnixTime(this DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalMilliseconds;
        }

        /// <summary>
        /// GetEnumMemberValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumMemberValue<T>(T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        /// <summary>
        /// 方便外部安全的转换 Newtonsoft.Json.Linq.JToken 到指定类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MyJsonSafeToObj<T>(object obj)
        {
            var token = obj as Newtonsoft.Json.Linq.JToken;
            if (token == null)
            {
                return default(T);
            }
            try
            {
                return token.ToObject<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ERROR] MyJsonSafeToObj: " + ex.Message);
            }
            return default(T);
        }

        public static T MyJsonUnmarshal<T>(string value)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("[ERROR] MyJsonSafeToObj: " + ex.Message);
                throw new InvalidOperationException($"JSON.Deserialize Failed: TXT={value}", ex);
            }
        }

        private static Newtonsoft.Json.JsonSerializerSettings _json_default_setting = new Newtonsoft.Json.JsonSerializerSettings
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate
        };

        public static string MyJsonMarshal(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value,
                Newtonsoft.Json.Formatting.None,
                _json_default_setting
                );
        }

        public static string MyJsonMarshalIndent(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, 
                Newtonsoft.Json.Formatting.Indented,
                _json_default_setting
                );
        }
    }
}
