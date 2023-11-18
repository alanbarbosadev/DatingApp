using DatingApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            builder.HasData(new User()
            {
                Id = "efe13601-f3a7-487b-96f8-e70e14f4d480",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = hasher.HashPassword(null, "12345")
            },
            new User()
            {
                Id = "7aa6159b-28ce-4139-91cb-3ef9125e2e31",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                UserName = "user@localhost.com",
                PasswordHash = hasher.HashPassword(null, "12345")
            });
        }
    }
}
