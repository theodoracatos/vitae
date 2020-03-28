using System;
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
                    CountryID = table.Column<Guid>(nullable: false),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Name_de = table.Column<string>(maxLength: 100, nullable: true),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: true),
                    Name_it = table.Column<string>(maxLength: 100, nullable: true),
                    Name_es = table.Column<string>(maxLength: 100, nullable: true),
                    Iso3 = table.Column<string>(maxLength: 3, nullable: true),
                    NumCode = table.Column<int>(nullable: true),
                    PhoneCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageID = table.Column<Guid>(nullable: false),
                    LanguageCode = table.Column<string>(maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Name_de = table.Column<string>(maxLength: 100, nullable: false),
                    Name_fr = table.Column<string>(maxLength: 100, nullable: false),
                    Name_it = table.Column<string>(maxLength: 100, nullable: false),
                    Name_es = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogID = table.Column<Guid>(nullable: false),
                    CurriculumID = table.Column<Guid>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    LogArea = table.Column<int>(maxLength: 100, nullable: false),
                    Page = table.Column<string>(maxLength: 2000, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 1000, nullable: true),
                    UserLanguage = table.Column<string>(maxLength: 2, nullable: true),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    MonthID = table.Column<Guid>(nullable: false),
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
                    Identifier = table.Column<Guid>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vfile", x => x.VfileID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetail",
                columns: table => new
                {
                    PersonalDetailID = table.Column<Guid>(nullable: false),
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
                    MobileNumber = table.Column<string>(maxLength: 16, nullable: false),
                    Citizenship = table.Column<string>(maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<int>(nullable: false),
                    CountryID = table.Column<Guid>(nullable: true),
                    LanguageID = table.Column<Guid>(nullable: true)
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
                        name: "FK_PersonalDetail_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutID = table.Column<Guid>(nullable: false),
                    AcademicTitle = table.Column<string>(maxLength: 100, nullable: true),
                    Slogan = table.Column<string>(maxLength: 4000, nullable: true),
                    Photo = table.Column<string>(type: "varchar(max)", nullable: false),
                    VfileID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutID);
                    table.ForeignKey(
                        name: "FK_About_Vfile_VfileID",
                        column: x => x.VfileID,
                        principalTable: "Vfile",
                        principalColumn: "VfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Child",
                columns: table => new
                {
                    ChildID = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PersonalDetailID = table.Column<Guid>(nullable: true)
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
                    PersonalDetailID = table.Column<Guid>(nullable: false),
                    CountryID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    PersonalDetailID = table.Column<Guid>(nullable: true),
                    AboutID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_About_AboutID",
                        column: x => x.AboutID,
                        principalTable: "About",
                        principalColumn: "AboutID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_PersonalDetail_PersonalDetailID",
                        column: x => x.PersonalDetailID,
                        principalTable: "PersonalDetail",
                        principalColumn: "PersonalDetailID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abroad",
                columns: table => new
                {
                    AbroadID = table.Column<Guid>(nullable: false),
                    CountryID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
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
                        name: "FK_Abroad_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    AwardID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    AwardedFrom = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    AwardedOn = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.AwardID);
                    table.ForeignKey(
                        name: "FK_Award_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    CertificateID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Issuer = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ExpiresOn = table.Column<DateTime>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK_Certificate_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(nullable: false),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CountryID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
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
                        name: "FK_Course_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    CurriculumID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ShortIdentifier = table.Column<string>(nullable: true),
                    FriendlyId = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_Curriculum_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    EducationID = table.Column<Guid>(nullable: false),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    CountryID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Grade = table.Column<float>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
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
                        name: "FK_Education_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    ExperienceID = table.Column<Guid>(nullable: false),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    CountryID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
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
                        name: "FK_Experience_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestID = table.Column<Guid>(nullable: false),
                    InterestName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestID);
                    table.ForeignKey(
                        name: "FK_Interest_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSkill",
                columns: table => new
                {
                    LanguageSkillID = table.Column<Guid>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    LanguageID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSkill", x => x.LanguageSkillID);
                    table.ForeignKey(
                        name: "FK_LanguageSkill_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageSkill_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    ReferenceID = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: false),
                    CountryID = table.Column<Guid>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
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
                        name: "FK_Reference_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(maxLength: 100, nullable: false),
                    Skillset = table.Column<string>(maxLength: 1000, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                    table.ForeignKey(
                        name: "FK_Skill_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialLink",
                columns: table => new
                {
                    SocialLinkID = table.Column<Guid>(nullable: false),
                    SocialPlatform = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLink", x => x.SocialLinkID);
                    table.ForeignKey(
                        name: "FK_SocialLink_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_About_VfileID",
                table: "About",
                column: "VfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Abroad_CountryID",
                table: "Abroad",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Abroad_PersonID",
                table: "Abroad",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Award_PersonID",
                table: "Award",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_PersonID",
                table: "Certificate",
                column: "PersonID");

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
                name: "IX_Course_CountryID",
                table: "Course",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_PersonID",
                table: "Course",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_FriendlyId",
                table: "Curriculum",
                column: "FriendlyId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_PersonID",
                table: "Curriculum",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_CountryID",
                table: "Education",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Education_PersonID",
                table: "Education",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_CountryID",
                table: "Experience",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_PersonID",
                table: "Experience",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_PersonID",
                table: "Interest",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCode",
                table: "Language",
                column: "LanguageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkill_LanguageID",
                table: "LanguageSkill",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkill_PersonID",
                table: "LanguageSkill",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_CurriculumID",
                table: "Log",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AboutID",
                table: "Person",
                column: "AboutID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonalDetailID",
                table: "Person",
                column: "PersonalDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_CountryID",
                table: "PersonalDetail",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetail_LanguageID",
                table: "PersonalDetail",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCountry_CountryID",
                table: "PersonCountry",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_CountryID",
                table: "Reference",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_PersonID",
                table: "Reference",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_PersonID",
                table: "Skill",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_PersonID",
                table: "SocialLink",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Vfile_Identifier",
                table: "Vfile",
                column: "Identifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "LanguageSkill");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "PersonCountry");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SocialLink");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "PersonalDetail");

            migrationBuilder.DropTable(
                name: "Vfile");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
