namespace HareDu;

using System.Collections.Generic;
using Core;

internal static class Responses
{
    public static Results<T> Failed<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    public static Results<T> Succeeded<T>(string url, IReadOnlyList<T> data, string request = null, string response = null) =>
        new SuccessfulResults<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    public static Results<T> Faulted<T>(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    public static Results<T> Panic<T>(string url, List<Error> errors, string request = null, string response = null) =>
        new UnsuccessfulResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};
}