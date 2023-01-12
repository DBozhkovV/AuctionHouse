using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionHouse.Migrations
{
    public partial class addpropertiestoitemmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28862ade-505b-4fa4-9fc5-19c40af37033"));

            migrationBuilder.AddColumn<List<string>>(
                name: "ImagesNames",
                table: "Items",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImageName",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("5ed6000b-5653-486b-abfd-f6cfd1ec7087"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$Mi21L9FGHUGMxecuhCnyw.Rj6J1CCsoFuvnpMGjziOd7pIsfyrUHi", null, null, "admin", 1, "admin", new Guid("f1f8a63d-49eb-439e-ab0d-aec080e6b834"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5ed6000b-5653-486b-abfd-f6cfd1ec7087"));

            migrationBuilder.DropColumn(
                name: "ImagesNames",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MainImageName",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "Email", "FirstName", "IsVerified", "LastName", "Password", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "Username", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("28862ade-505b-4fa4-9fc5-19c40af37033"), 0f, "admin@gmail.com", "Admin", true, "Admin", "$2a$11$DYxaWSyNjwJHZMrsDXCP7O/lqoDAw6nyqIQQa4hUzJbhlk36UCUde", null, null, "admin", 1, "admin", new Guid("14aa9801-4fb3-4ada-b499-d82294dacea9"), null });
        }
    }
}
