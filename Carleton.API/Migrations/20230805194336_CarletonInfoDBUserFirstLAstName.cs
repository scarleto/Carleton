using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBUserFirstLAstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "Email", "FirstName", "LastName", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 5, 20, 43, 35, 492, DateTimeKind.Local).AddTicks(3139), "scarleto64@hotmail.com", "Steve", "Carleton", new DateTime(2023, 8, 5, 20, 43, 35, 492, DateTimeKind.Local).AddTicks(3184) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthenticationDate", "Email", "Name", "RegistrationDate" },
                values: new object[] { new DateTime(2023, 8, 5, 20, 39, 6, 917, DateTimeKind.Local).AddTicks(301), "Steve Carleton", "Steve Carleton", new DateTime(2023, 8, 5, 20, 39, 6, 917, DateTimeKind.Local).AddTicks(340) });
        }
    }
}
