using ETicaretServer.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet] 
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
              new() { Id= Guid.NewGuid(), Name="Product 1", Price=100, CreatedDate=DateTime.UtcNow, Stock = 10 },
              new() { Id= Guid.NewGuid(), Name="Product 2", Price=100, CreatedDate=DateTime.UtcNow, Stock = 100 },
              new() { Id= Guid.NewGuid(), Name="Product 3", Price=100, CreatedDate=DateTime.UtcNow, Stock = 1000 },
              new() { Id= Guid.NewGuid(), Name="Product 4", Price=100, CreatedDate=DateTime.UtcNow, Stock = 1000 },
            });
            await _productWriteRepository.SaveAsync();
        }
    }
}
