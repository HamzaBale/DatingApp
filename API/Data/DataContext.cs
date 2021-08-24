
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

      //public DbSet<AppUser> Casual { get; set; }
    
    }
}