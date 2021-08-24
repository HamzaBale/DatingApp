using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{   [Table("Photos")]
    public class Photo
    {
        public int Id {get;set;}
        public string Url { get; set; }

        public bool IsMain { get; set; }

        public string PublicId {get;set;}

     //full delaration of the connection between AppUser table and Photos Table (relazione 1 a molti)
     //la full declartion va fatta nella classe che ha "1" in questo caso Photos.
        public AppUser AppUser { get; set; } 

        public int AppUserId { get; set; } //foreign key not nullable
    }
}