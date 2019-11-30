using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistency.Data.Migrations
{
    public partial class vNext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<byte[]>(nullable: false),
                    Slogan = table.Column<string>(nullable: true),
                    CV = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    StreetNo = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    AboutID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Persons_About_AboutID",
                        column: x => x.AboutID,
                        principalTable: "About",
                        principalColumn: "AboutID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    AwardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AwardedFrom = table.Column<string>(nullable: false),
                    AwardedOn = table.Column<DateTime>(nullable: false),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.AwardID);
                    table.ForeignKey(
                        name: "FK_Award_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    CurriculumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<Guid>(nullable: false),
                    FriendlyId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_Curriculum_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestID);
                    table.ForeignKey(
                        name: "FK_Interest_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                    table.ForeignKey(
                        name: "FK_Language_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                    table.ForeignKey(
                        name: "FK_Skill_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialLink",
                columns: table => new
                {
                    SocialLinkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialPlatform = table.Column<int>(nullable: false),
                    Hyperlink = table.Column<string>(nullable: false),
                    PersonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLink", x => x.SocialLinkID);
                    table.ForeignKey(
                        name: "FK_SocialLink_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Award_PersonID",
                table: "Award",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_FriendlyId",
                table: "Curriculum",
                column: "FriendlyId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_Identifier",
                table: "Curriculum",
                column: "Identifier");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_PersonID",
                table: "Curriculum",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_PersonID",
                table: "Interest",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Language_PersonID",
                table: "Language",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AboutID",
                table: "Persons",
                column: "AboutID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_PersonID",
                table: "Skill",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_PersonID",
                table: "SocialLink",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SocialLink");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "About");
        }
    }
}
