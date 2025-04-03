namespace HareDu;

using System.Collections.Generic;
using Core;

internal static class Faulted
{
    public static Result Result(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Result<T> Result<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Results<T> Results<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};
}