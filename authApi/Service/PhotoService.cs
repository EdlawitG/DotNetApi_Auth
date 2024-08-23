using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using authApi.Infrastructure;
using authApi.Interface;

namespace authApi.Service
{
    public class PhotoService(Cloudinary cloudinary) : IPhoto
    {
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task<PhotoUploadResult> AddPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file provided");
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams) ?? throw new Exception("Upload result is null.");
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error uploading photo: {uploadResult.Error?.Message}");
            }

            return new PhotoUploadResult
            {
                PhotoId = uploadResult.PublicId,
                PhotoUrl = uploadResult.SecureUrl?.ToString()
            };
           
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deleteParams);
            return deletionResult;
        }
    }
}
