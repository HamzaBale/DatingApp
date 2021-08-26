using System.Threading.Tasks;
using API.helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : ICloudinary
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config )
        {
          var acc = new Account(
              config.Value.CloudName,
              config.Value.ApiKey,
              config.Value.ApiSecret
          );
          _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile File)
        {   
            var uploadResult = new ImageUploadResult(); 

            if(File.Length > 0){

                using var stream = File.OpenReadStream();

                var uploadParams = new ImageUploadParams {
                        File = new FileDescription(File.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
        
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }

                
                return uploadResult; 
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParamas = new DeletionParams(publicId);

           var DeleteResult = await _cloudinary.DestroyAsync(deleteParamas);
            return DeleteResult;
        }
    }
}