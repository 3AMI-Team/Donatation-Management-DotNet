using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class FreshStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Distributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DistributionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    HandledByEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributions_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distributions_Donations_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distributions_Employees_HandledByEmployeeId",
                        column: x => x.HandledByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[,]
                {
                    { 101, "Student sponsorship and school supplies", "Education" },
                    { 102, "Medications, surgeries, and medical equipment", "Healthcare" },
                    { 103, "Food packages and meal distributions", "Food Security" },
                    { 104, "Home renovation and clean water access", "Housing" },
                    { 105, "Disaster response and urgent assistance", "Emergency Relief" }
                });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "RegisterDate", "Type" },
                values: new object[,]
                {
                    { 101, "Los Angeles, USA", "james.w@example.com", "James Wilson", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550101", new DateTime(2025, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Individual" },
                    { 102, "Chicago, USA", "mary.j@example.com", "Mary Johnson", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550102", new DateTime(2025, 2, 5, 12, 30, 0, 0, DateTimeKind.Utc), "Individual" },
                    { 103, "San Francisco, USA", "donations@globaltech.com", "Global Tech Corp", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550300", new DateTime(2025, 3, 12, 9, 15, 0, 0, DateTimeKind.Utc), "Corporate" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Ismailia, Egypt", "12baraka34@gmail.com", "Ibrahim Nasser", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01278988474", "Admin", "Ibrahim" },
                    { 2, "New York, USA", "sarah.c@example.com", "Sarah Connor", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550199", "Supervisor", "SarahC" },
                    { 3, "London, UK", "john.doe@example.com", "John Doe", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550188", "FieldWorker", "JohnD" }
                });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Address", "CategoryId", "Description", "Name", "Phone", "RegistDate", "Status", "SupervisorId" },
                values: new object[,]
                {
                    { 201, "Detroit, USA", 102, "Needs a new wheelchair for educational mobility", "Alice Peterson", "+12025550501", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Approved", 2 },
                    { 202, "Houston, USA", 103, "Monthly food support for a family of 6", "Robert's Family", "+12025550502", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Pending Review", 2 },
                    { 203, "Nairobi, Kenya", 104, "Roof repairs for the main dormitory", "St. Paul Orphanage", "+12025550503", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Approved", 2 }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "DonorId", "Status", "SupervisorId" },
                values: new object[,]
                {
                    { 301, 5000m, 102, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Annual CSR contribution for Healthcare", 103, "Completed", 2 },
                    { 302, 1000m, 103, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Personal gift for food drive", 101, "Completed", 2 },
                    { 303, 2500m, 105, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Emergency relief fund contribution", 102, "Pending", 2 }
                });

            migrationBuilder.InsertData(
                table: "Distributions",
                columns: new[] { "Id", "Amount", "CaseId", "DistributionDate", "DonationId", "HandledByEmployeeId", "Notes", "Status" },
                values: new object[,]
                {
                    { 401, 3000m, 201, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 301, 3, "Funding provided for wheelchair procurement", "Completed" },
                    { 402, 500m, 202, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), 302, 3, "First monthly food basket distribution", "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CategoryId",
                table: "Cases",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_SupervisorId",
                table: "Cases",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_CaseId",
                table: "Distributions",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_DonationId",
                table: "Distributions",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_HandledByEmployeeId",
                table: "Distributions",
                column: "HandledByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CategoryId",
                table: "Donations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_SupervisorId",
                table: "Donations",
                column: "SupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distributions");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
