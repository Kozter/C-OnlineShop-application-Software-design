using Microsoft.EntityFrameworkCore;
using OnlineShoppApp.Data;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class ProductRepository: IProductRepository
{
    private readonly ShoppingContext _context;

    public ProductRepository(ShoppingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddProductAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct != null)
        {
            // Map the updated values to the existing product entity
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
        }
        else
        {
            Console.WriteLine($"Product with id {product.Id} not found!");
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        else
        {
            Debug.WriteLine($"Product with id {productId} not found!");
        }
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }
    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
}
