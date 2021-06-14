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
        private readonly Mock<ICustomerRepository> _repository = new Mock<ICustomerRepository>();

        [Fact]
        public void GetCustomerById_should_return_a_customer_if_id_exists()
        {
            //Arrange
            var customerIdFromDb = 1;
            var customer = new Customer { Id = customerIdFromDb, CustomerId = Guid.NewGuid(), FirstName = "Tom", LastName = "Smith", CustomerType = "Personal" };
            _repository.Setup(x => x.GetCustomerById(customerIdFromDb))
                .Returns(customer);

            //Act
            var result = _repository.Object.GetCustomerById(customerIdFromDb);
            var expected = customer.Id;

            //Assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public void GetAllCustomers_should_return_all_customers()
        {
            //Arrange
            var customers = new List<Customer>() {
                new Customer(){ Id = 1, FirstName="Bill"},
                new Customer(){ Id = 2, FirstName="Steve"}
            };

            _repository.Setup(x => x.GetAllCustomers())
                .Returns(customers);

            //Act
            var result = _repository.Object.GetAllCustomers();
            var expected = customers;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddCustomer_should_add_a_new_customer_if_all_required_fileds_are_populated()
        {
            //Arrange
            var newCustomer = new Customer { Id = 1, FirstName = "Bill", LastName = "Williamson", CustomerType = "Personal" };
            _repository.Setup(x => x.AddCustomer(newCustomer))
                .Returns(newCustomer);

            //Act
            var expected = newCustomer;
            var result = _repository.Object.AddCustomer(newCustomer);

            //Assert
            Assert.Equal(expected, result);
        }

        //[Fact]
        //public void AddCustomer_should_return_a_bad_request_if_customer_object_is_null()
        //{
        //    //Arrange
        //    var newCustomer = new Customer { Id = 1, FirstName = "Bill", LastName = "Williamson", CustomerType = "Personal" };
        //    _repository.Setup(x => x.AddCustomer(null))
        //        .Returns(_repository.Object);

        //    //Act
        //    var expected = new ArgumentNullException(nameof(newCustomer));
        //    var result =_repository.Object.AddCustomer(null);

        //    //Assert
        //    Assert.Equal(expected, result);

        //}

        [Fact]
        public void UpdateCustomer_should_update_an_existing_customer_if_the_customer_exists()
        {
            //Arrange
            var customerToUpdate = new Customer { Id = 2, FirstName = "Bill", LastName = "Williamson", CustomerType = "Personal" };
            _repository.Setup(x => x.UpdateCustomer(customerToUpdate))
                .Returns(customerToUpdate);

            //Act
            var expected = customerToUpdate;
            var result = _repository.Object.UpdateCustomer(customerToUpdate);

            //Assert
            Assert.Equal(expected, result);
        }


    }
}
