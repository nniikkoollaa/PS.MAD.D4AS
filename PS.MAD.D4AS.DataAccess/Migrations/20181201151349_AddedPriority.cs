using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.MAD.D4AS.DataAccess.Migrations
{
    public partial class AddedPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Priority",
                table: "Tickets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Priority",
                table: "Images",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Images");
        }
    }
}
