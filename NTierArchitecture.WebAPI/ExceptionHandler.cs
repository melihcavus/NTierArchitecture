using Microsoft.AspNetCore.Diagnostics;
using NTierArchitecture.WebAPI.Fliters;
using TS.Result;


namespace NTierArchitecture.WebAPI
{
    public sealed class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationExceptionEx validationException)
            {
                var errors =Result<string>.Failure(validationException.Errors.SelectMany(s => s.Value).ToList());
                httpContext.Response.StatusCode = 422;
                await httpContext.Response.WriteAsJsonAsync(errors);
                return true;
            }
            var res = Result<string>.Failure(exception.Message);
            httpContext.Response.StatusCode = 500;

            await httpContext.Response.WriteAsJsonAsync(res);
            return true;
        }
    }
}
