using AzureServices.Models;
using DataAccessLayer.ViewModels.RequestVM;
using DataAccessLayer.ViewModels.ResponseVM;
using Microsoft.Azure.Search.Models;
using CustomerEntity = AzureServices.Models.CustomerEntity;

namespace DataAccessLayer.Helpers
{
    public static class CustomerHelper
    {
        public static CustomerEntity ConvertAdd(CustomerAddRequestVM vm)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                var model = new CustomerEntity("Customer", $"Customer--{guid}")
                {
                    Id = guid,
                    Name = vm.Name,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    isActive = true
                };
                return model;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerHelper: ConvertAdd FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public static CustomerEntity ConvertUpdate(CustomerUpdateRequestVM vm, CustomerEntity currentEmp)
        {
            try
            {
                currentEmp.Name = vm.Name;
                currentEmp.Email = vm.Email;
                currentEmp.PhoneNumber = vm.PhoneNumber;

                return currentEmp;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerHelper: ConvertUpdate FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public static List<CustomerGetAllResponseVM> ConvertGetALL(DocumentSearchResult<CustomerEntity> vm)
        {
            try
            {
                var model = new List<CustomerGetAllResponseVM>();

                foreach (SearchResult<CustomerEntity> data in vm.Results)
                {
                    if (data.Document.isActive)
                    {
                        var emp = new CustomerGetAllResponseVM
                        {
                            Id = data.Document.Id,
                            Name = data.Document.Name,
                            Email = data.Document.Email,
                            PhoneNumber = data.Document.PhoneNumber
                        };
                        model.Add(emp);
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerHelper: ConvertGetALL FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public static CustomerEntity ConvertDelete(CustomerEntity vm)
        {
            try
            {
                vm.isActive = false;
                return vm;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerHelper: ConvertDelete FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public static CustomerGetResponseVM ConvertGet(CustomerEntity vm)
        {
            try
            {
                var model = new CustomerGetResponseVM
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                };
                return model;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CustomerHelper: ConvertGet FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}

