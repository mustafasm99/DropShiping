using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class stores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreOwner",
                table: "Stores",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "Stores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "Stores",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Stores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreOwner",
                table: "Stores",
                column: "StoreOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_UsersAuths_StoreOwner",
                table: "Stores",
                column: "StoreOwner",
                principalTable: "UsersAuths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_UsersAuths_StoreOwner",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_StoreOwner",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreOwner",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "tags",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Stores");
        }
    }
}
