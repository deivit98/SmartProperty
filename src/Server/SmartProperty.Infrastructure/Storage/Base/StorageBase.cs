using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;

namespace SmartProperty.Infrastructure.Storage.Base;

public abstract class StorageBase(IMinioClient client, string bucketName) : IStorageBase
{
    protected string BucketName => bucketName;

    public virtual async Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default)
    {
        var exists = await client.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(BucketName),
            cancellationToken).ConfigureAwait(false);

        if (!exists)
        {
            await client.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(BucketName),
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
            .WithBucket(BucketName)
            .WithObject(objectKey)
            .WithStreamData(data)
            .WithObjectSize(size);

        if (!string.IsNullOrEmpty(contentType))
            args = args.WithContentType(contentType);

        await client.PutObjectAsync(args, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task GetObjectAsync(
        string objectKey,
        Stream destination,
        CancellationToken cancellationToken = default)
    {
        await client.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(BucketName)
                .WithObject(objectKey)
                .WithCallbackStream(async (stream, ct) => await stream.CopyToAsync(destination, ct).ConfigureAwait(false)),
            cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task RemoveObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        await client.RemoveObjectAsync(
            new RemoveObjectArgs().WithBucket(BucketName).WithObject(objectKey),
            cancellationToken).ConfigureAwait(false);
    }

    public virtual IAsyncEnumerable<Item> ListObjectsAsync(
        string? prefix = null,
        bool recursive = true,
        CancellationToken cancellationToken = default)
    {
        var args = new ListObjectsArgs()
            .WithBucket(BucketName)
            .WithRecursive(recursive);

        if (!string.IsNullOrEmpty(prefix))
            args = args.WithPrefix(prefix);

        return client.ListObjectsEnumAsync(args, cancellationToken);
    }

    public virtual async Task<bool> ObjectExistsAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await client.StatObjectAsync(
                new StatObjectArgs().WithBucket(BucketName).WithObject(objectKey),
                cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
