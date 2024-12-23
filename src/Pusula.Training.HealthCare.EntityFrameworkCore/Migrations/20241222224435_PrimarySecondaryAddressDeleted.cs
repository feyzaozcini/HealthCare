using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class PrimarySecondaryAddressDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCities_PrimaryCityId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCities_SecondaryCityId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCountries_PrimaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCountries_SecondaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppDistricts_PrimaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppDistricts_SecondaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppVillages_PrimaryVillageId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppVillages_SecondaryVillageId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_PrimaryCityId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_PrimaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_PrimaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_PrimaryVillageId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_SecondaryCityId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_SecondaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_SecondaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_SecondaryVillageId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PrimaryAddressDescription",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PrimaryCityId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PrimaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PrimaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PrimaryVillageId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "SecondaryAddressDescription",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "SecondaryCityId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "SecondaryCountryId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "SecondaryDistrictId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "SecondaryVillageId",
                table: "AppPatients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryAddressDescription",
                table: "AppPatients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCityId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryDistrictId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryVillageId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAddressDescription",
                table: "AppPatients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryCityId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryCountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryDistrictId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryVillageId",
                table: "AppPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_PrimaryCityId",
                table: "AppPatients",
                column: "PrimaryCityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_PrimaryCountryId",
                table: "AppPatients",
                column: "PrimaryCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_PrimaryDistrictId",
                table: "AppPatients",
                column: "PrimaryDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_PrimaryVillageId",
                table: "AppPatients",
                column: "PrimaryVillageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_SecondaryCityId",
                table: "AppPatients",
                column: "SecondaryCityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_SecondaryCountryId",
                table: "AppPatients",
                column: "SecondaryCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_SecondaryDistrictId",
                table: "AppPatients",
                column: "SecondaryDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_SecondaryVillageId",
                table: "AppPatients",
                column: "SecondaryVillageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCities_PrimaryCityId",
                table: "AppPatients",
                column: "PrimaryCityId",
                principalTable: "AppCities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCities_SecondaryCityId",
                table: "AppPatients",
                column: "SecondaryCityId",
                principalTable: "AppCities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCountries_PrimaryCountryId",
                table: "AppPatients",
                column: "PrimaryCountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCountries_SecondaryCountryId",
                table: "AppPatients",
                column: "SecondaryCountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppDistricts_PrimaryDistrictId",
                table: "AppPatients",
                column: "PrimaryDistrictId",
                principalTable: "AppDistricts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppDistricts_SecondaryDistrictId",
                table: "AppPatients",
                column: "SecondaryDistrictId",
                principalTable: "AppDistricts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppVillages_PrimaryVillageId",
                table: "AppPatients",
                column: "PrimaryVillageId",
                principalTable: "AppVillages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppVillages_SecondaryVillageId",
                table: "AppPatients",
                column: "SecondaryVillageId",
                principalTable: "AppVillages",
                principalColumn: "Id");
        }
    }
}
