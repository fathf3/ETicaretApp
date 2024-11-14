using ETicaretServer.Application.ViewModels.Baskets;
using ETicaretServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VM_Create_BasketItem model);
        public Task UpdateQuantityAsync(VM_Update_BasketItems model);
        public Task RemoveBasketItemsAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}
