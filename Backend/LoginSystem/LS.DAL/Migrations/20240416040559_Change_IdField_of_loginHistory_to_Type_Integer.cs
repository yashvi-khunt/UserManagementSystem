using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change_IdField_of_loginHistory_to_Type_Integer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d842f06f-88f4-4fb9-9594-7350be36a3d2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5f26864b-da74-418d-aa21-22be636c2827", "d395681f-e727-42ea-a6eb-b18e9d3216d4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f26864b-da74-418d-aa21-22be636c2827");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d395681f-e727-42ea-a6eb-b18e9d3216d4");

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories");

            // Drop the current column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "LoginHistories");

            // Recreate the column with the desired properties
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LoginHistories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add a new primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46492ea9-859f-4379-a91e-d67e39ab8c2a", null, "User", null },
                    { "76b642a9-3291-4a4b-9dcc-0fa838b6fa9b", null, "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "IsActivated", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5d52d6ad-4844-4943-a4fb-4aea64f2b577", 0, "cf7b766e-7437-4043-90d1-69bb496eff85", new DateTime(2024, 4, 16, 9, 35, 59, 121, DateTimeKind.Local).AddTicks(3591), null, true, null, true, null, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEIhBVSg5p/NLoHtl2vv5zbbmH2BrSz9aaoO9pUZUXTJ9GsOwrWAP2wGGQaCE63c5gg==", null, false, "573ca3e8-608f-4f8c-b8f2-7ceb03ea7f97", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "76b642a9-3291-4a4b-9dcc-0fa838b6fa9b", "5d52d6ad-4844-4943-a4fb-4aea64f2b577" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46492ea9-859f-4379-a91e-d67e39ab8c2a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "76b642a9-3291-4a4b-9dcc-0fa838b6fa9b", "5d52d6ad-4844-4943-a4fb-4aea64f2b577" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76b642a9-3291-4a4b-9dcc-0fa838b6fa9b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d52d6ad-4844-4943-a4fb-4aea64f2b577");

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories");

            // Drop the current column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "LoginHistories");

            // Recreate the column with the previous properties
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "LoginHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "")
                .Annotation("SqlServer:Identity", "1, 1");

            // Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f26864b-da74-418d-aa21-22be636c2827", null, "Admin", null },
                    { "d842f06f-88f4-4fb9-9594-7350be36a3d2", null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "IsActivated", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d395681f-e727-42ea-a6eb-b18e9d3216d4", 0, "9c8394bb-fa9a-4420-a787-b93655a64d1f", new DateTime(2024, 4, 12, 9, 56, 40, 645, DateTimeKind.Local).AddTicks(8548), null, true, null, true, null, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEAIo+AzN6RhJdn6KokJmIxkCi4zsMI+ieGrV3QH439iRdgs2PHk8Sa893FYqEJHjKw==", null, false, "6c2d2ebf-572f-414f-855b-d695453a6589", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5f26864b-da74-418d-aa21-22be636c2827", "d395681f-e727-42ea-a6eb-b18e9d3216d4" });
        }
    }
}
