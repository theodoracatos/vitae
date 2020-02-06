﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistency.Data;

namespace Persistency.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persistency.Poco.About", b =>
                {
                    b.Property<Guid>("AboutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Slogan")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<Guid?>("VfileID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AboutID");

                    b.HasIndex("VfileID");

                    b.ToTable("About");
                });

            modelBuilder.Entity("Persistency.Poco.Award", b =>
                {
                    b.Property<Guid>("AwardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AwardedFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("AwardedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AwardID");

                    b.HasIndex("PersonID");

                    b.ToTable("Award");
                });

            modelBuilder.Entity("Persistency.Poco.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Iso3")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_de")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_es")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_fr")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_it")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("NumCode")
                        .HasColumnType("int");

                    b.Property<int>("PhoneCode")
                        .HasColumnType("int");

                    b.HasKey("CountryID");

                    b.HasIndex("CountryCode")
                        .IsUnique()
                        .HasFilter("[CountryCode] IS NOT NULL");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Persistency.Poco.Curriculum", b =>
                {
                    b.Property<Guid>("CurriculumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FriendlyId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CurriculumID");

                    b.HasIndex("FriendlyId");

                    b.HasIndex("Identifier");

                    b.HasIndex("PersonID");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Persistency.Poco.Education", b =>
                {
                    b.Property<Guid>("EducationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Grade")
                        .HasColumnType("real");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Resumee")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("SchoolLink")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("EducationID");

                    b.HasIndex("PersonID");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("Persistency.Poco.Experience", b =>
                {
                    b.Property<Guid>("ExperienceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CompanyLink")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Resumee")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("ExperienceID");

                    b.HasIndex("PersonID");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("Persistency.Poco.Interest", b =>
                {
                    b.Property<Guid>("InterestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InterestID");

                    b.HasIndex("PersonID");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("Persistency.Poco.Language", b =>
                {
                    b.Property<Guid>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_de")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_es")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_fr")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_it")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("LanguageID");

                    b.HasIndex("LanguageCode")
                        .IsUnique();

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Persistency.Poco.LanguageSkill", b =>
                {
                    b.Property<Guid>("LanguageSkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.HasKey("LanguageSkillID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PersonID");

                    b.ToTable("LanguageSkill");
                });

            modelBuilder.Entity("Persistency.Poco.Month", b =>
                {
                    b.Property<Guid>("MonthID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MonthCode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_de")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_es")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_fr")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name_it")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MonthID");

                    b.ToTable("Month");
                });

            modelBuilder.Entity("Persistency.Poco.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AboutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LanguageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("StreetNo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("PersonID");

                    b.HasIndex("AboutID");

                    b.HasIndex("CountryID");

                    b.HasIndex("LanguageID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Persistency.Poco.PersonCountry", b =>
                {
                    b.Property<Guid>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("PersonID", "CountryID");

                    b.HasIndex("CountryID");

                    b.ToTable("PersonCountry");
                });

            modelBuilder.Entity("Persistency.Poco.Skill", b =>
                {
                    b.Property<Guid>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Rate")
                        .HasColumnType("real");

                    b.HasKey("SkillID");

                    b.HasIndex("PersonID");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Persistency.Poco.SocialLink", b =>
                {
                    b.Property<Guid>("SocialLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Hyperlink")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SocialPlatform")
                        .HasColumnType("int");

                    b.HasKey("SocialLinkID");

                    b.HasIndex("PersonID");

                    b.ToTable("SocialLink");
                });

            modelBuilder.Entity("Persistency.Poco.Vfile", b =>
                {
                    b.Property<Guid>("VfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VfileID");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("Vfile");
                });

            modelBuilder.Entity("Persistency.Poco.About", b =>
                {
                    b.HasOne("Persistency.Poco.Vfile", "Vfile")
                        .WithMany()
                        .HasForeignKey("VfileID");
                });

            modelBuilder.Entity("Persistency.Poco.Award", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("Awards")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Curriculum", b =>
                {
                    b.HasOne("Persistency.Poco.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Education", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("Educations")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Experience", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("Experiences")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Interest", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("Interests")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.LanguageSkill", b =>
                {
                    b.HasOne("Persistency.Poco.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("LanguageSkills")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Person", b =>
                {
                    b.HasOne("Persistency.Poco.About", "About")
                        .WithMany()
                        .HasForeignKey("AboutID");

                    b.HasOne("Persistency.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("Persistency.Poco.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");
                });

            modelBuilder.Entity("Persistency.Poco.PersonCountry", b =>
                {
                    b.HasOne("Persistency.Poco.Country", "Country")
                        .WithMany("PersonCountries")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistency.Poco.Person", "Person")
                        .WithMany("PersonCountries")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Persistency.Poco.Skill", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("Skills")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.SocialLink", b =>
                {
                    b.HasOne("Persistency.Poco.Person", null)
                        .WithMany("SocialLinks")
                        .HasForeignKey("PersonID");
                });
#pragma warning restore 612, 618
        }
    }
}
