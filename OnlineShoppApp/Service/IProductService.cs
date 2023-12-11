using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Service
{
    public interface IProductService
    {
        public Task ListProductsAsync();
        public Task DisplayProductAsync(int Id);
        public Task<Product> GetProductAsync(int Id);
        public Task CreateAsync(Product product);
    }
}
