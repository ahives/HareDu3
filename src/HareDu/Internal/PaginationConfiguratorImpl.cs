namespace HareDu.Internal;

using System.Text;

internal class PaginationConfiguratorImpl :
    PaginationConfigurator
{
    int _pageNumber;
    int _pageSize;
    string _pageName;
    bool _useRegex;
    bool _useRegexSet = false;

    public string GetPagination()
    {
        StringBuilder sb = new StringBuilder();

        if (_pageNumber > 0)
            sb.Append($"&page={_pageNumber}");

        if (_pageSize > 0)
            sb.Append($"&page_size={_pageSize}");

        if (_useRegexSet)
        {
            string regex = _useRegex ? "true" : "false";
            sb.Append($"&page_size={regex}");
        }

        if (!string.IsNullOrWhiteSpace(_pageName))
            sb.Append($"&name={_pageName}");
            
        return sb.ToString().TrimStart('&');
    }

    public void Page(int number)
    {
        _pageNumber = number;
    }

    public void PageSize(int size)
    {
        _pageSize = size;
    }

    public void Name(string name)
    {
        _pageName = name;
    }

    public void UseRegex(bool use)
    {
        _useRegex = use;
        _useRegexSet = true;
    }
}