namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomDateTimeConverter :
    JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                string stringValue = reader.GetString();
                if (DateTimeOffset.TryParse(stringValue, out var value))
                    return value;
                break;
            }
            case JsonTokenType.Null:
                return default;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}