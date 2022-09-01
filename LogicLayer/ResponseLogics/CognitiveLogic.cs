using DataAccessLayer.Functions.Interfaces;
using DataAccessLayer.Models;

namespace LogicLayer.ResponseLogics
{
    public class CognitiveLogic
    {
        private ICognitiveFunctions _cognitiveFunctions = new DataAccessLayer.Functions.CognitiveFunctions();

        public async Task<CognitiveEntities> initializeSearchService()
        {
            try
            {
                var response = await _cognitiveFunctions.InitializeCognitiveSearch();
                return response;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nLogicLayer: CognitiveLogic: runSearchService FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
