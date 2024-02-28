using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.API.Auth
{
    public class AuthRequestDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
