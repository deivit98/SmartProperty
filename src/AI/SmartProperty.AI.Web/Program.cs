using SmartProperty.AI.Worker.Consumers;
using SmartProperty.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSmartPropertyKafkaConsumer<PropertyCreatedMessage>("kafka", "smartproperty-ai");
builder.Services.AddHostedService<PropertyCreatedConsumer>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
