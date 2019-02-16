using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONSULS_SYSTEM.DATA
{
    public static class DbInit
    {
        public static void INIT(IServiceProvider serviceProvider)
        {
            var context = new CONSULS_Context(serviceProvider.GetRequiredService<DbContextOptions<CONSULS_Context>>());
            try
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
