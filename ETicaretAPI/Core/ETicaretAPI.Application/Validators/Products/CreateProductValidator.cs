using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;


namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Lütfen ürün adını boş geçmeyiniz")
                .MaximumLength(150).MinimumLength(4).WithMessage("Lütfen ürün adını 4 ile 100 karakter arasında giriniz");

            RuleFor(p => p.Stock).NotEmpty().NotNull().WithMessage("Lütfen Stok bilgisini boş geçmeyiniz boş geçmeyiniz")
                .Must(s => s >= 0).WithMessage("Stock bilgisi negatif olamaz!");

            RuleFor(p => p.Price).NotEmpty().NotNull().WithMessage("Lütfen Fiyat bilgisini boş geçmeyiniz boş geçmeyiniz")
              .Must(s => s >= 0).WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
