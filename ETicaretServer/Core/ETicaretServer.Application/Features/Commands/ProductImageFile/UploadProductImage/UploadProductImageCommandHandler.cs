using ETicaretServer.Application.Abstractions.Storage;
using ETicaretServer.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IStorageService _storageService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IConfiguration _configuration;


        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IStorageService storage, IProductImageFileWriteRepository productImageFileWriteRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _storageService = storage;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _configuration = configuration;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);


            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("files", request.Files);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(d => new Domain.Entities.ProductImageFile
            {
                FileName = d.fileName,
                // Azure icin ayarlanmistir!
                Path = _configuration["BaseStorageUrl"]+ d.pathOrContainerName,
                //Path =  d.pathOrContainerName, // -> local icin bu sekikde wwwroot altine files olarak kayit eder
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }

            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
