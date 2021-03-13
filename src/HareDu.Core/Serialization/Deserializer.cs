namespace HareDu.Core.Serialization
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class Deserializer
    {
        public static JsonSerializerOptions Options =>
            new()
            {
                WriteIndented = true,
                Converters =
                {
                    new CustomDecimalConverter(),
                    new CustomDateTimeConverter(),
                    new CustomLongConverter(),
                    new CustomStringConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
    }
}