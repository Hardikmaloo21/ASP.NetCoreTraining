using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HackerBank.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Balance", "Date", "Description", "Type" },
                values: new object[,]
                {
                    { 1, 1985.4m, "$12,234.45", "2026-03-28", "HACKERBANK INC. DES:CCD+ ID: 33375894749", 0 },
                    { 2, 3200m, "$10,249.05", "2026-03-27", "ACH TRANSFER - PAYROLL DEPOSIT", 0 },
                    { 3, 59.99m, "$7,049.05", "2026-03-27", "AMAZON.COM PURCHASE", 1 },
                    { 4, 6.75m, "$7,109.04", "2026-03-26", "STARBUCKS #4823 COFFEE", 1 },
                    { 5, 500m, "$7,115.79", "2026-03-26", "ONLINE TRANSFER FROM SAVINGS", 0 },
                    { 6, 13.99m, "$6,615.79", "2026-03-25", "NETFLIX SUBSCRIPTION", 1 },
                    { 7, 87.34m, "$6,629.78", "2026-03-25", "WHOLE FOODS MARKET", 1 },
                    { 8, 22.5m, "$6,717.12", "2026-03-24", "UBER TECHNOLOGIES", 1 },
                    { 9, 12.48m, "$6,739.62", "2026-03-24", "INTEREST PAYMENT", 0 },
                    { 10, 200m, "$6,727.14", "2026-03-23", "ATM WITHDRAWAL - 5TH AVE", 1 },
                    { 11, 9.99m, "$6,927.14", "2026-03-22", "SPOTIFY PREMIUM", 1 },
                    { 12, 750m, "$6,937.13", "2026-03-22", "DIRECT DEPOSIT - FREELANCE", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
