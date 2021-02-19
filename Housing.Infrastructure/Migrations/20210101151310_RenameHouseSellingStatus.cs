using Microsoft.EntityFrameworkCore.Migrations;

namespace Housing.Infrastructure.Migrations
{
    public partial class RenameHouseSellingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBought",
                table: "Houses",
                newName: "IsSelling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelling",
                table: "Houses",
                newName: "IsBought");
        }
    }
}
