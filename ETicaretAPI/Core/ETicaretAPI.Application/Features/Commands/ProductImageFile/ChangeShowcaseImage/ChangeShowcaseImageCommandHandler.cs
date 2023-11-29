using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : MediatR.IRequestHandler<ChangeShowcaseImageCommandRequest,
        ChangeShowcaseImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepositor)
        {
            _productImageFileWriteRepository = productImageFileWriteRepositor;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var quary = _productImageFileWriteRepository.Table
                   .Include(p => p.Products)
                   .SelectMany(p => p.Products, (pif, p) => new
                   {
                       pif,
                       p
                   });


            var data = await quary.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

          
            if (data != null)
            {
                data.pif.Showcase = false;
            }
         var image=await quary.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            if (data != null)
                image.pif.Showcase = true;

         await   _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
