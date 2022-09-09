using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kodlama.io.Devs.Persistence.Migrations
{
    public partial class Mig_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GithubProfiles_AppUsers_AppUserId",
                table: "GithubProfiles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "GithubProfiles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GithubProfiles_AppUserId",
                table: "GithubProfiles",
                newName: "IX_GithubProfiles_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GithubProfiles_Users_UserId",
                table: "GithubProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GithubProfiles_Users_UserId",
                table: "GithubProfiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GithubProfiles",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GithubProfiles_UserId",
                table: "GithubProfiles",
                newName: "IX_GithubProfiles_AppUserId");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GithubProfiles_AppUsers_AppUserId",
                table: "GithubProfiles",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
