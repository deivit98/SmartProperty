using SmartProperty.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder
    .AddOllama("ollama")
    .WithDataVolume()
    .WithGPUSupport()
    .WithOpenWebUI();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var qdrant = builder
    .AddQdrant("qdrant-vectorDB")
    .WithDataVolume();

var kafka = builder
    .AddKafka("kafka")
    .WithDataVolume(isReadOnly: false)
    .WithKafkaUI();

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

var db = postgres.AddDatabase("smartpropertydb");

var s3 = builder
    .AddMinioContainer("s3Storage")
    .WithDataVolume();

var apiApp = builder.AddProject<Projects.SmartProperty_API>("smartproperty-api");
var aiApp = builder.AddProject<Projects.SmartProperty_AI_Web>("smartproperty-ai");
var uiApp = builder.AddProject<Projects.SmartProperty_UI>("smartproperty-ui");

apiApp
    .WaitWithReference(kafka)
    .WaitWithReference(postgres, db)
    .WaitWithReference(s3);

aiApp
    .WaitFor(ollama)
    .WaitWithReference(chat)
    .WaitWithReference(embeddings)
    .WaitWithReference(qdrant)
    .WaitWithReference(kafka)
    .WaitWithReference(s3);

uiApp
     .WaitWithReference(apiApp)
     .WaitWithReference(aiApp);


builder.Build().Run();