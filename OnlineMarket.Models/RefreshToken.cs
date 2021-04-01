using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        [Key]
        public string JwtId { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Invalidated { get; set; }
        public bool Used { get; set; }
        public SystemUser User { get; set; }
    }
}