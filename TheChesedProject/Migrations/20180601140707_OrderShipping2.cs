using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheChesedProject.Migrations
{
    public partial class OrderShipping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddressLine2",
                table: "Orders",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressLine1",
                table: "Orders",
                newName: "AddressLine1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Orders",
                newName: "ShippingAddressLine2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Orders",
                newName: "ShippingAddressLine1");
        }
    }
}
