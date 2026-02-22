namespace SmartProperty.Result;

/// <summary>
/// Extension methods for <see cref="Result{TValue}"/>.
/// </summary>
public static class ResultExtensions
{
    /// <summary>Executes the appropriate action based on success or error; no return value.</summary>
    public static void Match<TValue>(this Result<TValue> result, Action<TValue> onSuccess, Action<Error> onError)
    {
        if (onSuccess == null)
            throw new ArgumentNullException(nameof(onSuccess));
        if (onError == null)
            throw new ArgumentNullException(nameof(onError));
        if (result.IsSuccess)
            onSuccess(result.Value!);
        else
            onError(result.Error);
    }
}
