using CustomerApi.Models;
using CustomerApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public Customer AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            customer.CustomerId = Guid.NewGuid();
             _context.Customer.Add(customer);

            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customer.FirstOrDefault(item => item.Id == id);
        }

        public Customer PatchCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer PatchCustomer(Customer customer, int id)
        {
            throw new NotImplementedException();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            return _context.Customer.FirstOrDefault();
        }

    }
}
