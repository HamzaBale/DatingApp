using System;
using System.Collections.Generic;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
     //An entity is a table
    {
        
       /* public int Id { get; set; } //Conventions are important
        // this is our unique key in database table AppUser


        public string UserName { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[]  passwordSalt { get; set; }
*/ //li togliamo perchè ci pensa IdentityUser.

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

        public ICollection<UserLike> LikedByUser { get; set; } //many to many relationship
        public ICollection<UserLike> LikedUser { get; set; }   //many to many relationship

        public ICollection<Message> MessagesSent { get; set; } 
        public ICollection<Message> MessagesReceived{ get; set; } 

          public ICollection<AppUserRole> UserRoles { get; set; }

        /*public int GetAge(){ //Extends Datetime affinchè abbia metodo che restituisca età
            return DateOfBirth.CalculateAge();
        }*/

    }
}