using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineMarket.API.Models
{
    public class SystemUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public Region Region { get; set; }
        public string Address { get; set; }
        public string ExtraDetails { get; set; }
        public Image ProfilePicture { get; set; }
        public WishList WishList { get; set; }
    }
}