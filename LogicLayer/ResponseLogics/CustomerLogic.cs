using DataAccessLayer.Functions.Interfaces;
using DataAccessLayer.ViewModels.RequestVM;
using DataAccessLayer.ViewModels.ResponseVM;

namespace LogicLayer.ResponseLogics
{
    public class CustomerLogic
    {
        private ICustomerFunction _customerFunctions = new DataAccessLayer.Functions.CustomerFunction();
        public async Task<string> add(CustomerAddRequestVM requestVM)
        {
            try
            {
                var response = await _customerFunctions.addCustomer(requestVM);
                return response.Id;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CustomerLogic: add FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<bool> update(string id, CustomerUpdateRequestVM requestVM)
        {
            try
            {
                var response = await _customerFunctions.updateCustomer(id, requestVM);
                if (response == null)
                {
                    throw new Exception($"\nLogicLayer: CustomerLogic: update FAILED. ID: {id}");
                }
                return true;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CustomerLogic: update FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<List<CustomerGetAllResponseVM>> getAll()
        {
            try
            {
                var response = await _customerFunctions.getAllCustomers();
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CustomerLogic: getAll FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<bool> delete(string id)
        {
            try
            {
                var response = await _customerFunctions.deleteCustomer(id);
                if (response == null)
                {
                    throw new Exception($"\nLogicLayer: CustomerLogic: delete FAILED. ID: {id}");
                }
                return true;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CustomerLogic: delete FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        public async Task<CustomerGetResponseVM> get(string id)
        {
            try
            {
                var response = await _customerFunctions.getCustomer(id);
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CustomerLogic: get FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
