using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace City_Vibe.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
    }
}
