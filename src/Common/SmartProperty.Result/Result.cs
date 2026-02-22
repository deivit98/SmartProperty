namespace SmartProperty.Result;

/// <summary>
/// Result type: either a success value or an error. When <see cref="IsError"/> is true, <see cref="Value"/> is null and <see cref="Error"/> is set.
/// </summary>
public readonly struct Result<TValue>
{
    private readonly bool _isSuccess;
    private readonly TValue? _value;
    private readonly Error _error;

    private Result(bool isSuccess, TValue? value, Error error)
    {
        _isSuccess = isSuccess;
        _value = value;
        _error = error;
    }

    /// <summary>True when the result has a value; false when it has an error.</summary>
    public bool IsSuccess => _isSuccess;

    /// <summary>True when the result has an error; then <see cref="Value"/> is null and <see cref="Error"/> is set.</summary>
    public bool IsError => !_isSuccess;

    /// <summary>The value when successful; null when <see cref="IsError"/> is true.</summary>
    public TValue? Value => _isSuccess ? _value : default;

    /// <summary>The error when failed; only meaningful when <see cref="IsError"/> is true.</summary>
    public Error Error => _error;

    /// <summary>Creates a successful result with the given value.</summary>
    public static Result<TValue> Ok(TValue value) =>
        new(true, value, default);

    /// <summary>Creates a failed result with the given error code and message.</summary>
    public static Result<TValue> Fail(ErrorCode code, string message) =>
        new(false, default, new Error(code, message));

    /// <summary>Creates a failed result with the given error.</summary>
    public static Result<TValue> Fail(Error error) =>
        new(false, default, error);

    /// <summary>Tries to get the value; returns true and sets <paramref name="value"/> when successful.</summary>
    public bool TryGetValue(out TValue? value)
    {
        if (_isSuccess) { value = _value; return true; }
        value = default; return false;
    }

    /// <summary>Returns the value when successful; throws when failed.</summary>
    public TValue GetValueOrThrow()
    {
        if (_isSuccess) return _value!;
        throw new InvalidOperationException("Result has no value; it contains an error.");
    }

    /// <summary>Maps the value to a new type when successful; preserves the error when failed.</summary>
    public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> map)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        return _isSuccess
            ? Result<TNewValue>.Ok(map(_value!))
            : Result<TNewValue>.Fail(_error);
    }

    /// <summary>Matches on success or error and returns a single value.</summary>
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onError)
    {
        if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
        if (onError == null) throw new ArgumentNullException(nameof(onError));
        return _isSuccess ? onSuccess(_value!) : onError(_error);
    }

    /// <summary>Chains another result-returning operation when successful; preserves the error when failed.</summary>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> bind)
    {
        if (bind == null) throw new ArgumentNullException(nameof(bind));
        return _isSuccess ? bind(_value!) : Result<TNewValue>.Fail(_error);
    }
}
