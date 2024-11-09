using ETicaretServer.Application.Repositories;
using MediatR;
using P = ETicaretServer.Domain.Entities;
namespace ETicaretServer.Application.Features.Queries.Product.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository repository)
        {
            _productReadRepository = repository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            { 
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };
        }
    }
}
