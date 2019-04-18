using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VchyMiddleware;

namespace vchy_api.Middleware
{
    public class ErrorHandling: ErrorHandlingMiddleware
    {
        public ErrorHandling(RequestDelegate requestDelegate):base(requestDelegate)
        {
        }
        public override Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //var code = HttpStatusCode.InternalServerError;
            //var result = JsonConvert.SerializeObject(new { error = exception.Message });
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)code;
            //return context.Response.WriteAsync(result);
            return base.HandleExceptionAsync(context, exception);
        }
    }
}
