var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var vectorDB = builder.AddQdrant("vectordb")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var apiApp = builder.AddProject<Projects.SmartProperty_API>("api");

var clientApp = builder.AddProject<Projects.SmartProperty_UI>("client");

builder.Build().Run();
