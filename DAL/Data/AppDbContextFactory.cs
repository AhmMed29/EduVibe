using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EduVibe.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = FindBasePath(Directory.GetCurrentDirectory())
                ?? Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration.GetConnectionString("connectionString"))
                .Options;

            return new AppDbContext(options);
        }

        private static string? FindBasePath(string start)
        {
            var dir = new DirectoryInfo(start);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "PL");
                if (Directory.Exists(candidate)
                    && File.Exists(Path.Combine(candidate, "appsettings.json")))
                    return candidate;
                dir = dir.Parent;
            }
            return null;
        }
    }
}
