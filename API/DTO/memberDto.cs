using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class memberDto
    {
        public int Id { get; set; } //Conventions are important
        // this is our unique key in database table AppUser


        public string Username { get; set; }
        public string PhotoUrl { get; set; }

        public int age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; } 
    }
}