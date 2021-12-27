using ProjectmongoDB.Application.Model.Configuration;

namespace ProjectMongoDB.Svc.Configurations
{
    public static class MongoDBExtension
    {
        public static void Configurations(WebApplicationBuilder builder)
        {            
            builder.Services.Configure<ConfigurationDB>(builder.Configuration.GetSection("ConfigurationMongoDb"));
        }
    }
}
