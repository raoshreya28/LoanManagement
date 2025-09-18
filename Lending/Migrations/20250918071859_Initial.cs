using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lending.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanSchemes",
                columns: table => new
                {
                    LoanSchemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanSchemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumTenureMonths = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanSchemes", x => x.LoanSchemeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AadhaarNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerIFSC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanAdmins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AdminDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAdmins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_LoanAdmins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanOfficers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoanOfficerBranch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoanOfficerCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CurrentAssignments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOfficers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_LoanOfficers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneratedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_LoanAdmins_GeneratedById",
                        column: x => x.GeneratedById,
                        principalTable: "LoanAdmins",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    LoanApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LoanSchemeId = table.Column<int>(type: "int", nullable: true),
                    LoanOfficerId = table.Column<int>(type: "int", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenureMonths = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentUrls = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.LoanApplicationId);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplications_LoanOfficers_LoanOfficerId",
                        column: x => x.LoanOfficerId,
                        principalTable: "LoanOfficers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_LoanApplications_LoanSchemes_LoanSchemeId",
                        column: x => x.LoanSchemeId,
                        principalTable: "LoanSchemes",
                        principalColumn: "LoanSchemeId");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "LoanApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: true),
                    PrincipalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenureMonths = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "LoanApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "LoanApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Repayments",
                columns: table => new
                {
                    RepaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: false),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LoanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repayments", x => x.RepaymentId);
                    table.ForeignKey(
                        name: "FK_Repayments_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "LoanApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repayments_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "LoanId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CreatedAt", "IsActive", "PasswordHash", "Role", "UpdatedAt", "UserEmail", "UserName", "UserPhone" },
                values: new object[] { 1, "Head Office", new DateTime(2025, 9, 18, 7, 18, 58, 420, DateTimeKind.Utc).AddTicks(7197), true, "$2a$11$HQeqXgpReDZHeEGRJ5b3H.Gl2f2t8k6VjzBGWUk7rbztp0u96BmlS", 0, null, "admin@lending.com", "Default Admin", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "LoanAdmins",
                columns: new[] { "UserId", "AdminDepartment" },
                values: new object[] { 1, "Finance" });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CustomerId",
                table: "Documents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LoanApplicationId",
                table: "Documents",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_CustomerId",
                table: "LoanApplications",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_LoanOfficerId",
                table: "LoanApplications",
                column: "LoanOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_LoanSchemeId",
                table: "LoanApplications",
                column: "LoanSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanApplicationId",
                table: "Loans",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LoanApplicationId",
                table: "Notifications",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_LoanApplicationId",
                table: "Repayments",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_LoanId",
                table: "Repayments",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GeneratedById",
                table: "Reports",
                column: "GeneratedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Repayments");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "LoanAdmins");

            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LoanOfficers");

            migrationBuilder.DropTable(
                name: "LoanSchemes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
