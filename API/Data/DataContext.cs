
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext //dbcontext is a bridge between our entity and the database.
    //layer of abstraction helping us quering and storing data in database
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

      public DbSet<AppUser> Users { get; set; } //create a table named Users

      public DbSet<UserLike> Likes { get; set; }

      protected override void OnModelCreating(ModelBuilder builder){ //in case of many to many relationship 
      //between the same entity

 
        base.OnModelCreating(builder); //se non fatto ==> errore quando migration
        
        builder.Entity<UserLike>().HasKey(x=> new {x.SourceUserId,x.LikedUserId}); 
        //crea la chiave primaria della tabella likes.(UserLike entity)
        
        builder.Entity<UserLike>().HasOne(s => s.SourceUser).WithMany(l => l.LikedUser)//fa riferimento alla lista all'interno di Appuser
        .HasForeignKey(s=>s.SourceUserId).OnDelete(DeleteBehavior.Cascade);
        //we are saying that 1 User can like Many users. 
        builder.Entity<UserLike>().HasOne(s => s.LikedUser).WithMany(l => l.LikedByUser)//fa riferimento alla lista all'interno di Appuser
        .HasForeignKey(l=>l.LikedUserId).OnDelete(DeleteBehavior.Cascade);
        //1 user can be liked by many users.
      }




      //public DbSet<AppUser> Casual { get; set; }
    
    }
}