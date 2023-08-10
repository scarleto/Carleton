using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBInitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 10, 11, 2, 1, 79, DateTimeKind.Local).AddTicks(2216), new DateTime(2023, 8, 10, 11, 2, 1, 79, DateTimeKind.Local).AddTicks(2267) });
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
