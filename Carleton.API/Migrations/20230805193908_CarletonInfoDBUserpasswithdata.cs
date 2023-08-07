using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carleton.API.Migrations
{
    public partial class CarletonInfoDBUserpasswithdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AuthenticationDate", "Email", "IsAuthenticated", "Name", "PasswordHashed", "RegistrationDate" },
                values: new object[] { 1, new DateTime(2023, 8, 5, 20, 39, 6, 917, DateTimeKind.Local).AddTicks(301), "Steve Carleton", true, "Steve Carleton", "e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a", new DateTime(2023, 8, 5, 20, 39, 6, 917, DateTimeKind.Local).AddTicks(340) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
