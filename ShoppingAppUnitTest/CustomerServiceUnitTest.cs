using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using OnlineShoppApp.Data;
using OnlineShoppApp.Models;
using OnlineShoppApp.Service;
using Xunit;

namespace OnlineShoppApp.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomer_ValidCustomer_Success()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(customerRepositoryMock.Object);
            var customer = new Customer { Name = "John Doe", Email = "john@example.com" };

            // Act
            await customerService.CreateCustomer(customer);

            // Assert
            customerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomerAsync_ExistingCustomerId_ReturnsCustomer()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(customerRepositoryMock.Object);
            var customerId = 1;
            var expectedCustomer = new Customer { Id = customerId, Name = "John Doe", Email = "john@example.com" };

            customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customerId))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await customerService.GetCustomerAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer, result);
        }

        [Fact]
        public async Task ListCustomersAsync_ReturnsListOfCustomers()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(customerRepositoryMock.Object);
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Customer { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };

            customerRepositoryMock.Setup(repo => repo.GetCustomersAsync())
                .ReturnsAsync(customers);

            // Act
            await customerService.ListCustomersAsync();

            // Assert
            customerRepositoryMock.Verify(repo => repo.GetCustomersAsync(), Times.Once);
            Assert.Equal(2, customers.Count); // Assert that the number of customers is 2
        }
    }
}
