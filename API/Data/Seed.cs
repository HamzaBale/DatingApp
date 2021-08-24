using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static  class Seed
    {
        public static async Task SeedUsers(DataContext context){

            if(await context.Users.AnyAsync()) return;
            
            var Data = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
        
            var Users = JsonSerializer.Deserialize<List<AppUser>>(Data);

            foreach (var user in Users)
            {

                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.passwordSalt = hmac.Key;
                user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                await context.Users.AddAsync(user);
            }
            await context.SaveChangesAsync();
        }
    }
}