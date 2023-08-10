using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBInitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 10, 12, 17, 20, 212, DateTimeKind.Local).AddTicks(50), new DateTime(2023, 8, 10, 12, 17, 20, 212, DateTimeKind.Local).AddTicks(78) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 6, 21, 57, 16, 611, DateTimeKind.Local).AddTicks(8752), new DateTime(2023, 8, 6, 21, 57, 16, 611, DateTimeKind.Local).AddTicks(8784) });
        }
    }
}
