﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanSachModel.Migrations
{
    /// <inheritdoc />
    public partial class ssss2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypePay",
                table: "tbl_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePay",
                table: "tbl_Order");
        }
    }
}
