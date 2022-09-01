using AzureServices.Models;
using DataAccessLayer.ViewModels.RequestVM;
using DataAccessLayer.ViewModels.ResponseVM;

namespace DataAccessLayer.Functions.Interfaces
{
    public interface ICustomerFunction
    {
        Task<CustomerEntity> addCustomer(CustomerAddRequestVM requestVM);
        Task<CustomerEntity> updateCustomer(string id, CustomerUpdateRequestVM requestVM);
        Task<CustomerGetResponseVM> getCustomer(string id);
        Task<List<CustomerGetAllResponseVM>> getAllCustomers();
        Task<CustomerEntity> deleteCustomer(string id);
    }
}
