using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class SetHousingUserHouseIdAsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments");

            migrationBuilder.AlterColumn<long>(
                name: "HouseId",
                table: "HouseResidents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments",
                column: "UserId",
                principalTable: "HouseResidents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments");

            migrationBuilder.AlterColumn<long>(
                name: "HouseId",
                table: "HouseResidents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAdvertisementComments_HouseResidents_UserId",
                table: "HouseAdvertisementComments",
                column: "UserId",
                principalTable: "HouseResidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
