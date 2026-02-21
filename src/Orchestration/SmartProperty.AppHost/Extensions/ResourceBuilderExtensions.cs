namespace SmartProperty.AppHost.Extensions
{
    public static class ResourceBuilderExtensions
    {
        extension(IResourceBuilder<ProjectResource> builder)
        {
            public IResourceBuilder<ProjectResource> WaitWithReference(IResourceBuilder<OllamaResource> ollama)
            {
                return builder
                    .WithReference(ollama)
                    .WaitFor(ollama);
            }

            public IResourceBuilder<ProjectResource> WaitWithReference(IResourceBuilder<OllamaModelResource> ollamaModel)
            {
                return builder
                    .WithReference(ollamaModel)
                    .WaitFor(ollamaModel);
            }

            public IResourceBuilder<ProjectResource> WaitWithReference(IResourceBuilder<QdrantServerResource> qdrant)
            {
                return builder
                    .WithReference(qdrant)
                    .WaitFor(qdrant);
            }

            public IResourceBuilder<ProjectResource> WaitWithReference(IResourceBuilder<PostgresServerResource> postgres, IResourceBuilder<PostgresDatabaseResource> db)
            {
                return builder
                     .WithReference(postgres)
                     .WithReference(db)
                     .WaitFor(postgres)
                     .WaitFor(db);

            }
        }
    }
}
