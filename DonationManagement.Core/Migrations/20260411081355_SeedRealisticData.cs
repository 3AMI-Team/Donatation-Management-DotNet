using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonationManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealisticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[,]
                {
                    { 101, "كفالة طلاب ومستلزمات دراسية", "تعليم" },
                    { 102, "أدوية وعمليات جراحية", "صحة" },
                    { 103, "مواد غذائية ووجبات إطعام", "غذاء" },
                    { 104, "بناء بيوت وتوصيل مياه", "إعمار" },
                    { 105, "مساعدة الشباب غير القادرين", "تجهيز عرائس" }
                });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "RegisterDate" },
                values: new object[,]
                {
                    { 101, "ahmed.a@example.com", "أحمد عبدالله", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01011112222", new DateTime(2025, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 102, "sara.g@example.com", "سارة جمال", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01122223333", new DateTime(2025, 2, 5, 12, 30, 0, 0, DateTimeKind.Utc) },
                    { 103, "mahmoud.h@example.com", "محمود حسن", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01233334444", new DateTime(2025, 3, 12, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { 104, "nourhan.t@example.com", "نورهان طارق", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01544445555", new DateTime(2025, 4, 18, 14, 45, 0, 0, DateTimeKind.Utc) },
                    { 105, "khaled.s@example.com", "خالد سعيد", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01055556666", new DateTime(2025, 5, 20, 16, 20, 0, 0, DateTimeKind.Utc) },
                    { 106, "yasmeen.k@example.com", "ياسمين كمال", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01166667777", new DateTime(2025, 6, 22, 11, 10, 0, 0, DateTimeKind.Utc) },
                    { 107, "mostafa.f@example.com", "مصطفى فهمي", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01277778888", new DateTime(2025, 7, 30, 8, 50, 0, 0, DateTimeKind.Utc) },
                    { 108, "raghda.s@example.com", "رغدة سمير", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01588889999", new DateTime(2025, 8, 14, 13, 25, 0, 0, DateTimeKind.Utc) },
                    { 109, "omar.f@example.com", "عمر فاروق", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01099990000", new DateTime(2025, 9, 5, 15, 55, 0, 0, DateTimeKind.Utc) },
                    { 110, "laila.a@example.com", "ليلى عبد الرحمن", "$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W", "01100001111", new DateTime(2025, 10, 1, 10, 5, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name" },
                values: new object[] { "Ismailia , Egypt", "Ibrahim Nasser" });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "DonorId", "Status", "SupervisorId" },
                values: new object[,]
                {
                    { 101, 3000m, 101, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "مساعدة طالب جامعي متعثر في مصاريف الكلية", 101, "Open", 1 },
                    { 102, 15000m, 102, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "عملية قسطرة قلب لمريض مسن", 102, "In Progress", 1 },
                    { 103, 500m, 103, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "شنطة غذاء لأسرة فقيرة", 103, "Closed", 1 },
                    { 104, 7000m, 104, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "توصيل مياه شرب لقرية نائية", 104, "Open", 1 },
                    { 105, 12000m, 105, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "جهاز كهربائي لتجهيز عروسة يتيمة", 105, "Open", 1 },
                    { 106, 2500m, 102, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "أدوية شهرية لمريض سكري", 106, "In Progress", 1 },
                    { 107, 1500m, 101, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "حقائب مدرسية وعمل أنشطة لأطفال أيتام", 107, "Closed", 1 },
                    { 108, 40000m, 104, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "بناء سقف منزل آيل للسقوط", 108, "Open", 1 },
                    { 109, 800m, 103, new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "دعم غذائي لأسرة من 5 أفراد", 109, "Closed", 1 },
                    { 110, 6000m, 105, new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "مساعدة عروسين في تأسيس المنزل", 110, "In Progress", 1 },
                    { 111, 3500m, 103, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "ملابس شتوية للأسر المتعففة", 101, "Open", 1 },
                    { 112, 20000m, 102, new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), "عملية شفط سوائل على المخ للطفلة خديجة", 102, "Open", 1 },
                    { 113, 4500m, 104, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Utc), "سداد دين عن غارمين", 103, "In Progress", 1 },
                    { 114, 1800m, 101, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "دعم مصروفات مدارس لأسرة أيتام", 104, "Open", 1 },
                    { 115, 9000m, 105, new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), "شراء ثلاجة وغسالة لعروسة", 105, "Open", 1 },
                    { 116, 1200m, 102, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), "تبرع لبنك الدم القومي", 106, "Closed", 1 },
                    { 117, 50000m, 104, new DateTime(2026, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ترميم دار إيواء للأطفال الرضع", 107, "In Progress", 1 },
                    { 118, 2500m, 103, new DateTime(2026, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), "وجبات إفطار صائم متكاملة", 108, "Closed", 1 },
                    { 119, 4200m, 101, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "أدوات ميكانيكا لشاب ليبدأ مشروع مستقل", 109, "Open", 1 },
                    { 120, 8000m, 102, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), "علاج كيمائي لمرضى الحروق", 110, "Open", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name" },
                values: new object[] { "Unknown", "Ibrahim Admin" });
        }
    }
}
