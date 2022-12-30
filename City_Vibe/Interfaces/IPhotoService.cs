using CloudinaryDotNet.Actions;

namespace City_Vibe.Interfaces
{
    public interface IPhotoService 
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
    }
}
