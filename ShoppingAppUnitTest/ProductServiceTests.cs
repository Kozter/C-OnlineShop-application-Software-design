using Moq;
using OnlineShoppApp.Data;
using OnlineShoppApp.Service;
using Xunit;

namespace OnlineShoppApp.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task CreateAsync_ValidProduct_Success()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Category = "Test Category",
                Description = "Test Description",
                Price = 19.99m
            };

            // Act
            await productService.CreateAsync(product);

            // Assert
            productRepositoryMock.Verify(repo => repo.AddProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task GetProductAsync_ExistingProductId_ReturnsProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            var productId = 1;
            var expectedProduct = new Product
            {
                Id = productId,
                Name = "Test Product",
                Category = "Test Category",
                Description = "Test Description",
                Price = 19.99m
            };

            productRepositoryMock.Setup(repo => repo.GetProductAsync(productId))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await productService.GetProductAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            // Add more assertions based on your product properties
        }

        [Fact]
        public async Task GetProductsListAsync_ReturnsCorrectNumberOfProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Category = "Category1", Price = 10.0m },
                new Product { Id = 2, Name = "Product2", Category = "Category2", Price = 15.0m }
                // Add more sample products as needed
            };

            productRepositoryMock.Setup(repo => repo.GetProductsAsync())
                .ReturnsAsync(expectedProducts);

            // Act
            var actualProducts = await productRepositoryMock.Object.GetProductsAsync();

            // Assert
            productRepositoryMock.Verify(repo => repo.GetProductsAsync(), Times.Once);
            Assert.NotNull(actualProducts); // Ensure the returned list is not null
            Assert.Equal(expectedProducts.Count, actualProducts.Count); // Assert the count of products
        }


    }
}
