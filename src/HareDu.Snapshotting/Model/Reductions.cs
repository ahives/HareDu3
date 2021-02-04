namespace HareDu.Snapshotting.Model
{
    public record Reductions
    {
        /// <summary>
        /// Total number of CPU reductions.
        /// </summary>
        public long Total { get; init; }
        
        /// <summary>
        /// Rate at which CPU reductions are happening.
        /// </summary>
        public decimal Rate { get; init; }
    }
}