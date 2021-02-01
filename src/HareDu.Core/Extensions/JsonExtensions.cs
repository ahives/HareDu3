namespace HareDu.Core.Extensions
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Serialization;

    public static class JsonExtensions
    {
        /// <summary>
        /// Takes an object and returns the JSON text representation of said object.
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJsonString<T>(this T obj)
        {
            if (obj.IsNull())
                return string.Empty;

            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Deserializes the contents of <see cref="HttpResponseMessage"/> and returns <see cref="Task"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ToObject<T>(this HttpResponseMessage responseMessage)
        {
            string rawResponse = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(rawResponse))
                return default;

            var options = new JsonSerializerOptions();
            
            options.Converters.Add(new CustomDecimalConverter());
            options.Converters.Add(new CustomDateTimeConverter());
            options.Converters.Add(new CustomLongConverter());
            
            return JsonSerializer.Deserialize<T>(rawResponse, options);
        }

        /// <summary>
        /// Deserializes the contents of a string encoded object and returns <see cref="T"/>
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToObject<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            var options = new JsonSerializerOptions();
            
            options.Converters.Add(new CustomDecimalConverter());
            options.Converters.Add(new CustomDateTimeConverter());
            options.Converters.Add(new CustomLongConverter());
            
            return JsonSerializer.Deserialize<T>(value, options);
        }
    }
}