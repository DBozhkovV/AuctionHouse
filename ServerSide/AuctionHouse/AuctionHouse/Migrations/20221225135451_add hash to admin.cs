using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHouse.Migrations
{
    public partial class addhashtoadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3514705-0829-4395-9d01-4701ce497796"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("28862ade-505b-4fa4-9fc5-19c40af37033"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$DYxaWSyNjwJHZMrsDXCP7O/lqoDAw6nyqIQQa4hUzJbhlk36UCUde", null, null, "admin", 1, "admin", new Guid("14aa9801-4fb3-4ada-b499-d82294dacea9"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28862ade-505b-4fa4-9fc5-19c40af37033"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("f3514705-0829-4395-9d01-4701ce497796"), 0f, "admin@gmail.com", "Admin", true, "Admin", "admin", null, null, "admin", 1, "admin", new Guid("73bc4a00-d40b-4f93-a6aa-72b28c7018e5"), null });
        }
    }
}
