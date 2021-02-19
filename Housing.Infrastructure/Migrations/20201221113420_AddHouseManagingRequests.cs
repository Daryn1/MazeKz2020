using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class AddHouseManagingRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxResidentsCount",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HousingOwnerRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingOwnerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HousingOwnerRequests_HouseOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "HouseOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HousingOwnerRequests_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HousingResidentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseId = table.Column<long>(type: "bigint", nullable: false),
                    ResidentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousingResidentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HousingResidentRequests_HouseResidents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "HouseResidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HousingResidentRequests_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HousingOwnerRequests_HouseId",
                table: "HousingOwnerRequests",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingOwnerRequests_OwnerId",
                table: "HousingOwnerRequests",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingResidentRequests_HouseId",
                table: "HousingResidentRequests",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HousingResidentRequests_ResidentId",
                table: "HousingResidentRequests",
                column: "ResidentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HousingOwnerRequests");

            migrationBuilder.DropTable(
                name: "HousingResidentRequests");

            migrationBuilder.DropColumn(
                name: "MaxResidentsCount",
                table: "Houses");
        }
    }
}
