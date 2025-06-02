namespace HareDu.Internal;

using System.Collections.Generic;
using Core;
using Core.Extensions;

internal class UnbindingConfiguratorImpl :
    UnbindingConfigurator
{
    List<Error> Errors { get; } = new();

    public string SourceBinding { get; private set; }
    public string DestinationBinding { get; private set; }

    public List<Error> Validate()
    {
        if (string.IsNullOrWhiteSpace(SourceBinding))
            Errors.Add("The name of the source binding (queue/exchange) is missing.");

        if (string.IsNullOrWhiteSpace(DestinationBinding))
            Errors.Add("The name of the destination binding (queue/exchange) is missing.");

        return Errors;
    }

    public void Source(string source) => SourceBinding = source;

    public void Destination(string destination) => DestinationBinding = destination;
}