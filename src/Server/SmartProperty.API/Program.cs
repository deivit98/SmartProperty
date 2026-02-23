using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SmartProperty.Application.Producers;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Kafka;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

       
builder.AddMinioClient("s3Storage");
builder.AddSmartPropertyKafkaProducer<PropertyCreatedMessage>();
builder.Services.AddSingleton<IPropertyProducer, PropertyProducer>();
builder.Services.AddSingleton<AuditTimestampInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.AddInterceptors(sp.GetRequiredService<AuditTimestampInterceptor>());
    options.UseNpgsql(builder.Configuration.GetConnectionString("smartpropertydb"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

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
    //await db.Database.EnsureDeletedAsync();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
