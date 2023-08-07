using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBUserPasswordChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHashed",
                table: "User",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 6, 19, 8, 6, 523, DateTimeKind.Local).AddTicks(1801), new DateTime(2023, 8, 6, 19, 8, 6, 523, DateTimeKind.Local).AddTicks(1831) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "PasswordHashed");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 5, 20, 43, 35, 492, DateTimeKind.Local).AddTicks(3139), new DateTime(2023, 8, 5, 20, 43, 35, 492, DateTimeKind.Local).AddTicks(3184) });
        }
    }
}
