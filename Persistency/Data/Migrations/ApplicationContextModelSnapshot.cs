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
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persistency.Poco.About", b =>
                {
                    b.Property<int>("AboutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("CV")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Slogan")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.HasKey("AboutID");

                    b.ToTable("About");
                });

            modelBuilder.Entity("Persistency.Poco.Award", b =>
                {
                    b.Property<int>("AwardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("AwardID");

                    b.HasIndex("PersonID");

                    b.ToTable("Award");
                });

            modelBuilder.Entity("Persistency.Poco.Curriculum", b =>
                {
                    b.Property<int>("CurriculumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("CurriculumID");

                    b.HasIndex("FriendlyId");

                    b.HasIndex("Identifier");

                    b.HasIndex("PersonID");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Persistency.Poco.Interest", b =>
                {
                    b.Property<int>("InterestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("InterestID");

                    b.HasIndex("PersonID");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("Persistency.Poco.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("LanguageID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Persistency.Poco.LanguageSkill", b =>
                {
                    b.Property<int>("LanguageSkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LanguageID")
                        .HasColumnType("int");

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.HasKey("LanguageSkillID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PersonID");

                    b.ToTable("LanguageSkill");
                });

            modelBuilder.Entity("Persistency.Poco.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AboutID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

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

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("StreetNo")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("ZipCode")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.HasKey("PersonID");

                    b.HasIndex("AboutID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Persistency.Poco.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.Property<float?>("Rate")
                        .HasColumnType("real");

                    b.HasKey("SkillID");

                    b.HasIndex("PersonID");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Persistency.Poco.SocialLink", b =>
                {
                    b.Property<int>("SocialLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Hyperlink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.Property<int>("SocialPlatform")
                        .HasColumnType("int");

                    b.HasKey("SocialLinkID");

                    b.HasIndex("PersonID");

                    b.ToTable("SocialLink");
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
                        .WithMany("Languages")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Persistency.Poco.Person", b =>
                {
                    b.HasOne("Persistency.Poco.About", "About")
                        .WithMany()
                        .HasForeignKey("AboutID")
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
