using Amazon.S3.Transfer;
using Amazon.S3;
using Gym_Community.Application.Interfaces;

namespace Gym_Community.Application.Services
{
    public class AwsService:IAwsService
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        public AwsService(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            _s3Client = s3Client;
        }

        //products
        //ProfileImages
        //categories

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fileKey = $"{folderName}/{fileName}";

            using var newMemoryStream = new MemoryStream();
            await file.CopyToAsync(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = fileKey,
                BucketName = _configuration["AWS:BucketName"],
                ContentType = file.ContentType,
                //CannedACL = S3CannedACL.PublicRead 
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            string fileUrl = $"https://{_configuration["AWS:BucketName"]}.s3.amazonaws.com/{fileKey}";
            return fileUrl;
        }

        public async Task<bool> DeleteFileAsync(string fileKey)
        {
            try
            {
                var deleteObjectRequest = new Amazon.S3.Model.DeleteObjectRequest
                {
                    BucketName = _configuration["AWS:BucketName"],
                    Key = fileKey
                };

                await _s3Client.DeleteObjectAsync(deleteObjectRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
