using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class AddCommentsForHouseAndHousingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_HousingUserId",
                table: "HouseAdvertisementComments");

            migrationBuilder.RenameColumn(
                name: "HousingUserId",
                table: "HouseAdvertisementComments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HouseAdvertisementComments_HousingUserId",
                table: "HouseAdvertisementComments",
                newName: "IX_HouseAdvertisementComments_UserId");

            migrationBuilder.AddColumn<long>(
                name: "HouseId",
                table: "HouseAdvertisementComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HouseAdvertisementComments_HouseId",
                table: "HouseAdvertisementComments",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments",
                column: "UserId",
                principalTable: "HouseResidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAdvertisementComments_Houses_HouseId",
                table: "HouseAdvertisementComments",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseAdvertisementComments_Houses_HouseId",
                table: "HouseAdvertisementComments");

            migrationBuilder.DropIndex(
                name: "IX_HouseAdvertisementComments_HouseId",
                table: "HouseAdvertisementComments");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "HouseAdvertisementComments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HouseAdvertisementComments",
                newName: "HousingUserId");

            migrationBuilder.RenameIndex(
                name: "IX_HouseAdvertisementComments_UserId",
                table: "HouseAdvertisementComments",
                newName: "IX_HouseAdvertisementComments_HousingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_HousingUserId",
                table: "HouseAdvertisementComments",
                column: "HousingUserId",
                principalTable: "HouseResidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
