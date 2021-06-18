using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.DataTransferObjects.Authentication
{
    public class ModeratorCreateDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password should be between 5 and 100 characters", MinimumLength = 5)]
        public string password { get; set; }
    }
}