using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealisticEnglishData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 101,
                column: "Description",
                value: "Sponsorship for a struggling university student");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 102,
                column: "Description",
                value: "Heart surgery for an elderly patient");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 103,
                column: "Description",
                value: "Monthly food basket for a family in need");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 104,
                column: "Description",
                value: "Installing a clean water well in a rural village");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Amount", "CategoryId", "Description" },
                values: new object[] { 1200m, 103, "Winter clothing drive for orphanages" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 106,
                column: "Description",
                value: "Monthly insulin medication for diabetic patients");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 107,
                column: "Description",
                value: "School uniforms and backpacks for 50 kids");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Amount", "Description" },
                values: new object[] { 4000m, "Repairing the roof of a collapsed house" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 109,
                column: "Description",
                value: "Emergency food supply for a refugee family");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 104, "Furniture and basics for a newly built shelter" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 101, "Laptops for high-achieving low-income students" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 112,
                column: "Description",
                value: "Urgent diagnostic center for specialized tests");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 105, "Clearing debts for single mothers" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 114,
                column: "Description",
                value: "Online course subscriptions for skill dev");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 104, "Solar panel installation for a community center" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 116,
                column: "Description",
                value: "Wheelchairs for disabled athletes");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Amount", "CategoryId", "Description" },
                values: new object[] { 5000m, 101, "Restoration of a local library" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 118,
                column: "Description",
                value: "Nutrition kits for pregnant women");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 105, "Vocational training for unemployed youth" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 120,
                column: "Description",
                value: "Rehabilitation center for post-surgery recovery");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Description", "Type" },
                values: new object[] { "Student sponsorship and school supplies", "Education" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Description", "Type" },
                values: new object[] { "Medications, surgeries, and medical equipment", "Healthcare" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Description", "Type" },
                values: new object[] { "Food packages and meal distributions", "Food Security" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Description", "Type" },
                values: new object[] { "Home renovation and clean water access", "Housing" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Description", "Type" },
                values: new object[] { "Disaster response and urgent assistance", "Emergency Relief" });

            migrationBuilder.InsertData(
                table: "Distributions",
                columns: new[] { "Id", "Amount", "CaseId", "DistributionDate", "HandledByEmployeeId", "Recipient", "Status" },
                values: new object[,]
                {
                    { 101, 500m, 103, new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Sarah Jenkins", "Completed" },
                    { 102, 1500m, 107, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 1, "City General Hospital", "Completed" },
                    { 103, 800m, 109, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Local Refugee Center", "Completed" },
                    { 104, 5000m, 102, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Health Services Dept", "Processing" },
                    { 105, 2000m, 106, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Diabetic Care Clinic", "Completed" }
                });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "james.w@example.com", "James Wilson", "+12025550101" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "mary.j@example.com", "Mary Johnson", "+12025550102" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "robert.s@example.com", "Robert Smith", "+12025550103" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "patricia.b@example.com", "Patricia Brown", "+12025550104" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "michael.d@example.com", "Michael Davis", "+12025550105" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "linda.m@example.com", "Linda Miller", "+12025550106" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "david.t@example.com", "David Taylor", "+12025550107" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "elizabeth.a@example.com", "Elizabeth Anderson", "+12025550108" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "richard.t@example.com", "Richard Thomas", "+12025550109" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "barbara.j@example.com", "Barbara Jackson", "+12025550110" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Ismailia, Egypt");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "Role", "Username" },
                values: new object[,]
                {
                    { 2, "New York, USA", "sarah.c@example.com", "Sarah Connor", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550199", "Supervisor", "SarahC" },
                    { 3, "London, UK", "john.doe@example.com", "John Doe", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "+12025550188", "FieldWorker", "JohnD" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Distributions",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 101,
                column: "Description",
                value: "مساعدة طالب جامعي متعثر في مصاريف الكلية");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 102,
                column: "Description",
                value: "عملية قسطرة قلب لمريض مسن");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 103,
                column: "Description",
                value: "شنطة غذاء لأسرة فقيرة");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 104,
                column: "Description",
                value: "توصيل مياه شرب لقرية نائية");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Amount", "CategoryId", "Description" },
                values: new object[] { 12000m, 105, "جهاز كهربائي لتجهيز عروسة يتيمة" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 106,
                column: "Description",
                value: "أدوية شهرية لمريض سكري");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 107,
                column: "Description",
                value: "حقائب مدرسية وعمل أنشطة لأطفال أيتام");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Amount", "Description" },
                values: new object[] { 40000m, "بناء سقف منزل آيل للسقوط" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 109,
                column: "Description",
                value: "دعم غذائي لأسرة من 5 أفراد");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 105, "مساعدة عروسين في تأسيس المنزل" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 103, "ملابس شتوية للأسر المتعففة" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 112,
                column: "Description",
                value: "عملية شفط سوائل على المخ للطفلة خديجة");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 104, "سداد دين عن غارمين" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 114,
                column: "Description",
                value: "دعم مصروفات مدارس لأسرة أيتام");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 105, "شراء ثلاجة وغسالة لعروسة" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 116,
                column: "Description",
                value: "تبرع لبنك الدم القومي");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Amount", "CategoryId", "Description" },
                values: new object[] { 50000m, 104, "ترميم دار إيواء للأطفال الرضع" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 118,
                column: "Description",
                value: "وجبات إفطار صائم متكاملة");

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 101, "أدوات ميكانيكا لشاب ليبدأ مشروع مستقل" });

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 120,
                column: "Description",
                value: "علاج كيمائي لمرضى الحروق");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Description", "Type" },
                values: new object[] { "كفالة طلاب ومستلزمات دراسية", "تعليم" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Description", "Type" },
                values: new object[] { "أدوية وعمليات جراحية", "صحة" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Description", "Type" },
                values: new object[] { "مواد غذائية ووجبات إطعام", "غذاء" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Description", "Type" },
                values: new object[] { "بناء بيوت وتوصيل مياه", "إعمار" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Description", "Type" },
                values: new object[] { "مساعدة الشباب غير القادرين", "تجهيز عرائس" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "ahmed.a@example.com", "أحمد عبدالله", "01011112222" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "sara.g@example.com", "سارة جمال", "01122223333" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "mahmoud.h@example.com", "محمود حسن", "01233334444" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "nourhan.t@example.com", "نورهان طارق", "01544445555" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "khaled.s@example.com", "خالد سعيد", "01055556666" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "yasmeen.k@example.com", "ياسمين كمال", "01166667777" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "mostafa.f@example.com", "مصطفى فهمي", "01277778888" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "raghda.s@example.com", "رغدة سمير", "01588889999" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "omar.f@example.com", "عمر فاروق", "01099990000" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "laila.a@example.com", "ليلى عبد الرحمن", "01100001111" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Ismailia , Egypt");
        }
    }
}
