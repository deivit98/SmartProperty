using Microsoft.OpenApi;
using SmartProperty.AI.Worker.Consumers;
using SmartProperty.AI.Worker.Services;
using SmartProperty.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddOllamaApiClient("embeddings").AddEmbeddingGenerator();
builder.AddQdrantClient("qdrant-vectorDB");

builder.AddSmartPropertyKafkaConsumer<PropertyCreatedMessage>("kafka", "smartproperty-ai");
builder.Services.AddScoped<IPropertyVectorizationService, PropertyVectorizationService>();
builder.Services.AddHostedService<PropertyCreatedConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartProperty AI API",
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartProperty AI API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/api/properties/search", async (string? q, IPropertyVectorizationService vectorizationService, CancellationToken cancellationToken) =>
{
    var query = q?.Trim();
    if (string.IsNullOrEmpty(query))
        return Results.BadRequest("Query parameter 'q' is required.");
    var match = await vectorizationService.SearchAsync(query, limit: 1, cancellationToken);
    return match is null ? Results.NotFound() : Results.Ok(match);
})
.WithName("SearchProperties")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);

app.Run();
