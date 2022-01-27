using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QA.Infra;
using Microsoft.Extensions.Configuration;
using System.Configuration;

public class QAContextFactory : IDesignTimeDbContextFactory<QAContext>
{
    public QAContext CreateDbContext(string[] args)
    {
        var migrationsAssembly = typeof(QAContext).GetTypeInfo().Assembly.GetName().Name;
        var optionsBuilder = new DbContextOptionsBuilder<QAContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultPgConnection");        
        optionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly));

        return new QAContext(optionsBuilder.Options);
    }
}