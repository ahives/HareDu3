namespace HareDu.Internal;

using System.Collections.Generic;
using Core;
using Core.Extensions;

internal class UnbindingConfiguratorImpl :
    UnbindingConfigurator
{
    List<Error> InternalErrors { get; } = new();

    public string SourceBinding { get; private set; }
    public string DestinationBinding { get; private set; }

    public List<Error> Validate()
    {
        InternalErrors.AddIfTrue(SourceBinding, string.IsNullOrWhiteSpace, Errors.Create("The name of the source binding (queue/exchange) is missing."));
        InternalErrors.AddIfTrue(DestinationBinding, string.IsNullOrWhiteSpace, Errors.Create("The name of the destination binding (queue/exchange) is missing."));

        return InternalErrors;
    }

    public void Source(string source) => SourceBinding = source;

    public void Destination(string destination) => DestinationBinding = destination;
}