using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
    }
}