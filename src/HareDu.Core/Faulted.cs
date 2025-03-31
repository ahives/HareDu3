namespace HareDu.Core;

using System.Collections.Generic;

public static class Faulted
{
    public static Result ExceptionResult(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Result<T> ExceptionResult<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Results<T> ExceptionResults<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Result<T> Result<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Result Result(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Results<T> Results<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};
}