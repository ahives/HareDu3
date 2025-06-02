namespace HareDu;

using System.Collections.Generic;
using Core;

internal static class Panic
{
    public static Result<T> Result<T>(T data, DebugInfo debugInfo) => new UnsuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result Result(DebugInfo debugInfo) => new UnsuccessfulResult {DebugInfo = debugInfo};

    public static Result<T> Result<T>(DebugInfo debugInfo) => new UnsuccessfulResult<T> {DebugInfo = debugInfo};

    public static Result<T> Result<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResult<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Result Result(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Results<T> Results<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};
}