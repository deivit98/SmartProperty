using Microsoft.AspNetCore.Mvc;

namespace SmartProperty.Result;

/// <summary>
/// Extension methods to map <see cref="Result{TValue}"/> to <see cref="IActionResult"/>.
/// </summary>
public static class ResultActionResultExtensions
{
    /// <summary>
    /// Maps a result to an action result. Success returns 200 OK with the value; failure returns an appropriate status and body from <see cref="Error.Code"/> and message.
    /// </summary>
    public static IActionResult ToActionResult<TValue>(this Result<TValue> result, ControllerBase controller)
    {
        if (controller == null)
            throw new ArgumentNullException(nameof(controller));

        if (result.IsSuccess)
            return controller.Ok(result.Value);

        var (statusCode, body) = ToStatusAndBody(result.Error);
        return controller.StatusCode(statusCode, body);
    }

    private static (int StatusCode, object Body) ToStatusAndBody(Error error)
    {
        var statusCode = error.Code switch
        {
            ErrorCode.NotFound => 404,
            ErrorCode.EmptyParameter => 400,
            ErrorCode.None => 500,
            _ => 500,
        };
        var body = new { code = error.Code.ToString(), message = error.Message };
        return (statusCode, body);
    }
}

