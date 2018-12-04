using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.MAD.D4AS.DataAccess.Migrations
{
    public partial class UpdatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalyserDescription",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDescription",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalyserDescription",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserDescription",
                table: "Images");
        }
    }
}
