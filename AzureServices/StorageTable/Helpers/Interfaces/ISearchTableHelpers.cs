using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServices.StorageTable.Helpers.Interfaces
{
    public interface ISearchTableHelpers
    {
        Task reRunIndexer();

        ISearchIndexClient IndexClient();
    }
}
