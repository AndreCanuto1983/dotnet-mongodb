using ProjectmongoDB.Application.Interfaces;
using ProjectmongoDB.Infra.Repository;

namespace ProjectMongoDB.Svc.Configurations
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionSettings(this IServiceCollection services)
        {
            services.AddScoped<IProjectMongoDBRepository, ProjectMongoDBRepository>();
        }
    }
}
