using System;

namespace OnlineMarket.DataTransferObjects.SystemUser
{
    public class SystemUserViewDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}