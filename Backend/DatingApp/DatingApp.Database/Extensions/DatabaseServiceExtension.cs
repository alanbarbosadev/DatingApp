using DatingApp.Database.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Database.Extensions
{
    public static class DatabaseServiceExtension
    {
        public static IServiceCollection AddDatabaseServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatingAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
    }
}
