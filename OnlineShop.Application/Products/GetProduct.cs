using OnlineShop.Database;

namespace OnlineShop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _ctx;
        public GetProduct(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Do()
        {

        }
    }

    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
