﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistency.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true),
                    Iso3 = table.Column<string>(maxLength: 3, nullable: true),
                    NumCode = table.Column<int>(nullable: true),
                    PhoneCode = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "HierarchyLevel",
                columns: table => new
                {
                    HierarchyLevelID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchyLevelCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyLevel", x => x.HierarchyLevelID);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    IndustryID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.IndustryID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Name_de = table.Column<string>(maxLength: 100, nullable: false),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: false),
                    Name_it = table.Column<string>(maxLength: 100, nullable: false),
                    Name_es = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "LogActivity",
                columns: table => new
                {
                    LogActivityID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurriculumID = table.Column<Guid>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    LogArea = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 1000, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 1000, nullable: true),
                    UserLanguage = table.Column<string>(maxLength: 2, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActivity", x => x.LogActivityID);
                });

            migrationBuilder.CreateTable(
                name: "LogPublication",
                columns: table => new
                {
                    LogPublicationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurriculumID = table.Column<Guid>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    LogArea = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 1000, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 1000, nullable: true),
                    UserLanguage = table.Column<string>(maxLength: 2, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    PublicationID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPublication", x => x.LogPublicationID);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                columns: table => new
                {
                    MaritalStatusID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaritalStatusCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.MaritalStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    MonthID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.MonthID);
                });

            migrationBuilder.CreateTable(
                name: "Vfile",
                columns: table => new
                {
                    VfileID = table.Column<Guid>(nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    MimeType = table.Column<string>(maxLength: 100, nullable: false),
                    FileName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vfile", x => x.VfileID);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    CurriculumID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    LanguageID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_Curriculum_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    AcademicTitle = table.Column<string>(maxLength: 100, nullable: true),
                    Slogan = table.Column<string>(maxLength: 1000, nullable: true),
                    Photo = table.Column<string>(type: "varchar(max)", nullable: true),
                    VfileID = table.Column<Guid>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutID);
                    table.ForeignKey(
                        name: "FK_About_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_About_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_About_Vfile_VfileID",
                        column: x => x.VfileID,
                        principalTable: "Vfile",
                        principalColumn: "VfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abroad",
                columns: table => new
                {
                    AbroadID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CountryID = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abroad", x => x.AbroadID);
                    table.ForeignKey(
                        name: "FK_Abroad_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abroad_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Abroad_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    AwardID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    AwardedFrom = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    AwardedOn = table.Column<DateTime>(nullable: false),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.AwardID);
                    table.ForeignKey(
                        name: "FK_Award_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Award_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    CertificateID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Issuer = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ExpiresOn = table.Column<DateTime>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK_Certificate_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificate_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CountryID = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumLanguage",
                columns: table => new
                {
                    CurriculumID = table.Column<Guid>(nullable: false),
                    LanguageID = table.Column<long>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumLanguage", x => new { x.CurriculumID, x.LanguageID });
                    table.ForeignKey(
                        name: "FK_CurriculumLanguage_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumLanguage_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    EducationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    CountryID = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Grade = table.Column<float>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.EducationID);
                    table.ForeignKey(
                        name: "FK_Education_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Education_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    ExperienceID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    CompanyDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    HierarchyLevelID = table.Column<long>(nullable: false),
                    IndustryID = table.Column<long>(nullable: false),
                    CountryID = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.ExperienceID);
                    table.ForeignKey(
                        name: "FK_Experience_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experience_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experience_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experience_HierarchyLevel_HierarchyLevelID",
                        column: x => x.HierarchyLevelID,
                        principalTable: "HierarchyLevel",
                        principalColumn: "HierarchyLevelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experience_Industry_IndustryID",
                        column: x => x.IndustryID,
                        principalTable: "Industry",
                        principalColumn: "IndustryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    InterestName = table.Column<string>(maxLength: 100, nullable: false),
                    Association = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestID);
                    table.ForeignKey(
                        name: "FK_Interest_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interest_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSkill",
                columns: table => new
                {
                    LanguageSkillID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    SpokenLanguageID = table.Column<long>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSkill", x => x.LanguageSkillID);
                    table.ForeignKey(
                        name: "FK_LanguageSkill_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageSkill_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageSkill_Language_SpokenLanguageID",
                        column: x => x.SpokenLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetail",
                columns: table => new
                {
                    PersonalDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    StreetNo = table.Column<string>(maxLength: 10, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PhonePrefix = table.Column<string>(maxLength: 6, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 16, nullable: false),
                    Citizenship = table.Column<string>(maxLength: 100, nullable: false),
                    MaritalStatusID = table.Column<long>(nullable: true),
                    CountryID = table.Column<long>(nullable: true),
                    SpokenLanguageID = table.Column<long>(nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetail", x => x.PersonalDetailID);
                    table.ForeignKey(
                        name: "FK_PersonalDetail_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetail_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetail_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetail_MaritalStatus_MaritalStatusID",
                        column: x => x.MaritalStatusID,
                        principalTable: "MaritalStatus",
                        principalColumn: "MaritalStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDetail_Language_SpokenLanguageID",
                        column: x => x.SpokenLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    PublicationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    PublicationIdentifier = table.Column<Guid>(nullable: false),
                    Anonymize = table.Column<bool>(nullable: false),
                    Secure = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 250, nullable: true),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true),
                    Color = table.Column<string>(maxLength: 100, nullable: false),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.PublicationID);
                    table.ForeignKey(
                        name: "FK_Publication_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publication_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    ReferenceID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhonePrefix = table.Column<string>(maxLength: 6, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: false),
                    CountryID = table.Column<long>(nullable: true),
                    Hide = table.Column<bool>(nullable: false),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.ReferenceID);
                    table.ForeignKey(
                        name: "FK_Reference_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reference_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reference_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(maxLength: 100, nullable: false),
                    Skillset = table.Column<string>(maxLength: 1000, nullable: true),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                    table.ForeignKey(
                        name: "FK_Skill_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skill_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialLink",
                columns: table => new
                {
                    SocialLinkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    CurriculumLanguageLanguageID = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    SocialPlatform = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: false),
                    CurriculumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLink", x => x.SocialLinkID);
                    table.ForeignKey(
                        name: "FK_SocialLink_Curriculum_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SocialLink_Language_CurriculumLanguageLanguageID",
                        column: x => x.CurriculumLanguageLanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Child",
                columns: table => new
                {
                    ChildID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    PersonalDetailID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Child", x => x.ChildID);
                    table.ForeignKey(
                        name: "FK_Child_PersonalDetail_PersonalDetailID",
                        column: x => x.PersonalDetailID,
                        principalTable: "PersonalDetail",
                        principalColumn: "PersonalDetailID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonCountry",
                columns: table => new
                {
                    PersonalDetailID = table.Column<long>(nullable: false),
                    CountryID = table.Column<long>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCountry", x => new { x.PersonalDetailID, x.CountryID });
                    table.ForeignKey(
                        name: "FK_PersonCountry_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCountry_PersonalDetail_PersonalDetailID",
                        column: x => x.PersonalDetailID,
                        principalTable: "PersonalDetail",
                        principalColumn: "PersonalDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_About_CurriculumID",
                table: "About",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_About_CurriculumLanguageLanguageID",
                table: "About",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_About_VfileID",
                table: "About",
                column: "VfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Abroad_CountryID",
                table: "Abroad",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Abroad_CurriculumID",
                table: "Abroad",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Abroad_CurriculumLanguageLanguageID",
                table: "Abroad",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Award_CurriculumID",
                table: "Award",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Award_CurriculumLanguageLanguageID",
                table: "Award",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_CurriculumID",
                table: "Certificate",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_CurriculumLanguageLanguageID",
                table: "Certificate",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Child_PersonalDetailID",
                table: "Child",
                column: "PersonalDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CountryCode",
                table: "Country",
                column: "CountryCode",
                unique: true,
                filter: "[CountryCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Order",
                table: "Country",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CountryID",
                table: "Course",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CurriculumID",
                table: "Course",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CurriculumLanguageLanguageID",
                table: "Course",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_LanguageID",
                table: "Curriculum",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_UserID",
                table: "Curriculum",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumLanguage_CurriculumID",
                table: "CurriculumLanguage",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumLanguage_LanguageID",
                table: "CurriculumLanguage",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CountryID",
                table: "Education",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CurriculumID",
                table: "Education",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CurriculumLanguageLanguageID",
                table: "Education",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_CountryID",
                table: "Experience",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_CurriculumID",
                table: "Experience",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_CurriculumLanguageLanguageID",
                table: "Experience",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_HierarchyLevelID",
                table: "Experience",
                column: "HierarchyLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_IndustryID",
                table: "Experience",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyLevel_HierarchyLevelCode",
                table: "HierarchyLevel",
                column: "HierarchyLevelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Industry_IndustryCode",
                table: "Industry",
                column: "IndustryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interest_CurriculumID",
                table: "Interest",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_CurriculumLanguageLanguageID",
                table: "Interest",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCode",
                table: "Language",
                column: "LanguageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_Name",
                table: "Language",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_Order",
                table: "Language",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkill_CurriculumID",
                table: "LanguageSkill",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkill_CurriculumLanguageLanguageID",
                table: "LanguageSkill",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkill_SpokenLanguageID",
                table: "LanguageSkill",
                column: "SpokenLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_LogActivity_CurriculumID",
                table: "LogActivity",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_LogActivity_Timestamp",
                table: "LogActivity",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_LogPublication_CurriculumID",
                table: "LogPublication",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_LogPublication_PublicationID",
                table: "LogPublication",
                column: "PublicationID");

            migrationBuilder.CreateIndex(
                name: "IX_LogPublication_Timestamp",
                table: "LogPublication",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_MaritalStatusCode",
                table: "MaritalStatus",
                column: "MaritalStatusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Month_MonthCode",
                table: "Month",
                column: "MonthCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_CountryID",
                table: "PersonalDetail",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_CurriculumID",
                table: "PersonalDetail",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_CurriculumLanguageLanguageID",
                table: "PersonalDetail",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_MaritalStatusID",
                table: "PersonalDetail",
                column: "MaritalStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_SpokenLanguageID",
                table: "PersonalDetail",
                column: "SpokenLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCountry_CountryID",
                table: "PersonCountry",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_CurriculumID",
                table: "Publication",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_CurriculumLanguageLanguageID",
                table: "Publication",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_PublicationIdentifier",
                table: "Publication",
                column: "PublicationIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reference_CountryID",
                table: "Reference",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_CurriculumID",
                table: "Reference",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_CurriculumLanguageLanguageID",
                table: "Reference",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CurriculumID",
                table: "Skill",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CurriculumLanguageLanguageID",
                table: "Skill",
                column: "CurriculumLanguageLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_CurriculumID",
                table: "SocialLink",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_CurriculumLanguageLanguageID",
                table: "SocialLink",
                column: "CurriculumLanguageLanguageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "Abroad");

            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Child");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "CurriculumLanguage");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "LanguageSkill");

            migrationBuilder.DropTable(
                name: "LogActivity");

            migrationBuilder.DropTable(
                name: "LogPublication");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "PersonCountry");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SocialLink");

            migrationBuilder.DropTable(
                name: "Vfile");

            migrationBuilder.DropTable(
                name: "HierarchyLevel");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "PersonalDetail");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "MaritalStatus");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
