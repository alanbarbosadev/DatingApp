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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>()
            {
                UserId = "efe13601-f3a7-487b-96f8-e70e14f4d480",
                RoleId = "2954573f-9b9b-4f8d-9a6f-7a97e281ad82"
            },
            new IdentityUserRole<string>()
            {
                UserId = "7aa6159b-28ce-4139-91cb-3ef9125e2e31",
                RoleId = "9794b8e8-3336-4e37-b904-e6538f1ab033"
            });
        }
    }
}
