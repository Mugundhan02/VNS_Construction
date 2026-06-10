using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildManager.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_DoorNoAndStreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_PinCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_CityOrTalukName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_DistrictAndStateName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_EmailId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactInfo_WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BankDetails_AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BankBranch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BankDetails_IfscCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IdentityDetails_PanCardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_TinNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_CstNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_AadhaarNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EstimateDetails_Unit = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    EstimateDetails_Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    EstimateDetails_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_DoorNoAndStreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_PinCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_CityOrTalukName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_DistrictAndStateName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_EmailId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactInfo_WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdentityDetails_PanCardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_TinNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IdentityDetails_CstNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IdentityDetails_AadhaarNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentTerms",
                columns: table => new
                {
                    InstallmentTermId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentTerms", x => x.InstallmentTermId);
                });

            migrationBuilder.CreateTable(
                name: "JobWorks",
                columns: table => new
                {
                    JobWorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobWorkName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorks", x => x.JobWorkId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "OfficeExpenses",
                columns: table => new
                {
                    OfficeExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeExpenses", x => x.OfficeExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SubContractors",
                columns: table => new
                {
                    SubContractorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubContractorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_DoorNoAndStreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_PinCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_CityOrTalukName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_DistrictAndStateName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_EmailId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactInfo_WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BankDetails_AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BankBranch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BankDetails_IfscCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IdentityDetails_PanCardNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IdentityDetails_TinNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_CstNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_AadhaarNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    WorkDetails_WorkName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WorkDetails_Esr = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    WorkDetails_Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractors", x => x.SubContractorId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_DoorNoAndStreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_PinCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address_CityOrTalukName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_DistrictAndStateName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactInfo_EmailId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactInfo_WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BankDetails_AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BankDetails_BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BankBranch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankDetails_BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BankDetails_IfscCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IdentityDetails_PanCardNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IdentityDetails_TinNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_CstNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentityDetails_AadhaarNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Whoms",
                columns: table => new
                {
                    WhomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhomName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whoms", x => x.WhomId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBanks",
                columns: table => new
                {
                    CompanyBankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BankBranch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IfscCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBanks", x => x.CompanyBankId);
                    table.ForeignKey(
                        name: "FK_CompanyBanks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsers",
                columns: table => new
                {
                    CompanyUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsers", x => x.CompanyUserId);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientTransactions",
                columns: table => new
                {
                    ClientTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    ByWhomId = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTransactions", x => x.ClientTransactionId);
                    table.ForeignKey(
                        name: "FK_ClientTransactions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientTransactions_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClientTransactions_Whoms_ByWhomId",
                        column: x => x.ByWhomId,
                        principalTable: "Whoms",
                        principalColumn: "WhomId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompanyExpenseTransactions",
                columns: table => new
                {
                    CompanyExpenseTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    OfficeExpenseId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    MaterialOrJobWorkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    ToWhomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyExpenseTransactions", x => x.CompanyExpenseTransactionId);
                    table.ForeignKey(
                        name: "FK_CompanyExpenseTransactions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompanyExpenseTransactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyExpenseTransactions_OfficeExpenses_OfficeExpenseId",
                        column: x => x.OfficeExpenseId,
                        principalTable: "OfficeExpenses",
                        principalColumn: "OfficeExpenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyExpenseTransactions_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompanyExpenseTransactions_Whoms_ToWhomId",
                        column: x => x.ToWhomId,
                        principalTable: "Whoms",
                        principalColumn: "WhomId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SubContractorTransactions",
                columns: table => new
                {
                    SubContractorTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SubContractorId = table.Column<int>(type: "int", nullable: false),
                    JobWorkId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    ToWhomId = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSubBill = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractorTransactions", x => x.SubContractorTransactionId);
                    table.ForeignKey(
                        name: "FK_SubContractorTransactions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubContractorTransactions_JobWorks_JobWorkId",
                        column: x => x.JobWorkId,
                        principalTable: "JobWorks",
                        principalColumn: "JobWorkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubContractorTransactions_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SubContractorTransactions_SubContractors_SubContractorId",
                        column: x => x.SubContractorId,
                        principalTable: "SubContractors",
                        principalColumn: "SubContractorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubContractorTransactions_Whoms_ToWhomId",
                        column: x => x.ToWhomId,
                        principalTable: "Whoms",
                        principalColumn: "WhomId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SupplierTransactions",
                columns: table => new
                {
                    SupplierTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    ToWhomId = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSubBill = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierTransactions", x => x.SupplierTransactionId);
                    table.ForeignKey(
                        name: "FK_SupplierTransactions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierTransactions_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierTransactions_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SupplierTransactions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierTransactions_Whoms_ToWhomId",
                        column: x => x.ToWhomId,
                        principalTable: "Whoms",
                        principalColumn: "WhomId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_CompanyUsers_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUsers",
                        principalColumn: "CompanyUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTransactions_ByWhomId",
                table: "ClientTransactions",
                column: "ByWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTransactions_ClientId",
                table: "ClientTransactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTransactions_PaymentTypeId",
                table: "ClientTransactions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBanks_CompanyId",
                table: "CompanyBanks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExpenseTransactions_ClientId",
                table: "CompanyExpenseTransactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExpenseTransactions_CompanyId",
                table: "CompanyExpenseTransactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExpenseTransactions_OfficeExpenseId",
                table: "CompanyExpenseTransactions",
                column: "OfficeExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExpenseTransactions_PaymentTypeId",
                table: "CompanyExpenseTransactions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExpenseTransactions_ToWhomId",
                table: "CompanyExpenseTransactions",
                column: "ToWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_CompanyId",
                table: "CompanyUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_UserName",
                table: "CompanyUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobWorks_JobWorkName",
                table: "JobWorks",
                column: "JobWorkName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialName",
                table: "Materials",
                column: "MaterialName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeExpenses_ExpenseName",
                table: "OfficeExpenses",
                column: "ExpenseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_PaymentTypeName",
                table: "PaymentTypes",
                column: "PaymentTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_CompanyUserId",
                table: "RefreshTokens",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubContractorTransactions_ClientId",
                table: "SubContractorTransactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractorTransactions_JobWorkId",
                table: "SubContractorTransactions",
                column: "JobWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractorTransactions_PaymentTypeId",
                table: "SubContractorTransactions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractorTransactions_SubContractorId",
                table: "SubContractorTransactions",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubContractorTransactions_ToWhomId",
                table: "SubContractorTransactions",
                column: "ToWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_ClientId",
                table: "SupplierTransactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_MaterialId",
                table: "SupplierTransactions",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_PaymentTypeId",
                table: "SupplierTransactions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_SupplierId",
                table: "SupplierTransactions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_ToWhomId",
                table: "SupplierTransactions",
                column: "ToWhomId");

            migrationBuilder.CreateIndex(
                name: "IX_Whoms_WhomName",
                table: "Whoms",
                column: "WhomName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ClientTransactions");

            migrationBuilder.DropTable(
                name: "CompanyBanks");

            migrationBuilder.DropTable(
                name: "CompanyExpenseTransactions");

            migrationBuilder.DropTable(
                name: "InstallmentTerms");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SubContractorTransactions");

            migrationBuilder.DropTable(
                name: "SupplierTransactions");

            migrationBuilder.DropTable(
                name: "OfficeExpenses");

            migrationBuilder.DropTable(
                name: "CompanyUsers");

            migrationBuilder.DropTable(
                name: "JobWorks");

            migrationBuilder.DropTable(
                name: "SubContractors");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Whoms");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
