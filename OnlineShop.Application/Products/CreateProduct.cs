using OnlineShop.Database;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Products
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(string Name, string Description, decimal Value)
        {
            _context.Products.Add(new Product
            {
                Name = Name,
                Description = Description,
                Value = Value
            });

            await _context.SaveChangesAsync();
        }
    }
}
