using Domain.Enums;

namespace Api.Common
{
    public static class ExceptionTypeMapper
    {
        public static int ToStatusCode(this ExceptionType type)
        {
            return type switch
            {
                ExceptionType.ValidationError => StatusCodes.Status422UnprocessableEntity,
                ExceptionType.NotFound => StatusCodes.Status404NotFound,
                ExceptionType.Unauthorized => StatusCodes.Status401Unauthorized,
                ExceptionType.Forbidden => StatusCodes.Status403Forbidden,
                ExceptionType.Conflict => StatusCodes.Status409Conflict,
                ExceptionType.BadRequest => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
