using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineMarket.Models
{
    public class SystemUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
        public Image ProfilePicture { get; set; }
        public ICollection<Product> WishList { get; set; }
        public bool IsDeleted { get; set; }
    }
}