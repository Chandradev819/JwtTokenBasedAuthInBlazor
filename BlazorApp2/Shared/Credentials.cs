using System.ComponentModel.DataAnnotations;

namespace BlazorApp2.Shared
{
    public class Credentials
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
