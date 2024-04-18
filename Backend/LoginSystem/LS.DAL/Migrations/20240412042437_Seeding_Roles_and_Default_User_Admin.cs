using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seeding_Roles_and_Default_User_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b34900f-460b-4e89-9b9d-916e6982ce3d", null, "User", "USER" },
                    { "9d436439-0f9f-415a-bc36-2076fb464429", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "IsActivated", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "86f878e6-6390-4eca-b66f-81c5146a073d", 0, "c7823155-3676-48e2-aca9-9fe3ca116337", new DateTime(2024, 4, 12, 9, 54, 37, 174, DateTimeKind.Local).AddTicks(1470), null, true, null, true, null, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEMAhirPuX0cXAGzPN/RnUFCpvS5ldfUqztG6Bmu4NSDL/g3MUpuyhvNbJBFB06C7Xg==", null, false, "68d16198-72a4-4635-845e-e950e83d0b0c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9d436439-0f9f-415a-bc36-2076fb464429", "86f878e6-6390-4eca-b66f-81c5146a073d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b34900f-460b-4e89-9b9d-916e6982ce3d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9d436439-0f9f-415a-bc36-2076fb464429", "86f878e6-6390-4eca-b66f-81c5146a073d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d436439-0f9f-415a-bc36-2076fb464429");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86f878e6-6390-4eca-b66f-81c5146a073d");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
