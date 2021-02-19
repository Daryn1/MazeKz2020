using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class MergeHousingOwnerWithHousingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_Users_UserId",
                table: "HouseResidents");

            migrationBuilder.DropIndex(
                name: "IX_HouseResidents_UserId",
                table: "HouseResidents");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "HouseResidents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HouseResidents",
                newName: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseResidents_OwnerId",
                table: "HouseResidents",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_HouseOwners_OwnerId",
                table: "HouseResidents",
                column: "OwnerId",
                principalTable: "HouseOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_HouseOwners_OwnerId",
                table: "HouseResidents");

            migrationBuilder.DropIndex(
                name: "IX_HouseResidents_OwnerId",
                table: "HouseResidents");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "HouseResidents",
                newName: "UserId");

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "HouseResidents",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_HouseResidents_UserId",
                table: "HouseResidents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_Users_UserId",
                table: "HouseResidents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
