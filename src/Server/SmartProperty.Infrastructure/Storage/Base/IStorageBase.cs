using Minio.DataModel;

namespace SmartProperty.Infrastructure.Storage.Base;

public interface IStorageBase
{
    Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default);

    Task PutObjectAsync(
        string objectKey,
        Stream data,
        long size,
        string? contentType = null,
        CancellationToken cancellationToken = default);

    Task GetObjectAsync(
        string objectKey,
        Stream destination,
        CancellationToken cancellationToken = default);

    Task RemoveObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<Item> ListObjectsAsync(
        string? prefix = null,
        bool recursive = true,
        CancellationToken cancellationToken = default);

    Task<bool> ObjectExistsAsync(
        string objectKey,
        CancellationToken cancellationToken = default);
}
