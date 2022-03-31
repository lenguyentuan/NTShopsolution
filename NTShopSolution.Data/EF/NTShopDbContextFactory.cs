using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.EF
{
    class NTShopDbContextFactory : IDesignTimeDbContextFactory<NTShopDbContext>
    {
        public NTShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("NTShopSolutionDb");
            var optionsBuilder = new DbContextOptionsBuilder<NTShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NTShopDbContext(optionsBuilder.Options);
        }
    }
}
