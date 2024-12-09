using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.GestioneDevice.Migrations
{
    /// <inheritdoc />
    public partial class Added_DeviceUserConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppDevices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppDevices_UserId",
                table: "AppDevices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDevices_AppUsers_UserId",
                table: "AppDevices",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDevices_AppUsers_UserId",
                table: "AppDevices");

            migrationBuilder.DropIndex(
                name: "IX_AppDevices_UserId",
                table: "AppDevices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppDevices");
        }
    }
}
