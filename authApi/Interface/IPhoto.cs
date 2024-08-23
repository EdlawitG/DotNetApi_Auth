using authApi.Infrastructure;
using CloudinaryDotNet.Actions;

namespace authApi.Interface
{
    interface IPhoto
    {
        Task<PhotoUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);

    }
}