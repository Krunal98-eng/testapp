using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testProject.Models;

namespace testProject.Repositories
{
    public interface ICustomerRepo
    {
        //int GetEnumValue(string Type);
        List<CustomerModel> ValidateLogin(CustomerModel customer);
        List<CustomerModel> GetAllCustomers();
        List<CustomerModel> ValidateEmail(string Email);
        CustomerModel CreateCustomer(CustomerModel customer);
        CustomerModel GetCustomerById(string CustomerId);
        bool CreateMenuAccess(MenuAccessModel menu);
        void DeleteCustomer(string email);
    }
}
