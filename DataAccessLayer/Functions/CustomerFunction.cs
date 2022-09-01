using AzureServices.Models;
using DataAccessLayer.Functions.Interfaces;
using CustomerHelper = DataAccessLayer.Helpers.CustomerHelper;
using DataAccessLayer.ViewModels.RequestVM;
using DataAccessLayer.ViewModels.ResponseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.Azure.Cosmos.Table;
using AzureServices.StorageTable;

namespace DataAccessLayer.Functions
{
    public class CustomerFunction : ICustomerFunction
    {
        public async Task<CustomerEntity> addCustomer(CustomerAddRequestVM requestVM)
        {
            try
            {
                StorageTableFunctions storageTableFunctions = new StorageTableFunctions();

                var data = CustomerHelper.ConvertAdd(requestVM);

                var result = await storageTableFunctions.AddDataToTable(data);
                return result;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerFunction: addCustomer FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerEntity> deleteCustomer(string id)
        {
            try
            {
                StorageTableFunctions storageTableFunctions = new StorageTableFunctions();

                var data = await storageTableFunctions.GetTableData(id);
                data = CustomerHelper.ConvertDelete(data);

                var result = await storageTableFunctions.UpdateDataInTable(data);
                return result;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerFunction: deleteCustomer FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<List<CustomerGetAllResponseVM>> getAllCustomers()
        {
            try
            {
                StorageTableFunctions storageTableFunctions = new StorageTableFunctions();

                var data = await storageTableFunctions.GetAllTableData();

                var result = CustomerHelper.ConvertGetALL(data);
                return result;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerFunction: getAllCustomers FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerGetResponseVM> getCustomer(string id)
        {
            try
            {
                StorageTableFunctions storageTableFunctions = new StorageTableFunctions();

                var data = await storageTableFunctions.GetTableData(id);

                var result = CustomerHelper.ConvertGet(data);
                return result;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerFunction: getCustomer FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerEntity> updateCustomer(string id, CustomerUpdateRequestVM requestVM)
        {
            try
            {
                StorageTableFunctions storageTableFunctions = new StorageTableFunctions();

                var data = await storageTableFunctions.GetTableData(id);
                data = CustomerHelper.ConvertUpdate(requestVM, data);

                var result = await storageTableFunctions.UpdateDataInTable(data);
                return result;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerFunction: updateCustomer FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
