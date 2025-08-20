namespace HareDu.Diagnostics.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class ProbeResultStatusEnumConverter :
    JsonConverter<ProbeResultStatus>
{
    public override ProbeResultStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "healthy" => ProbeResultStatus.Healthy,
            "unhealthy" => ProbeResultStatus.Unhealthy,
            "inconclusive" => ProbeResultStatus.Inconclusive,
            "warning" => ProbeResultStatus.Warning,
            "na" => ProbeResultStatus.NA,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, ProbeResultStatus value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case ProbeResultStatus.Healthy:
                writer.WriteStringValue("healthy");
                break;

            case ProbeResultStatus.Unhealthy:
                writer.WriteStringValue("unhealthy");
                break;

            case ProbeResultStatus.Inconclusive:
                writer.WriteStringValue("inconclusive");
                break;

            case ProbeResultStatus.Warning:
                writer.WriteStringValue("warning");
                break;

            case ProbeResultStatus.NA:
                writer.WriteStringValue("na");
                break;

            default:
                throw new JsonException();
        }
    }
}