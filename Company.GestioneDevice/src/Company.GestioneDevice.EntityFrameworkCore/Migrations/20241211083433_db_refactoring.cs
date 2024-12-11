using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.GestioneDevice.Migrations
{
    /// <inheritdoc />
    public partial class db_refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPolicies_AppPolicies_PolicieId",
                table: "AppUserPolicies");

            migrationBuilder.RenameColumn(
                name: "PolicieId",
                table: "AppUserPolicies",
                newName: "PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserPolicies_PolicieId",
                table: "AppUserPolicies",
                newName: "IX_AppUserPolicies_PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPolicies_AppPolicies_PolicyId",
                table: "AppUserPolicies",
                column: "PolicyId",
                principalTable: "AppPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPolicies_AppPolicies_PolicyId",
                table: "AppUserPolicies");

            migrationBuilder.RenameColumn(
                name: "PolicyId",
                table: "AppUserPolicies",
                newName: "PolicieId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserPolicies_PolicyId",
                table: "AppUserPolicies",
                newName: "IX_AppUserPolicies_PolicieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPolicies_AppPolicies_PolicieId",
                table: "AppUserPolicies",
                column: "PolicieId",
                principalTable: "AppPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
