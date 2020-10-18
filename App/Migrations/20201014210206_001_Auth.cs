using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitKidCateringApp.Migrations
{
    public partial class _001_Auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorType = table.Column<string>(nullable: true),
                    AuthorId = table.Column<long>(nullable: false),
                    PermissionsType = table.Column<string>(nullable: true),
                    PermissionsJson = table.Column<string>(nullable: true),
                    ResourceType = table.Column<string>(nullable: true),
                    ResourceId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoreRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoreUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    RolesJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorePermissions");

            migrationBuilder.DropTable(
                name: "CoreRoles");

            migrationBuilder.DropTable(
                name: "CoreUsers");
        }
    }
}
