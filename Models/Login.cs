using Microsoft.Build.Framework;

namespace Complete.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string Remember { get; set; }
    }
}
