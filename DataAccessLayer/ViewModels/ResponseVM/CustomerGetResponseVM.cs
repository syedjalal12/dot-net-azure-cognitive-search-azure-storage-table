using System;
namespace DataAccessLayer.ViewModels.ResponseVM
{
    public class CustomerGetResponseVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public bool isActive { get; set; }
    }
}
