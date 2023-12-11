using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Data
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(int productId);
        public Task<Product> GetProductAsync(int productId);
        public Task<ICollection<Product>> GetProductsAsync();
    }
}
