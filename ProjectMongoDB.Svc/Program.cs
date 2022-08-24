using ProjectMongoDB.Svc.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.MongoDBSettings();
builder.Services.DependencyInjectionSettings();
builder.Services.ServiceExtensionSettings();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseAuthorization();
app.MapControllers();
app.Run();
