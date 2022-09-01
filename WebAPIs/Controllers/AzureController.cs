using DataAccessLayer.Models;
using LogicLayer.ResponseLogics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
        private CognitiveLogic _cognitiveLogic = new CognitiveLogic();

        [HttpGet("startCognitiveService")]
        public async Task<CognitiveEntities> InitializeSearchService()
        {
            try
            {
                CognitiveEntities response = await _cognitiveLogic.initializeSearchService();
                return response;
            }
            catch (Exception ex)
            {
                var ewsExcption = new Exception($"\nWebAPIs: AzureController: InitializeSearchService FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ewsExcption.Message);
            }
        }
    }
}
