namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomDecimalConverter :
    JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                string stringValue = reader.GetString();
                if (decimal.TryParse(stringValue, out var value))
                    return value;
                break;

            case JsonTokenType.Number:
                return reader.GetDecimal();

            case JsonTokenType.Null:
                return default;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}