using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static  class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager){

            if(await userManager.Users.AnyAsync()) return;
            
            var Data = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
        
            var Users = JsonSerializer.Deserialize<List<AppUser>>(Data);

            var Roles = new List<AppRole>{
                new AppRole{Name="Member"},
                new AppRole{Name="Admin"},
                new AppRole{Name="Moderator"}
            };

            foreach (var role in Roles)
            {
               await roleManager.CreateAsync(role); // crea nella tabella AspNetRoles 3 righe
               //id = 1 ==> member
               //id = 2 ==> admin
               //id = 3 ==> Moderator
            }



            foreach (var user in Users)
            {

                user.UserName = user.UserName.ToLower();
                
                await userManager.CreateAsync(user,"Passw0rd");//non c'è bisogno di fare saveall come per context
                //ci pensa gia lui

                await userManager.AddToRoleAsync(user,"Member");
            }
           // await context.SaveAllAsync(); 
            var userAdmin = new AppUser{
                UserName="admin"
            };
            await userManager.CreateAsync(userAdmin, "Passw0rd");
            await userManager.AddToRolesAsync(userAdmin, new[] {"Admin", "Moderator"});

        }
    }
}