using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistency.Extensions;
using Persistency.Poco;
using System.Diagnostics;
using System.IO;

namespace Persistency.Data
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Vfile> Vfiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.Entity<Curriculum>().HasIndex(c => c.Identifier);
            modelBuilder.Entity<Curriculum>().HasIndex(c => c.FriendlyId);
            modelBuilder.Entity<Country>().HasIndex(c => c.CountryCode).IsUnique();
            modelBuilder.Entity<Language>().HasIndex(c => c.LanguageCode).IsUnique();
            modelBuilder.Entity<Vfile>().HasIndex(c => c.Identifier).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            string pathToContentRoot = Directory.GetCurrentDirectory();
            string json = Path.Combine(pathToContentRoot, "appsettings.json");

            if (!File.Exists(json))
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = configurationBuilder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}