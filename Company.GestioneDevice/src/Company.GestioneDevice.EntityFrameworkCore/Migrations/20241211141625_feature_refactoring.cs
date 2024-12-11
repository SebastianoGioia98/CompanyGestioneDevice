using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.GestioneDevice.Migrations
{
    /// <inheritdoc />
    public partial class feature_refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppFeatures",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppFeatures");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppFeatures");
        }
    }
}
