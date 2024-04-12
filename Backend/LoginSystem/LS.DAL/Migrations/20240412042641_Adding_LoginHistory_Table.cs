using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adding_LoginHistory_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_UserId",
                table: "LoginHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginHistories");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b34900f-460b-4e89-9b9d-916e6982ce3d", null, "User", null },
                    { "9d436439-0f9f-415a-bc36-2076fb464429", null, "Admin", null }
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
    }
}
