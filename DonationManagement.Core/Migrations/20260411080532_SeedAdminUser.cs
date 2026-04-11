using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "Role", "Username" },
                values: new object[] { 1, "Unknown", "12baraka34@gmail.com", "Ibrahim Admin", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01278988474", "Admin", "Ibrahim" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
