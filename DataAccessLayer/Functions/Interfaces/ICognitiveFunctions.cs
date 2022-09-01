using DataAccessLayer.Models;

namespace DataAccessLayer.Functions.Interfaces
{
    public interface ICognitiveFunctions
    {
        Task<CognitiveEntities> InitializeCognitiveSearch();
    }
}
