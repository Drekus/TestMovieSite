using TestMovieSite.Domain.Helpers;
using TestMovieSite.DTO;

namespace TestMovieSite.Domain.Storage
{
    public interface IStorage
    {
        int Id { get; }
        StorageType Type { get; }
        bool IsDefault { get; }
        string StoragePath { get; }

        OperationResult<byte[]> TryDownloadFile(string filePath);
        OperationResult<string> TryUploadFile(FileDto fileDto);
    }
}