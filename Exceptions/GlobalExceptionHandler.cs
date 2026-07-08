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
            if (exception is NotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not found",
                    Detail = exception.Message
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true; // Báo hiệu đã xử lý xong lỗi này
            }
            if (exception is AppValidationException validationEx)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Lỗi dữ liệu đầu vào (Validation Error)",
                    Detail = validationEx.Message
                };
                // Đính kèm bảng băm lỗi vào Extensions để trả về cho Client
                problemDetails.Extensions.Add("errors", validationEx.Errors);
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true; // Báo hiệu đã xử lý xong lỗi này
            }

            if (exception is BadRequestException badRequestEx)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = badRequestEx.Message
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true; // Báo hiệu đã xử lý xong lỗi này
            }
            return false;
        }
    }
}
