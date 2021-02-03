namespace HareDu.Snapshotting.Model
{
    public enum ConnectionState
    {
        Starting,
        Tuning,
        Opening,
        Running,
        Flow,
        Blocking,
        Blocked,
        Closing,
        Closed,
        Inconclusive
    }
}