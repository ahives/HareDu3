namespace HareDu;

using System.Collections.Generic;
using Core;

public static class Panic
{
    public static Result<T> Result<T>(T data, DebugInfo debugInfo) => new FaultedResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result Result(DebugInfo debugInfo) => new FaultedResult {DebugInfo = debugInfo};

    public static Result<T> Result<T>(DebugInfo debugInfo) => new FaultedResult<T> {DebugInfo = debugInfo};

    public static Result<T> Result<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Result Result(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Results<T> Results<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};
}