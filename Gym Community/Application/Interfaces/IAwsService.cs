namespace Gym_Community.Application.Interfaces
{
    public interface IAwsService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string fileKey);
    }
}
