using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface ICloudinary
    {
         Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> AddPhotoAsync(IFormFile File);
    }
}