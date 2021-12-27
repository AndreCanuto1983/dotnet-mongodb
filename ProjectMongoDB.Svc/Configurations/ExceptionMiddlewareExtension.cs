using ProjectMongoDB.Svc.Configurations.Services;

namespace ProjectMongoDB.Svc.Configurations
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
