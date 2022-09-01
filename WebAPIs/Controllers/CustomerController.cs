using DataAccessLayer.ViewModels.RequestVM;
using DataAccessLayer.ViewModels.ResponseVM;
using LogicLayer.ResponseLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CustomerController : ControllerBase
    {
        private CustomerLogic _customerLogic = new CustomerLogic();

        [HttpPost("addCustomer")]
        public async Task<string> Add([FromBody] CustomerAddRequestVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string response = await _customerLogic.add(request);
                    return response;
                }
                throw new Exception($"Invalid request model: {request}");
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nWebAPIs: CustomerController: Add FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        [HttpPut("updateCustomer")]
        public async Task<bool> Update([FromQuery] string id, [FromBody] CustomerUpdateRequestVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool response = await _customerLogic.update(id, request);
                    return response;
                }
                throw new Exception($"Invalid request model: {request}");
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nWebAPIs: CustomerController: Update FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        [HttpGet("getAllCustomer")]
        public async Task<List<CustomerGetAllResponseVM>> GetAll()
        {
            try
            {
                var response = await _customerLogic.getAll();
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nWebAPIs: CustomerController: GetAll FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        [HttpDelete("deleteCustomer")]
        public async Task<bool> Delete([FromQuery] string id)
        {
            try
            {
                bool response = await _customerLogic.delete(id);
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nWebAPIs: CustomerController: Delete FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }

        [HttpGet("getCustomer")]
        public async Task<CustomerGetResponseVM> Get(string id)
        {
            try
            {
                var response = await _customerLogic.get(id);
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nWebAPIs: CustomerController: Get FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}

