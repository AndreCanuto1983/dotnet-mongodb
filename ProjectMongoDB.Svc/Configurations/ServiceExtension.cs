﻿using System.Text.Json.Serialization;

namespace ProjectMongoDB.Svc.Configurations
{
    public static class ServiceExtension
    {
        public static void ServiceExtensionSettings(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    });                    
        }
    }
}
