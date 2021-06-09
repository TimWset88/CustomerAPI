using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using CustomerAPI;
using CustomerApi.Data;
using CustomerApi.Repository;
using CustomerApi.Models;
using Xunit;

namespace CustomerAPITests.CustomerRepositoryTests
{
    public class CustomerRepositoryTests
    {
        private readonly Mock<CustomerContext> _context;
        private readonly Mock<ICustomerRepository> _customerRepo;
        public CustomerRepositoryTests(Mock<ICustomerRepository> customerRepo, Mock<CustomerContext> context)
        {
            _context = context;
            _customerRepo = customerRepo;
        }

        [Fact]
        public void GetCustomerById_should_return_a_customer_if_id_exists()
        {
            //Arrange
            var customerIdFromDb = 1;
            var customer = new Customer { Id = customerIdFromDb, CustomerId = Guid.NewGuid(), FirstName = "Tom", LastName = "Smith", CustomerType = "Personal" };

            //Act
            var result = _customerRepo.Object.GetCustomerById(customerIdFromDb);
            var expected = customerIdFromDb;

            //Assert
            Assert.Equal(expected, result.Id);
        }


    }
}
