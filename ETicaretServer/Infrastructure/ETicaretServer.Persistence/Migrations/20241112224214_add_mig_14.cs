﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretServer.Persistence.Migrations
{
    public partial class add_mig_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Baskets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OrderId",
                table: "Baskets",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_OrderId",
                table: "Baskets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_OrderId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OrderId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Baskets");
        }
    }
}
