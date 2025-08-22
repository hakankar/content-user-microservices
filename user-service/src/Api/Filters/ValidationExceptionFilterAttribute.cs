using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ValidationExceptionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var firstError = context.ModelState
                    .Where(ms => ms.Value!= null && ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Error = ms.Value!.Errors.First().ErrorMessage
                    })
                    .FirstOrDefault();

                if (firstError != null)
                {
                    throw new CustomException(firstError.Error, ExceptionType.ValidationError);
                }
            }
        }
    }
}
