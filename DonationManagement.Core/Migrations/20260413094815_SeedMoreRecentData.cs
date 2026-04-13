using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreRecentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "DonorId", "Status", "SupervisorId" },
                values: new object[] { 123, 3000m, 102, new DateTime(2026, 4, 13, 10, 0, 0, 0, DateTimeKind.Utc), "Medical kits for rural area", 101, "Open", 1 });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "RegisterDate" },
                values: new object[,]
                {
                    { 111, "william.w@example.com", "William White", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550111", new DateTime(2026, 4, 12, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 112, "susan.g@example.com", "Susan Green", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550112", new DateTime(2026, 4, 13, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "DonorId", "Status", "SupervisorId" },
                values: new object[,]
                {
                    { 121, 5500m, 104, new DateTime(2026, 4, 12, 10, 30, 0, 0, DateTimeKind.Utc), "Clean Water for primary school", 111, "Open", 1 },
                    { 122, 2500m, 103, new DateTime(2026, 4, 13, 9, 15, 0, 0, DateTimeKind.Utc), "Daily Bread for Homeless", 112, "Open", 1 }
                });

            migrationBuilder.InsertData(
                table: "Distributions",
                columns: new[] { "Id", "Amount", "CaseId", "DistributionDate", "HandledByEmployeeId", "Recipient", "Status" },
                values: new object[,]
                {
                    { 106, 1200m, 121, new DateTime(2026, 4, 12, 11, 0, 0, 0, DateTimeKind.Utc), 1, "Alexandria School", "Completed" },
                    { 107, 2000m, 122, new DateTime(2026, 4, 13, 9, 30, 0, 0, DateTimeKind.Utc), 1, "Public Shelter", "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 112);
        }
    }
}
