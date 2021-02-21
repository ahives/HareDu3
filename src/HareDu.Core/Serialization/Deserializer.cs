namespace HareDu.Core.Serialization
{
    using System.Text.Json;

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
                    new CustomStringConverter()
                }
            };
    }
}