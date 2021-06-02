using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        Customer PatchCustomer(Customer customer, int id);
    }
}
