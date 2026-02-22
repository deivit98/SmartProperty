namespace SmartProperty.Result;

/// <summary>
/// Domain-agnostic error codes for Result failures.
/// Extend with BadInput, Conflict, validation, or external-service codes as needed.
/// </summary>
public enum ErrorCode
{
    None = 0,
    NotFound,
    EmptyParameter,
}
