using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FPTRewardSystem.API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Kiểm tra xem ngoại lệ bị ném ra có phải là ConflictException không
            if (exception is ConflictException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Conflict",
                    Detail = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true; // Báo hiệu đã xử lý xong lỗi này
            }
            return false;
        }
    }
}
