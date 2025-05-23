namespace HareDu;

using System.Collections.Generic;
using System.Text;
using Core;

internal class PaginationConfiguratorImpl :
    PaginationConfigurator
{
    int _pageNumber;
    int _pageSize;
    string _pageName;
    bool _useRegex;
    bool _useRegexSet = false;

    public string BuildPaginationParams()
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
    
    public List<Error> Validate()
    {
        var errors = new List<Error>();

        if (_pageSize is < 1 or > 500)
            errors.Add(new(){Reason = "Page size must be between 1 and 500."});

        return errors;
    }
}