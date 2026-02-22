using SmartProperty.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume()
    .WithGPUSupport()
    .WithOpenWebUI();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var qdrant = builder.AddQdrant("qdrant-vectorDB")
    .WithDataVolume();

var kafka = builder.AddKafka("kafka")
    .WithDataVolume(isReadOnly: false)
    .WithKafkaUI();

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

var db = postgres.AddDatabase("smartpropertydb");

var s3 = builder.AddMinioContainer("s3Storage")
    .WithDataVolume();

var apiWebApp = builder.AddProject<Projects.SmartProperty_API>("smartproperty-api");
var aiWebApp = builder.AddProject<Projects.SmartProperty_AI_Web>("smartproperty-ai-web");
var uiWebApp = builder.AddProject<Projects.SmartProperty_UI>("smartproperty-ui-web");

apiWebApp
    .WaitWithReference(kafka)
    .WaitWithReference(postgres, db)
    .WaitWithReference(s3);

aiWebApp
    .WaitFor(ollama)
    .WaitWithReference(chat)
    .WaitWithReference(embeddings)
    .WaitWithReference(qdrant)
    .WaitWithReference(kafka)
    .WaitWithReference(s3);

uiWebApp
     .WaitWithReference(apiWebApp)
     .WaitWithReference(aiWebApp);


builder.AddProject<Projects.SmartProperty_UI>("smartproperty-ui");


builder.Build().Run();