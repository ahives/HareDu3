namespace HareDu;

using System.Collections.Generic;
using Core;

internal static class Successful
{
    public static Result<T> Result<T>(string url, T data, string request, string response) =>
        new SuccessfulResult<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    public static Results<T> Results<T>(string url, IReadOnlyList<T> data, string request = null, string response = null) =>
        new SuccessfulResults<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    public static Result Result(string url, string response, string request = null) =>
        new SuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};
}