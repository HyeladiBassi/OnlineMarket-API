using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.DataTransferObjects.Authentication
{
    public class AuthRequest
    {
        [Required(ErrorMessage = "Access is required")]
        public string access { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}