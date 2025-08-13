namespace HareDu.Core.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomULongConverter :
    JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                string stringValue = reader.GetString();
                if (ulong.TryParse(stringValue, out ulong value))
                    return value;
                
                return stringValue == "infinity" ? ulong.MaxValue : default;

            case JsonTokenType.Number:
                return reader.GetUInt64();

            case JsonTokenType.Null:
                return 0;

            default:
                throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}