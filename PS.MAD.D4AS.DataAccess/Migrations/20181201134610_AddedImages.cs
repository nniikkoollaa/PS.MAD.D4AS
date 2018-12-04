using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.MAD.D4AS.DataAccess.Migrations
{
    public partial class AddedImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Image_Tickets_TicketId",
            //    table: "Image");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Image",
            //    table: "Image");

            //migrationBuilder.RenameTable(
            //    name: "Image",
            //    newName: "Images");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Image_TicketId",
            //    table: "Images",
            //    newName: "IX_Images_TicketId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Images",
            //    table: "Images",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Images_Tickets_TicketId",
            //    table: "Images",
            //    column: "TicketId",
            //    principalTable: "Tickets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Images_Tickets_TicketId",
            //    table: "Images");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Images",
            //    table: "Images");

            //migrationBuilder.RenameTable(
            //    name: "Images",
            //    newName: "Image");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Images_TicketId",
            //    table: "Image",
            //    newName: "IX_Image_TicketId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Image",
            //    table: "Image",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Image_Tickets_TicketId",
            //    table: "Image",
            //    column: "TicketId",
            //    principalTable: "Tickets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
