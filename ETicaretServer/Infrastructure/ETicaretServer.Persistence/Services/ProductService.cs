using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.Order;
using ETicaretServer.Application.DTOs.Product;
using ETicaretServer.Application.Repositories;
using ETicaretServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;

        public ProductService(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<ListProductDto> GetAllProductsAsync(int page, int size)
        {
            var products =  _productReadRepository.GetAll(false).Skip(page * size).Take(size)
            .Include(p => p.ProductImageFiles)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                images = p.ProductImageFiles.Select(bi => new
                {
                    bi.Path,
                    bi.Showcase
                })

            }).ToList();
            return new()
            {
                TotalProductCount = await _productReadRepository.GetCount(),
                Products = products

            };
        }

        public async Task<SingleProductDto> GetProdctById(string id)
        {
            var product = await _productReadRepository.Table
               .Include(p => p.ProductImageFiles)
               .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new SingleProductDto
            {
                Images = product.ProductImageFiles.Select(i => new
                {
                    i.Path,
                    i.Showcase,

                }),
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };

        }
    }
}
