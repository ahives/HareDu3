namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record SocketOptions
    {
        [JsonPropertyName("backlog")]
        public long Backlog { get; init; }
        
        [JsonPropertyName("nodelay")]
        public bool NoDelay { get; init; }
        
        [JsonPropertyName("linger")]
        public IList<object> Linger { get; init; }
        
        [JsonPropertyName("exit_on_close")]
        public bool ExitOnClose { get; init; }
        
        [JsonPropertyName("cowboy_opts")]
        public ServerOptions ServerOptions { get; init; }
    }
}