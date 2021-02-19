using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class AddImagePathForHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_Houses_HouseId",
                table: "HouseResidents");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_Houses_HouseId",
                table: "HouseResidents",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_Houses_HouseId",
                table: "HouseResidents");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Houses");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_Houses_HouseId",
                table: "HouseResidents",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
