using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Model.Poco;

using Persistency.Extensions;

using System.Diagnostics;
using System.IO;

namespace Persistency.Data
{
    public class VitaeContext : DbContext
    {
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<CurriculumLanguage> CurriculumLanguages { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<HierarchyLevel> HierarchyLevels { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Vfile> Vfiles { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        public VitaeContext(DbContextOptions<VitaeContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            /* Index, unique */
            modelBuilder.Entity<Country>().HasIndex(c => c.CountryCode).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name);
            modelBuilder.Entity<Country>().HasIndex(c => c.Order);

            modelBuilder.Entity<Language>().HasIndex(c => c.LanguageCode).IsUnique();
            modelBuilder.Entity<Language>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Language>().HasIndex(c => c.Order).IsUnique();

            modelBuilder.Entity<CurriculumLanguage>().HasIndex(c => c.CurriculumID);
            modelBuilder.Entity<CurriculumLanguage>().HasIndex(c => c.LanguageID);
            modelBuilder.Entity<Industry>().HasIndex(c => c.IndustryCode).IsUnique();
            modelBuilder.Entity<HierarchyLevel>().HasIndex(c => c.HierarchyLevelCode).IsUnique();

            modelBuilder.Entity<Month>().HasIndex(c => c.MonthCode).IsUnique();
            modelBuilder.Entity<MaritalStatus>().HasIndex(c => c.MaritalStatusCode).IsUnique();
            modelBuilder.Entity<Publication>().HasIndex(p => p.PublicationIdentifier).IsUnique();

            modelBuilder.Entity<Log>().HasIndex(c => c.CurriculumID);
            modelBuilder.Entity<Log>().HasIndex(c => c.PublicationID);
            modelBuilder.Entity<Log>().HasIndex(c => c.Timestamp);

            /* N-M */
            modelBuilder.Entity<PersonCountry>()
                .HasKey(pc => new { pc.PersonalDetailID, pc.CountryID});
            modelBuilder.Entity<PersonCountry>()
                .HasOne(pc => pc.PersonalDetail)
                .WithMany(p => p.PersonCountries)
                .HasForeignKey(pc => pc.PersonalDetailID);
            modelBuilder.Entity<PersonCountry>()
                .HasOne(pc => pc.Country)
                .WithMany(c => c.PersonCountries)
                .HasForeignKey(pc => pc.CountryID);

            modelBuilder.Entity<CurriculumLanguage>()
                .HasKey(cu => new { cu.CurriculumID, cu.LanguageID });
            modelBuilder.Entity<CurriculumLanguage>()
                .HasOne(cu => cu.Curriculum)
                .WithMany(c => c.CurriculumLanguages)
                .HasForeignKey(cu => cu.CurriculumID);
            modelBuilder.Entity<CurriculumLanguage>()
                .HasOne(la => la.Language)
                .WithMany(l => l.CurriculumLanguages)
                .HasForeignKey(la => la.LanguageID);
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