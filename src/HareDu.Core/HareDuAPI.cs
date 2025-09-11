namespace HareDu.Core;

using Serialization;

public interface HareDuAPI
{
    IHareDuDeserializer Deserializer { get; }
}