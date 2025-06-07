using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAB_251_Assessment2.Migrations
{
    /// <inheritdoc />
    public partial class AddI4I6Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerDecision",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerDecision",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Quotations");
        }
    }
}
