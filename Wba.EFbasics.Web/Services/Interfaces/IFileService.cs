namespace Wba.EFbasics.Web.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> StoreFile(IFormFile file);
    }
}
