using ApiProjeKampi.WebApi.Entities;
using FluentValidation;

namespace ApiProjeKampi.WebApi.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün Adını Boş Geçmeyin");
            RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("En az 2 Karakter veri girişi yapın!");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("En fazla 50 karakter veri girişi yapın!");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün Fiyatı Boş Geçilemez").GreaterThan(0).WithMessage("Ürün fiyatı negatif olamaz").LessThan(1000).WithMessage("Ürün Fiyatı bu kadar yüksek olamaz, girdiğiniz değeri kontrol edin");

            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün Açıklamaları Boş Geçilemez!");

        }
    }
}
