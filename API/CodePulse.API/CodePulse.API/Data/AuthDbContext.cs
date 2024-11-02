using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Create Reader and writer roles

            var readerRoleId = "4a92903f-897f-4ec9-82b6-338b8ab93e2c";
            var writerRoleId = "8b025fd0-214c-43b8-b15c-311bc7f6634c";

            var roles = new List<IdentityRole> 
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };


            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User by default
            var adminUserId = "f9f1ff0a-4ed0-4f25-870e-16e122009002";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper(),
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to Admin default user

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId=adminUserId,
                    RoleId=readerRoleId
                },
                new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
