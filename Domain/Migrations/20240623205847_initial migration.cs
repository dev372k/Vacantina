using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedOn", "IsActive", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2771), true, false, "Permission.User", new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2781) },
                    { 2, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2783), true, false, "Permission.Setting", new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2784) },
                    { 3, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2785), true, false, "Permission.Role", new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2785) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "IsActive", "IsDeleted", "Name", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2877), true, false, "Owner", new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2877) });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreatedOn", "IsActive", "IsAllowed", "IsDeleted", "PermissionId", "RoleId", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2890), true, true, false, 1, 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2890) },
                    { 2, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2892), true, true, false, 2, 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2892) },
                    { 3, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2893), true, true, false, 3, 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2894) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "IsActive", "IsDeleted", "Name", "PasswordHash", "RoleId", "UpdatedOn" },
                values: new object[] { 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2905), "sa@mailinator.com", true, false, "Owner", "$2a$11$LF.jO5445FGwpoGW9PGgR.TKNymOmleYKS2vPhTcpqanjMM9stbIC", 1, new DateTime(2024, 6, 24, 1, 58, 47, 278, DateTimeKind.Local).AddTicks(2905) });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
