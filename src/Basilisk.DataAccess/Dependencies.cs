using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.DataAccess
{
    public static class Dependencies
    {
        public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasiliskTfContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("BasiliskConnection"))
                    );
        }
    }
}
