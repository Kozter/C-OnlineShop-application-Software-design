using OnlineShoppApp.Data;
using System.Diagnostics;

namespace OnlineShoppApp.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(Product product)
        {
            try
            {
                if (product == null)
                {
                    Console.WriteLine($"Product object is null");
                }
                else
                {
                    await _repository.AddProductAsync( product );
                    Console.WriteLine($"\nProduct added successfully!\n");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Console.WriteLine($"Error occured while trying to persisit the product!: {e.Message}");
            }
        }
        public async Task<Product> GetProductAsync(int Id)
        {
            try
            {
                return await _repository.GetProductAsync( Id );
            }catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task DisplayProductAsync(int Id)
        {
            try
            {
                var product = await _repository.GetProductAsync(Id);
                if (product == null)
                {
                    Debug.WriteLine($"Product with id {Id} not found!");
                }
                else
                {
                    Console.WriteLine($"Product:: [Id: {product.Id}, Name: {product.Name}, Category: {product.Category}, Description: {product.Description}, Unit price: {product.Price}]\n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while getting product with id {Id}");
                Console.WriteLine(e.Message);
            }
        }


        public async Task ListProductsAsync()
        {
            try
            {
                var products = await _repository.GetProductsAsync();
                if (products == null)
                {
                    Console.WriteLine($"Currently there are no products!\n");
                }
                else
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Product:: [Id: {product.Id}, Name: {product.Name}, Category: {product.Category}, Description: {product.Description}, Unit price: {product.Price}]\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching list of products");
                Console.WriteLine(e.Message);
            }
        }
    }
}
