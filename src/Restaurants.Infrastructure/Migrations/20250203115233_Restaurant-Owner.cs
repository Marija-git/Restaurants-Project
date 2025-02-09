using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // foreign key constraint ne sme biti prekrsen
            //osiguravamo da svi postojeci restorani iz tabele Restaurants imaju vlasnika
            // u ovom slucaju uzimamo prvi id iz tabele aspnetusers i postavljamo tog user-a za vlasnike restorana koji postoje u bazi
            //ovo radimo da restorani ne bi imali null vrednost ili nevalidan id za kolonu OwnerId
            migrationBuilder.Sql("UPDATE Restaurants " +
              "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)"); //039cff19-267c-434f-a0a3-bbdddef8ccd5 -> owner id

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Restaurants");
        }
    }
}
