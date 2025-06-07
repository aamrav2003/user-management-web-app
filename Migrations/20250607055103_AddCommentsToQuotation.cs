using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAB_251_Assessment2.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToQuotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomerResponseDate",
                table: "Quotations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreparedBy",
                table: "Quotations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "CustomerResponseDate",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PreparedBy",
                table: "Quotations");
        }
    }
}
