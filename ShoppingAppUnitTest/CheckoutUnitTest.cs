using Moq;
using OnlineShoppApp.Models;
using OnlineShoppApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAppUnit.Test
{
    public class CheckoutUnitTest
    {
        public class ShoppingCartServiceTests
        {
            [Fact]
            public async Task Checkout_Successful()
            {
                // Arrange
                var orderServiceMock = new Mock<IOrderService>();
                var orderLineServiceMock = new Mock<IOrderLineService>();

                var shoppingCartService = new ShoppingCartService(orderServiceMock.Object, orderLineServiceMock.Object);

                var cartItems = new List<CartItem>
                {
                    new CartItem { ProductId = 1, ProductName = "Product1", Quantity = 2, CustomerId = 1 },
                    new CartItem { ProductId = 2, ProductName = "Product2", Quantity = 1, CustomerId = 1 }
                };

                // Set up mock behavior for order service
                orderServiceMock.Setup(os => os.CreateAsync(It.IsAny<Order>()))
                    .Returns(Task.CompletedTask);

                orderServiceMock.Setup(os => os.GetOrderByCustomerAndDateAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                    .ReturnsAsync(new Order { Id = 1 });

                // Act
                await shoppingCartService.Checkout(cartItems);

                // Assert
                orderServiceMock.Verify(os => os.CreateAsync(It.IsAny<Order>()), Times.Once);
                orderServiceMock.Verify(os => os.GetOrderByCustomerAndDateAsync(It.IsAny<int>(), It.IsAny<DateTime>()), Times.Once);
                // Add more assertions if needed
            }
        }
    }
}

