using Microsoft.AspNetCore.Http;

namespace TemplateApi.Web;

internal static class ExceptionMapper
{
    public static int MapToStatusCode(this Exception exception)
    {
        switch (exception)
        {
            case ArgumentException:
                return StatusCodes.Status400BadRequest;
            case KeyNotFoundException:
                return StatusCodes.Status404NotFound;
            default:
                return StatusCodes.Status500InternalServerError;
        }
    }
}
