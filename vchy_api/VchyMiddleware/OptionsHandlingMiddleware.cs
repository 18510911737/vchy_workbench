using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VchyMiddleware
{
    public class OptionsHandlingMiddleware
    {
        public readonly RequestDelegate _next;

        public OptionsHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);
            responseBody.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(responseBody).ReadToEnd();
            var body = JsonConvert.DeserializeObject<dynamic>(json);
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBody);
        }
    }
}
