using DatingApp.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Identity.IdentityDataContext
{
    public class DatingAppIdentityContext : IdentityDbContext<User>
    {
        public DatingAppIdentityContext(DbContextOptions<DatingAppIdentityContext> options) : base(options)
        {          
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DatingAppIdentityContext).Assembly);
        }
    }
}
