using ETicaretServer.Application.Features.Commands.Product.CreateProduct;
using ETicaretServer.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lutfen urun adını bos gecmeyiniz!")
                .MaximumLength(250)
                .MinimumLength(2)
                    .WithMessage("Urun adı 2 ile 250 karakter olmalı");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lutfen stok bilginisi bos gecmeyiniz!")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif  değer olamaz");
            
            RuleFor(p => p.Price)
               .NotEmpty()
               .NotNull()
                   .WithMessage("Lutfen fiyat bilginisi bos gecmeyiniz!")
               .Must(s => s >= 0)
                   .WithMessage("Fiyat bilgisi negatif  değer olamaz");
        }

    }
}
