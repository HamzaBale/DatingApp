using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser //An entity is a table
    {
        
        public int Id { get; set; } //Conventions are important
        // this is our unique key in database table AppUser


        public string UserName { get; set; }
        
        public byte[] passwordHash { get; set; }

        public byte[]  passwordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; } //relazione 1 a molti. Photo è una tabella a parte con
        // il foreign key che punta ad AppUser. Grazie al "full Declaration" dove abbiamo dato 2 proprietà 
        //alla tabella Photos per collegarla con AppUser, non avremmo problemi del tipo " 1 foto non ha utenti",
        //poichè la foreignkey può essere nullable. "Cancellare un utente lascia le foto nel database".

        /*public int GetAge(){ //Extends Datetime affinchè abbia metodo che restituisca età
            return DateOfBirth.CalculateAge();
        }*/

    }
}