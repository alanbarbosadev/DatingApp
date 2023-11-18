using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatingApp.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {  
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole()
            {
                Id = "2954573f-9b9b-4f8d-9a6f-7a97e281ad82",
                Name = "Visitor",
                NormalizedName = "VISITOR",
            },
            new IdentityRole()
            {
                Id = "9794b8e8-3336-4e37-b904-e6538f1ab033",
                Name = "Admin",
                NormalizedName = "ADMIN",
            });
        }
    }
}
