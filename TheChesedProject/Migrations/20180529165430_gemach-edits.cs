using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheChesedProject.Migrations
{
    public partial class gemachedits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeClose",
                table: "Gemachs");

            migrationBuilder.DropColumn(
                name: "TimeOpen",
                table: "Gemachs");

            migrationBuilder.AddColumn<string>(
                name: "OwnerFirstName",
                table: "Gemachs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerLastName",
                table: "Gemachs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerFirstName",
                table: "Gemachs");

            migrationBuilder.DropColumn(
                name: "OwnerLastName",
                table: "Gemachs");

            migrationBuilder.AddColumn<int>(
                name: "TimeClose",
                table: "Gemachs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeOpen",
                table: "Gemachs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
