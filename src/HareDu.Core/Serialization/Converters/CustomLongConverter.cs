namespace HareDu.Core.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomLongConverter :
    JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                string stringValue = reader.GetString();
                if (long.TryParse(stringValue, out long value))
                    return value;
                
                return stringValue == "infinity" ? long.MaxValue : default;

            case JsonTokenType.Number:
                return reader.GetInt64();

            case JsonTokenType.Null:
                return default;

            default:
                throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}