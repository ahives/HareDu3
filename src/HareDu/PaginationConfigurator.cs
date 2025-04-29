namespace HareDu;

/// <summary>
/// Represents a configuration interface for defining pagination parameters.
/// </summary>
public interface PaginationConfigurator
{
    /// <summary>
    /// Sets the page number for pagination.
    /// </summary>
    /// <param name="number">The page number to be set.</param>
    void Page(int number);

    /// <summary>
    /// Sets the page size for pagination.
    /// </summary>
    /// <param name="size">The number of items per page to be set.</param>
    void PageSize(int size);

    /// <summary>
    /// Sets the name filter for the pagination query.
    /// </summary>
    /// <param name="name">The name to be used as a filter.</param>
    void Name(string name);

    /// <summary>
    /// Enables or disables the use of regular expressions for the query.
    /// </summary>
    /// <param name="use">A boolean value indicating whether to use regular expressions.</param>
    void UseRegex(bool use);
}