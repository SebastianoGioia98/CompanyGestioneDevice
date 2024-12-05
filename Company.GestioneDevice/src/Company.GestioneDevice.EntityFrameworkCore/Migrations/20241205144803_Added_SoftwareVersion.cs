using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.GestioneDevice.Migrations
{
    /// <inheritdoc />
    public partial class Added_SoftwareVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftwareVersion",
                table: "AppDevices");

            migrationBuilder.CreateTable(
                name: "AppSoftwareVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSoftwareVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSoftwareVersions_AppDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "AppDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSoftwareVersions_DeviceId",
                table: "AppSoftwareVersions",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSoftwareVersions");

            migrationBuilder.AddColumn<string>(
                name: "SoftwareVersion",
                table: "AppDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
