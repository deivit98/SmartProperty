using SmartProperty.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var vectorDB = builder.AddQdrant("vectordb")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var db = postgres.AddDatabase("smartpropertydb");

var apiApp = builder.AddProject<Projects.SmartProperty_API>("api");

apiApp
    .WaitWithReference(chat)
    .WaitWithReference(embeddings)
    .WaitWithReference(vectorDB)
    .WaitWithReference(postgres, db);

builder.Build().Run();