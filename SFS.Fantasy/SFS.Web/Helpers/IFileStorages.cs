namespace SFS.Web.Helpers
{
    public interface IFileStorages
    {
        Task<string> SaveFileAsync(byte[] content, string extention, string containerName);

        Task RemoveFileAsync(string path, string containerName);
    }
}