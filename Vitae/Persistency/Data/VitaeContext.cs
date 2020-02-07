using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Persistency.Extensions;
using Persistency.Poco;

using System.Diagnostics;
using System.IO;

namespace Persistency.Data
{
    public class VitaeContext : DbContext
    {
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<Vfile> Vfiles { get; set; }

        public VitaeContext(DbContextOptions<VitaeContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            /* Index, unique */
            modelBuilder.Entity<Curriculum>().HasIndex(c => c.Identifier);
            modelBuilder.Entity<Curriculum>().HasIndex(c => c.FriendlyId);
            modelBuilder.Entity<Country>().HasIndex(c => c.CountryCode).IsUnique();
            modelBuilder.Entity<Language>().HasIndex(c => c.LanguageCode).IsUnique();
            modelBuilder.Entity<Vfile>().HasIndex(c => c.Identifier).IsUnique();

            /* N-M */
            modelBuilder.Entity<PersonCountry>()
                .HasKey(pc => new { pc.PersonID, pc.CountryID});
            modelBuilder.Entity<PersonCountry>()
                .HasOne(pc => pc.Person)
                .WithMany(p => p.PersonCountries)
                .HasForeignKey(pc => pc.PersonID);
            modelBuilder.Entity<PersonCountry>()
                .HasOne(pc => pc.Country)
                .WithMany(c => c.PersonCountries)
                .HasForeignKey(pc => pc.CountryID);
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