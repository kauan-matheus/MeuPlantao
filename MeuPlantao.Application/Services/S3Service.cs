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
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public S3Service(
            IServiceProvider serviceProvider,
            IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _config = config;
        }

        public async Task<string> UploadArquivo(
            IFormFile arquivo,
            string caminho)
        {
            // Resolve IAmazonS3 apenas quando realmente necessário (lazy),
            // evitando crash por falta de credenciais AWS em operações que não usam S3.
            var s3 = _serviceProvider.GetService(typeof(IAmazonS3)) as IAmazonS3;

            if (s3 == null)
                throw new InvalidOperationException(
                    "Serviço AWS S3 não está configurado. Verifique as credenciais AWS no servidor.");

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

            await s3.PutObjectAsync(request);

            return
                $"https://{bucket}.s3.amazonaws.com/{caminho}";
        }
    }
}