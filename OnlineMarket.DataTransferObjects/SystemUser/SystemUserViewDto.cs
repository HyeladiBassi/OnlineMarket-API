using System;
using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.DataTransferObjects.SystemUser
{
    public class SystemUserViewDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
    }
}