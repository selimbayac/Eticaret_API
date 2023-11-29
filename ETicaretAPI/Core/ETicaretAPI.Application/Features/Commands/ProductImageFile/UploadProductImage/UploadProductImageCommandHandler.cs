using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest,
        UploadProductImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository ;
        public UploadProductImageCommandHandler(IStorageService storageService, IStorage storage, IProductImageFileWriteRepository productImageFileReadRepository   )
        {
            _productImageFileWriteRepository = productImageFileReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileReadRepository;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-image",
                request.Files);
           Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
           await  _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new
          Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
