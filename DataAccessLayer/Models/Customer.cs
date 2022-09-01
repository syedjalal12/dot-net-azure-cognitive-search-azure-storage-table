using Microsoft.Azure.Search;
using System.ComponentModel.DataAnnotations;
using TableEntity = Microsoft.Azure.Cosmos.Table.TableEntity;

namespace DataAccessLayer.Models
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public CustomerEntity() { }

        [Key]
        [IsSearchable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string Id { get; set; }

        [IsSearchable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string Name { get; set; }

        [IsSearchable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string Email { get; set; }

        [IsSearchable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string PhoneNumber { get; set; }

        [IsFilterable]
        [IsRetrievable(true)]
        public bool isActive { get; set; }
        //public string isActive { get; set; }
    }
}