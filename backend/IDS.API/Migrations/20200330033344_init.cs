using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDS.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "idsapp");

            migrationBuilder.CreateTable(
                name: "Identity",
                schema: "idsapp",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    Modifier = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    ModifiedTime = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityGrant",
                schema: "idsapp",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityGrant", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "idsapp",
                table: "Identity",
                columns: new[] { "Id", "CreatedTime", "Creator", "Email", "ModifiedTime", "Modifier", "Name", "Password", "Phone", "Username" },
                values: new object[] { "M8fqzEmNwrzyEFk", 1585539223000L, "admin", "bamboo@bamboo.com", 1585539223000L, "admin", "Admin", "e10adc3949ba59abbe56e057f20f883e", "157", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity",
                schema: "idsapp");

            migrationBuilder.DropTable(
                name: "IdentityGrant",
                schema: "idsapp");
        }
    }
}
