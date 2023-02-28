using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.S3
{
    public class AmazonS3Service : IObjectStoreService
    {        
        public async Task UploadObjectAsync(string path, FileInfo fileToUpload) {
            var s3Client=new AmazonS3Client();
            var putObjectRequest=new PutObjectRequest{
                BucketName=path,
                Key=fileToUpload.Name,
                FilePath=fileToUpload.FullName
            };
            await s3Client.PutObjectAsync(putObjectRequest);
        }
    }
}