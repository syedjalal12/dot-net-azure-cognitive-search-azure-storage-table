using DataAccessLayer.Models;

namespace DataAccessLayer.Helpers
{
    public static class CognitiveHelpers
    {
        public static CognitiveEntities ConvertCognitiveEntities(AzureServices.Models.CognitiveEntities vm)
        {
            try
            {
                var model = new CognitiveEntities
                {
                    IndexCreated = vm.IndexCreated,
                    IndexerCreated = vm.IndexerCreated,
                    DataSourceCreated = vm.DataSourceCreated,
                    IndexerRunning = vm.IndexerRunning
                };
                return model;
            }
            catch (Exception ex)
            {
                var ExcptionMessage = new Exception($"\nDataAccessLayer: CognitiveHelpers: ConvertCognitiveEntities FAILED\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception(ExcptionMessage.Message);
            }
        }
    }
}
