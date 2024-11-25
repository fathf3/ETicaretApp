using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductService _productService;

        public GetAllProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var products = await _productService.GetAllProductsAsync(request.Page, request.Size);

            return new()
            {
                Products = products.Products,
                TotalProductCount = products.TotalProductCount
            };
        }
    }
}
