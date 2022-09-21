using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityApi.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailUser = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailUser",
                table: "Users",
                column: "EmailUser",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
