using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class addCreatedAtCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24fbe278-676c-4dc7-a3cc-7c5f7c41ab09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3c12d94-1f1f-49cb-899e-98e4c1add9da");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "296e93c6-5701-47c5-8654-a86232ef739f", "69e9e30d-0e01-4aeb-914e-8ca9e2fc69d3", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36f585dc-7c1f-4c79-9e10-02fb8538334d", "ecfbecea-081f-4391-8774-2289551beb72", "administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "296e93c6-5701-47c5-8654-a86232ef739f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36f585dc-7c1f-4c79-9e10-02fb8538334d");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24fbe278-676c-4dc7-a3cc-7c5f7c41ab09", "1484ad16-818d-4c98-aef6-6fb373d2476e", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3c12d94-1f1f-49cb-899e-98e4c1add9da", "94fd31ec-a6cc-463d-af04-903f6a9e0539", "administrator", "ADMINISTRATOR" });
        }
    }
}
