using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistency.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_About_Vfile_VfileID",
                table: "About");

            migrationBuilder.DropIndex(
                name: "IX_About_VfileID",
                table: "About");

            migrationBuilder.DropColumn(
                name: "VfileID",
                table: "About");

            migrationBuilder.AddColumn<long>(
                name: "AboutID",
                table: "Vfile",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableCVDownload",
                table: "Publication",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableDocumentsDownload",
                table: "Publication",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Vfile_AboutID",
                table: "Vfile",
                column: "AboutID",
                unique: true,
                filter: "[AboutID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vfile_About_AboutID",
                table: "Vfile",
                column: "AboutID",
                principalTable: "About",
                principalColumn: "AboutID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vfile_About_AboutID",
                table: "Vfile");

            migrationBuilder.DropIndex(
                name: "IX_Vfile_AboutID",
                table: "Vfile");

            migrationBuilder.DropColumn(
                name: "AboutID",
                table: "Vfile");

            migrationBuilder.DropColumn(
                name: "EnableCVDownload",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "EnableDocumentsDownload",
                table: "Publication");

            migrationBuilder.AddColumn<Guid>(
                name: "VfileID",
                table: "About",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_About_VfileID",
                table: "About",
                column: "VfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_About_Vfile_VfileID",
                table: "About",
                column: "VfileID",
                principalTable: "Vfile",
                principalColumn: "VfileID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
