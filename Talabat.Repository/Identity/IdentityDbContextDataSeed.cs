using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class IdentityDbContextDataSeed
    {
        public static async Task UserIdentitySeed(UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Roushdy",
                    Email = "melmasry818@gmail.com",
                    UserName = "mahmoud.roushdy",
                    PhoneNumber = "01067388216",

                };
                await userManager.CreateAsync(user, "Elmasry@871993");
            }

        }
    }
}
