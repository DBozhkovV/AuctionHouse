using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHouse.Migrations
{
    public partial class addcategorypropertytotheitemmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5ed6000b-5653-486b-abfd-f6cfd1ec7087"));

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("21c7357c-8b0e-46de-b4e5-be01809da710"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$L03l8jlgv9hxdooxqILWsOIKfHBYU6HrBmtFNn3JRZfH0YE/NWa/i", null, null, "admin", 1, "admin", new Guid("9f4c6141-121d-4ff1-9131-7729be93f9ea"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("21c7357c-8b0e-46de-b4e5-be01809da710"));

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("5ed6000b-5653-486b-abfd-f6cfd1ec7087"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$Mi21L9FGHUGMxecuhCnyw.Rj6J1CCsoFuvnpMGjziOd7pIsfyrUHi", null, null, "admin", 1, "admin", new Guid("f1f8a63d-49eb-439e-ab0d-aec080e6b834"), null });
        }
    }
}
