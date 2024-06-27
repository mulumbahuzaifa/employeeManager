using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26f33b29-0f3a-4445-ba88-a429b9c72fe9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd8a651-022c-4b7a-bec2-8ff871ab9c01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a5fd4343-53e9-4687-a725-a6dce5a32ae6", null, "Admin", "ADMIN" },
                    { "df797f3c-b6a1-4ce0-9052-91c14de19536", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5fd4343-53e9-4687-a725-a6dce5a32ae6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df797f3c-b6a1-4ce0-9052-91c14de19536");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26f33b29-0f3a-4445-ba88-a429b9c72fe9", null, "User", "USER" },
                    { "8dd8a651-022c-4b7a-bec2-8ff871ab9c01", null, "Admin", "ADMIN" }
                });
        }
    }
}
