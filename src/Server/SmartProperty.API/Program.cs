using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SmartProperty.Application.Producers;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("smartpropertydb");
builder.AddMinioClient("s3Storage");
builder.AddSmartPropertyKafkaProducer<PropertyCreatedMessage>();
builder.Services.AddSingleton<IPropertyProducer, PropertyProducer>();

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SmartProperty.Application.ApplicationAssembly).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartProperty API",
        Version = "v1"
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1);
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartProperty API v1");
    });

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
