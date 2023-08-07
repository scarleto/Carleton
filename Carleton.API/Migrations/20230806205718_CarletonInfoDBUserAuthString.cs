using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBUserAuthString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticationString",
                table: "User",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "AuthenticationString", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 6, 21, 57, 16, 611, DateTimeKind.Local).AddTicks(8752), "", new DateTime(2023, 8, 6, 21, 57, 16, 611, DateTimeKind.Local).AddTicks(8784) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthenticationString",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 6, 19, 8, 6, 523, DateTimeKind.Local).AddTicks(1801), new DateTime(2023, 8, 6, 19, 8, 6, 523, DateTimeKind.Local).AddTicks(1831) });
        }
    }
}
