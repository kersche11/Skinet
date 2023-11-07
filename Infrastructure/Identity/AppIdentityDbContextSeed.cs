

using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.at",
                    UserName = "bob@test.at",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobby",
                        Street="10 The Street",
                        City="New York",
                        State="NY",
                        ZipCode="9020"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}