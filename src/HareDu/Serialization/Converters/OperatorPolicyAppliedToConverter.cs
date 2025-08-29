namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class OperatorPolicyAppliedToConverter :
    JsonConverter<OperatorPolicyAppliedTo>
{
    public override OperatorPolicyAppliedTo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "classic_queues" => OperatorPolicyAppliedTo.ClassicQueues,
            "queues" => OperatorPolicyAppliedTo.Queues,
            "quorum_queues" => OperatorPolicyAppliedTo.QuorumQueues,
            "streams" => OperatorPolicyAppliedTo.Streams,
            "exchanges" => OperatorPolicyAppliedTo.Exchanges,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, OperatorPolicyAppliedTo value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case OperatorPolicyAppliedTo.ClassicQueues:
                writer.WriteStringValue("classic_queues");
                break;

            case OperatorPolicyAppliedTo.Queues:
                writer.WriteStringValue("queues");
                break;

            case OperatorPolicyAppliedTo.QuorumQueues:
                writer.WriteStringValue("quorum_queues");
                break;

            case OperatorPolicyAppliedTo.Streams:
                writer.WriteStringValue("streams");
                break;

            case OperatorPolicyAppliedTo.Exchanges:
                writer.WriteStringValue("exchanges");
                break;

            default:
                throw new JsonException();
        }
    }
}