using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

        public string City { get; set; }

        public string knownAs { get; set; }

        public string country { get; set; }

        public string dateOfBirth { get; set; }

    }
}