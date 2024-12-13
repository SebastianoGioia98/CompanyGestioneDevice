using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.GestioneDevice.Migrations
{
    /// <inheritdoc />
    public partial class feature_policy_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PolicyId",
                table: "AppFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatures_PolicyId",
                table: "AppFeatures",
                column: "PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatures_AppPolicies_PolicyId",
                table: "AppFeatures",
                column: "PolicyId",
                principalTable: "AppPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatures_AppPolicies_PolicyId",
                table: "AppFeatures");

            migrationBuilder.DropIndex(
                name: "IX_AppFeatures_PolicyId",
                table: "AppFeatures");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "AppFeatures");
        }
    }
}
