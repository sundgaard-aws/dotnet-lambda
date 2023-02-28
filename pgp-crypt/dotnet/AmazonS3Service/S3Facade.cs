using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace PGPDemo.Amazon.S3
{
    public class S3Facade
    {        
        public async Task UploadFile(string bucketName, FileInfo fileToUpload) {
            var s3Client=new AmazonS3Client();
            var putObjectRequest=new PutObjectRequest{
                BucketName=bucketName,
                Key=fileToUpload.Name,
                FilePath=fileToUpload.FullName
            };
            await s3Client.PutObjectAsync(putObjectRequest);
        }
    }
}