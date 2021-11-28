﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservices.StockTrading.Migrations
{
    public partial class pghg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UserStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserStocks");
        }
    }
}
