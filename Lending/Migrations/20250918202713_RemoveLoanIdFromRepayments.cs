using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lending.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLoanIdFromRepayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1️⃣ Drop foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Repayments_Loans_LoanId",
                table: "Repayments");

            // 2️⃣ Drop index on LoanId
            migrationBuilder.DropIndex(
                name: "IX_Repayments_LoanId",
                table: "Repayments");

            // 3️⃣ Drop column itself
            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Repayments");

            // Update seeded data if necessary
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[]
                {
            DateTime.UtcNow,
            BCrypt.Net.BCrypt.HashPassword("Admin@123")
                });
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Repayments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_LoanId",
                table: "Repayments",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repayments_Loans_LoanId",
                table: "Repayments",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[]
                {
            new DateTime(2025, 9, 18, 7, 18, 58, 420, DateTimeKind.Utc),
            "$2a$11$HQeqXgpReDZHeEGRJ5b3H.Gl2f2t8k6VjzBGWUk7rbztp0u96BmlS"
                });
        }


    }
}
