using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerSample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMockUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("2d595a7b-697c-407e-b425-0d6a65e2e1b6"), "adm@adm.com", "adm@123", "Admin" },
                    { new Guid("b3b5c683-b261-4684-a7de-d16bdca73f8e"), "user1@user.com", "user@222", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2d595a7b-697c-407e-b425-0d6a65e2e1b6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3b5c683-b261-4684-a7de-d16bdca73f8e"));
        }
    }
}
