namespace HareDu;

using System.Collections.Generic;
using Core;

internal static class Response
{
    public static Result<T> Failed<T>(T data, DebugInfo debugInfo) => new UnsuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result Failed(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Result<T> Failed<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResult<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Result<T> Succeeded<T>(T data, DebugInfo debugInfo) => new SuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result<T> Succeeded<T>(string url, T data, string request, string response) =>
        new SuccessfulResult<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    public static Result Succeeded(string url, string response, string request = null) =>
        new SuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    public static Result Faulted(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Result<T> Faulted<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};
}