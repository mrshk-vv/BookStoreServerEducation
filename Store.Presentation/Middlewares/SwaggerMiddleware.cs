using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Store.Presentation.Middlewares
{
    public class SwaggerMiddleware
    {

        private readonly IApplicationBuilder _app;
        private readonly RequestDelegate _next;

        public SwaggerMiddleware(RequestDelegate next, IApplicationBuilder app)
        {
            _next = next;
            _app = app;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
        }
    }
}
