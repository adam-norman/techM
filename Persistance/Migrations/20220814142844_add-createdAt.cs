using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class addcreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba2bf0f0-828c-4da4-837f-589ce409bfcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2148221-09e4-476a-93ca-2caabff33713");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24fbe278-676c-4dc7-a3cc-7c5f7c41ab09", "1484ad16-818d-4c98-aef6-6fb373d2476e", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3c12d94-1f1f-49cb-899e-98e4c1add9da", "94fd31ec-a6cc-463d-af04-903f6a9e0539", "administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24fbe278-676c-4dc7-a3cc-7c5f7c41ab09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3c12d94-1f1f-49cb-899e-98e4c1add9da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba2bf0f0-828c-4da4-837f-589ce409bfcd", "cf082c0a-5d80-47a0-aeb2-6ecb0d8e19c9", "administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2148221-09e4-476a-93ca-2caabff33713", "703e1b1d-44b7-4655-95c3-7805efa93d62", "employee", "EMPLOYEE" });
        }
    }
}
