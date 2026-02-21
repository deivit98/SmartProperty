var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var vectorDB = builder.AddQdrant("vectordb")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var postregres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var apiApp = builder.AddProject<Projects.SmartProperty_API>("api");

apiApp
    .WithReference(chat)
    .WaitFor(chat)
    .WithReference(embeddings)
    .WaitFor(embeddings)
    .WithReference(vectorDB)
    .WaitFor(vectorDB)
    .WithReference(postregres)
    .WaitFor(postregres);

builder.Build().Run();
