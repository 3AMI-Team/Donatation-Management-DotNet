using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToDonor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Donors");
        }
    }
}
