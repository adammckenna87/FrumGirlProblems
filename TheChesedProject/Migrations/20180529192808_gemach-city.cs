using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheChesedProject.Migrations
{
    public partial class gemachcity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Gemachs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Community",
                table: "Gemachs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Gemachs");

            migrationBuilder.DropColumn(
                name: "Community",
                table: "Gemachs");
        }
    }
}
