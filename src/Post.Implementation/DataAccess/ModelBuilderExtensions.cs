using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Implementation.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            
            List<IdentityRole> roles = new List<IdentityRole>()
            {

                new IdentityRole{Name="Admin",NormalizedName="ADMIN"},
                new IdentityRole{Name="User",NormalizedName="USER"},
                new IdentityRole{Name="Tester",NormalizedName="TESTER"},
                new IdentityRole{Name="Visitor",NormalizedName="VISITOR"},
            };
            builder.Entity<IdentityRole>().HasData(roles);
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser{
                    UserName="Meko",
                    NormalizedUserName="MEKO",
                    Email="ayhamalama@gmail.com",
                    NormalizedEmail="",
                }};
            builder.Entity<ApplicationUser>().HasData(users);
            var passwordhasher = new PasswordHasher<ApplicationUser>();
            users[0].PasswordHash = passwordhasher.HashPassword(users[0], "e2I0s3A4g#sd");
            List<IdentityUserRole<string>> usersRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>{
                UserId = users[0].Id,
                RoleId = roles.First(x => x.Name =="Admin").Id
                }
                
            };
            builder.Entity<IdentityUserRole<string>>().HasData(usersRoles);

        }
    }
}
