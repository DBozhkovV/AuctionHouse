using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHouse.Migrations
{
    public partial class addpropertyBidderIdinItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("21c7357c-8b0e-46de-b4e5-be01809da710"));

            migrationBuilder.AddColumn<Guid>(
                name: "BidderId",
                table: "Items",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("8aea20bf-be69-4ec6-ad2e-d6fb4f202e02"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$oHXrNJbRmOhlC5sRy/U/yuj5A72H/8NSPGb4LpQLVC7N4YOZ7C82i", null, null, "admin", 1, "admin", new Guid("faaf78d4-93de-4898-a6f1-a11b4707f810"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8aea20bf-be69-4ec6-ad2e-d6fb4f202e02"));

            migrationBuilder.DropColumn(
                name: "BidderId",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("21c7357c-8b0e-46de-b4e5-be01809da710"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$L03l8jlgv9hxdooxqILWsOIKfHBYU6HrBmtFNn3JRZfH0YE/NWa/i", null, null, "admin", 1, "admin", new Guid("9f4c6141-121d-4ff1-9131-7729be93f9ea"), null });
        }
    }
}
