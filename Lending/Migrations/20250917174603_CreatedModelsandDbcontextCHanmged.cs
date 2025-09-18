using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lending.Migrations
{
    /// <inheritdoc />
    public partial class CreatedModelsandDbcontextCHanmged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_LoanApplications_LoanApplicationId",
                table: "Documents");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 17, 17, 46, 3, 7, DateTimeKind.Utc).AddTicks(691), "$2a$11$o5DuoMAzo.oL7fTkv6zLBuSdRYqS60iN1VP6lfMHNXstsNp6aHJmO" });

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_LoanApplications_LoanApplicationId",
                table: "Documents",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "LoanApplicationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_LoanApplications_LoanApplicationId",
                table: "Documents");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 17, 17, 40, 52, 158, DateTimeKind.Utc).AddTicks(4967), "$2a$11$AlMa5nr3FizOHXVf9/EISu48WAbKBqT8p4w.ZlEH5zgW.d7SqFS5K" });

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_LoanApplications_LoanApplicationId",
                table: "Documents",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "LoanApplicationId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
