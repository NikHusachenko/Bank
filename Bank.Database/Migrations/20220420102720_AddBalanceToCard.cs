﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Database.Migrations
{
    public partial class AddBalanceToCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Cards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Cards");
        }
    }
}
