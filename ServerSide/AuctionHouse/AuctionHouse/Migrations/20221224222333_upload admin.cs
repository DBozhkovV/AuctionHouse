using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHouse.Migrations
{
    public partial class uploadadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("f3514705-0829-4395-9d01-4701ce497796"), 0f, "admin@gmail.com", "Admin", true, "Admin", "admin", null, null, "admin", 1, "admin", new Guid("73bc4a00-d40b-4f93-a6aa-72b28c7018e5"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3514705-0829-4395-9d01-4701ce497796"));
        }
    }
}
