﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistency.Data;

namespace Persistency.Migrations
{
    [DbContext(typeof(VitaeContext))]
    partial class VitaeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Poco.About", b =>
                {
                    b.Property<Guid>("AboutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicTitle")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Slogan")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<Guid?>("VfileID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AboutID");

                    b.HasIndex("VfileID");

                    b.ToTable("About");
                });

            modelBuilder.Entity("Model.Poco.Abroad", b =>
                {
                    b.Property<Guid>("AbroadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("AbroadID");

                    b.HasIndex("CountryID");

                    b.HasIndex("PersonID");

                    b.ToTable("Abroad");
                });

            modelBuilder.Entity("Model.Poco.Award", b =>
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
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

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

            modelBuilder.Entity("Model.Poco.Certificate", b =>
                {
                    b.Property<Guid>("CertificateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("ExpiresOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssuedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CertificateID");

                    b.HasIndex("PersonID");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("Model.Poco.Child", b =>
                {
                    b.Property<Guid>("ChildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonalDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ChildID");

                    b.HasIndex("PersonalDetailID");

                    b.ToTable("Child");
                });

            modelBuilder.Entity("Model.Poco.Country", b =>
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

            modelBuilder.Entity("Model.Poco.Course", b =>
                {
                    b.Property<Guid>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("CourseID");

                    b.HasIndex("CountryID");

                    b.HasIndex("PersonID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Model.Poco.Curriculum", b =>
                {
                    b.Property<Guid>("CurriculumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FriendlyId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CurriculumID");

                    b.HasIndex("FriendlyId");

                    b.HasIndex("PersonID");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Model.Poco.Education", b =>
                {
                    b.Property<Guid>("EducationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Grade")
                        .HasColumnType("real");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("CountryID");

                    b.HasIndex("PersonID");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("Model.Poco.Experience", b =>
                {
                    b.Property<Guid>("ExperienceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("ExperienceID");

                    b.HasIndex("CountryID");

                    b.HasIndex("PersonID");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("Model.Poco.Interest", b =>
                {
                    b.Property<Guid>("InterestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Association")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("InterestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InterestID");

                    b.HasIndex("PersonID");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("Model.Poco.Language", b =>
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

            modelBuilder.Entity("Model.Poco.LanguageSkill", b =>
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

            modelBuilder.Entity("Model.Poco.Log", b =>
                {
                    b.Property<Guid>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurriculumID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("LogArea")
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<int>("LogLevel")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Page")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("UserLanguage")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("LogID");

                    b.HasIndex("CurriculumID");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Model.Poco.Month", b =>
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

            modelBuilder.Entity("Model.Poco.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AboutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PersonalDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonID");

                    b.HasIndex("AboutID");

                    b.HasIndex("PersonalDetailID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Model.Poco.PersonCountry", b =>
                {
                    b.Property<Guid>("PersonalDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("PersonalDetailID", "CountryID");

                    b.HasIndex("CountryID");

                    b.ToTable("PersonCountry");
                });

            modelBuilder.Entity("Model.Poco.PersonalDetail", b =>
                {
                    b.Property<Guid>("PersonalDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Citizenship")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

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

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("int");

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

                    b.HasKey("PersonalDetailID");

                    b.HasIndex("CountryID");

                    b.HasIndex("LanguageID");

                    b.ToTable("PersonalDetail");
                });

            modelBuilder.Entity("Model.Poco.Reference", b =>
                {
                    b.Property<Guid>("ReferenceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

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

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("ReferenceID");

                    b.HasIndex("CountryID");

                    b.HasIndex("PersonID");

                    b.ToTable("Reference");
                });

            modelBuilder.Entity("Model.Poco.Skill", b =>
                {
                    b.Property<Guid>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Skillset")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("SkillID");

                    b.HasIndex("PersonID");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Model.Poco.SocialLink", b =>
                {
                    b.Property<Guid>("SocialLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

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

            modelBuilder.Entity("Model.Poco.Vfile", b =>
                {
                    b.Property<Guid>("VfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VfileID");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("Vfile");
                });

            modelBuilder.Entity("Model.Poco.About", b =>
                {
                    b.HasOne("Model.Poco.Vfile", "Vfile")
                        .WithMany()
                        .HasForeignKey("VfileID");
                });

            modelBuilder.Entity("Model.Poco.Abroad", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Abroads")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Award", b =>
                {
                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Awards")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Certificate", b =>
                {
                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Certificates")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Child", b =>
                {
                    b.HasOne("Model.Poco.PersonalDetail", null)
                        .WithMany("Children")
                        .HasForeignKey("PersonalDetailID");
                });

            modelBuilder.Entity("Model.Poco.Course", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Courses")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Curriculum", b =>
                {
                    b.HasOne("Model.Poco.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Education", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Educations")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Experience", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Experiences")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Interest", b =>
                {
                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Interests")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.LanguageSkill", b =>
                {
                    b.HasOne("Model.Poco.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("LanguageSkills")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Person", b =>
                {
                    b.HasOne("Model.Poco.About", "About")
                        .WithMany()
                        .HasForeignKey("AboutID");

                    b.HasOne("Model.Poco.PersonalDetail", "PersonalDetail")
                        .WithMany()
                        .HasForeignKey("PersonalDetailID");
                });

            modelBuilder.Entity("Model.Poco.PersonCountry", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany("PersonCountries")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Poco.PersonalDetail", "PersonalDetail")
                        .WithMany("PersonCountries")
                        .HasForeignKey("PersonalDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Poco.PersonalDetail", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("Model.Poco.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");
                });

            modelBuilder.Entity("Model.Poco.Reference", b =>
                {
                    b.HasOne("Model.Poco.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("References")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.Skill", b =>
                {
                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("Skills")
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("Model.Poco.SocialLink", b =>
                {
                    b.HasOne("Model.Poco.Person", null)
                        .WithMany("SocialLinks")
                        .HasForeignKey("PersonID");
                });
#pragma warning restore 612, 618
        }
    }
}
