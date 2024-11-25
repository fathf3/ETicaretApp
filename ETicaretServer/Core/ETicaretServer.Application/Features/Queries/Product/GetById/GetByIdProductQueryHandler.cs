using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ETicaretServer.Domain.Entities;
namespace ETicaretServer.Application.Features.Queries.Product.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductService _productService;

        public GetByIdProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {

            var result = await _productService.GetProdctById(request.Id);
            return new()
            {
                Product = result,
            };
           
        }
    }
}
