using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;

namespace SmartProperty.Infrastructure.Storage.Base;

public abstract class StorageBase(IMinioClient client, string bucketName) : IStorageBase
{
    private readonly IMinioClient _client = client;
    private readonly string _bucketName = bucketName;

    protected string BucketName => _bucketName;

    public virtual async Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default)
    {
        var exists = await _client.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(_bucketName),
            cancellationToken).ConfigureAwait(false);

        if (!exists)
        {
            await _client.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(_bucketName),
                cancellationToken).ConfigureAwait(false);
        }
    }

    public virtual async Task PutObjectAsync(
        string objectKey,
        Stream data,
        long size,
        string? contentType = null,
        CancellationToken cancellationToken = default)
    {
        var args = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectKey)
            .WithStreamData(data)
            .WithObjectSize(size);

        if (!string.IsNullOrEmpty(contentType))
            args = args.WithContentType(contentType);

        await _client.PutObjectAsync(args, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task GetObjectAsync(
        string objectKey,
        Stream destination,
        CancellationToken cancellationToken = default)
    {
        await _client.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectKey)
                .WithCallbackStream(async (stream, ct) => await stream.CopyToAsync(destination, ct).ConfigureAwait(false)),
            cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task RemoveObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        await _client.RemoveObjectAsync(
            new RemoveObjectArgs().WithBucket(_bucketName).WithObject(objectKey),
            cancellationToken).ConfigureAwait(false);
    }

    public virtual IAsyncEnumerable<Item> ListObjectsAsync(
        string? prefix = null,
        bool recursive = true,
        CancellationToken cancellationToken = default)
    {
        var args = new ListObjectsArgs()
            .WithBucket(_bucketName)
            .WithRecursive(recursive);

        if (!string.IsNullOrEmpty(prefix))
            args = args.WithPrefix(prefix);

        return _client.ListObjectsEnumAsync(args, cancellationToken);
    }

    public virtual async Task<bool> ObjectExistsAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _client.StatObjectAsync(
                new StatObjectArgs().WithBucket(_bucketName).WithObject(objectKey),
                cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
