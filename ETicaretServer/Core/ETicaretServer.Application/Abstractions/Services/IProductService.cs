using ETicaretServer.Application.DTOs.Order;
using ETicaretServer.Application.DTOs.Product;

namespace ETicaretServer.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ListProductDto> GetAllProductsAsync(int page, int size);
        Task<SingleProductDto> GetProdctById(string id);
    }
}
