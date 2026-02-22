namespace SmartProperty.ResultPattern;

/// <summary>
/// Canonical error type with code and message, used as TError in Result&lt;TValue, Error&gt; and Result&lt;TValue&gt;.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">Human-readable error message.</param>
public readonly record struct Error(ErrorCode Code, string Message);
