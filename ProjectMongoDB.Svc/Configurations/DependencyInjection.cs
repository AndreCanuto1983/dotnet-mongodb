using ProjectmongoDB.Application.Interfaces;
using ProjectmongoDB.Infra.Repository;

namespace ProjectMongoDB.Svc.Configurations
{
    public static class DependencyInjection
    {
        public static void Configurations(IServiceCollection services)
        {
            services.AddScoped<IProjectMongoDBRepository, ProjectMongoDBRepository>();
        }
    }
}
