using Microsoft.AspNetCore.Builder;

namespace WebApiFundamentos.Middlewares
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UserLogResponse(this IApplicationBuilder app)
        {
            app.UseMiddleware<RespuestaLogMiddleware>();

            return app;
        }
    }
}
