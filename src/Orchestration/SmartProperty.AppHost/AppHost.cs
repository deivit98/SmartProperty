using SmartProperty.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var qdrant = builder.AddQdrant("qdrant")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var kafka = builder.AddKafka("kafka")
    .WithDataVolume(isReadOnly: false)
    .WithKafkaUI()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var db = postgres.AddDatabase("smartpropertydb");

var apiWebApp = builder.AddProject<Projects.SmartProperty_API>("smartproperty-api");
var aiWebApp = builder.AddProject<Projects.SmartProperty_AI_Web>("smartproperty-ai-web");

apiWebApp
    .WaitWithReference(kafka)
    .WaitWithReference(postgres, db);

aiWebApp
    .WaitFor(ollama)
    .WaitWithReference(chat)
    .WaitWithReference(embeddings)
    .WaitWithReference(qdrant)
    .WaitWithReference(kafka);


builder.Build().Run();