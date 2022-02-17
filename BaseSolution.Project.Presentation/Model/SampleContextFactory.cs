using BaseSolution.Project.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BaseSolution.Project.Presentation.Model
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DataConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("BaseSolution.Project.Presentation"));
            return new DataContext(builder.Options);
        }
    }
}
