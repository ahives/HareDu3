namespace HareDu.Core;

public static class ResultHelper
{
    public static Result<T> Missing<T>() => ResultCache<T>.MissingValue;
}