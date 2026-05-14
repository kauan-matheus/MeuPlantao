using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MeuPlantao.Application.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3;
        private readonly IConfiguration _config;

        public S3Service(
            IAmazonS3 s3,
            IConfiguration config)
        {
            _s3 = s3;
            _config = config;
        }

        public async Task<string> UploadArquivo(
            IFormFile arquivo,
            string caminho)
        {
            var bucket =
                _config["AWS:BucketName"];

            using var stream =
                arquivo.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = bucket,
                Key = caminho,
                InputStream = stream,
                ContentType = arquivo.ContentType
            };

            await _s3.PutObjectAsync(request);

            return
                $"https://{bucket}.s3.amazonaws.com/{caminho}";
        }
    }
}