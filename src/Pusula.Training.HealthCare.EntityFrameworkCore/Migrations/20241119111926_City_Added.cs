using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class City_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "AppCities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCities",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCities",
                table: "AppCities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCities_CountryId",
                table: "AppCities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCities_AppCountries_CountryId",
                table: "AppCities",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCities_AppCountries_CountryId",
                table: "AppCities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCities",
                table: "AppCities");

            migrationBuilder.DropIndex(
                name: "IX_AppCities_CountryId",
                table: "AppCities");

            migrationBuilder.RenameTable(
                name: "AppCities",
                newName: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");
        }
    }
}
