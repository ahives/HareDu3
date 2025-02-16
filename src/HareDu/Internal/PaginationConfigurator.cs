namespace HareDu.Internal;

public interface PaginationConfigurator
{
    void Page(int number);

    void PageSize(int size);

    void Name(string name);

    void UseRegex(bool use);
}