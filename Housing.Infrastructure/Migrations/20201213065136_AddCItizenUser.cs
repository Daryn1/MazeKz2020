using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class AddCItizenUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseOwners_CitizenUser_UserId",
                table: "HouseOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_CitizenUser_UserId",
                table: "HouseResidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenUser",
                table: "CitizenUser");

            migrationBuilder.RenameTable(
                name: "CitizenUser",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseOwners_Users_UserId",
                table: "HouseOwners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_Users_UserId",
                table: "HouseResidents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseOwners_Users_UserId",
                table: "HouseOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseResidents_Users_UserId",
                table: "HouseResidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "CitizenUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenUser",
                table: "CitizenUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseOwners_CitizenUser_UserId",
                table: "HouseOwners",
                column: "UserId",
                principalTable: "CitizenUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseResidents_CitizenUser_UserId",
                table: "HouseResidents",
                column: "UserId",
                principalTable: "CitizenUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
