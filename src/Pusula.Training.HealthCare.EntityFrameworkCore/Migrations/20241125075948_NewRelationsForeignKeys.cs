using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationsForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "AppPatients",
                newName: "SecondaryVillageId");

            migrationBuilder.RenameIndex(
                name: "IX_AppPatients_CountryId",
                table: "AppPatients",
                newName: "IX_AppPatients_SecondaryVillageId");

            

            migrationBuilder.AlterColumn<string>(
                name: "MobilePhoneNumber",
                table: "AppPatients",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "EmergencyPhoneNumber",
                table: "AppPatients",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryAddressDescription",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCityId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryDistrictId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryVillageId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAddressDescription",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryCityId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryCountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryDistrictId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "SecondaryVillageId",
                table: "AppPatients",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_AppPatients_SecondaryVillageId",
                table: "AppPatients",
                newName: "IX_AppPatients_CountryId");

            

            migrationBuilder.AlterColumn<string>(
                name: "MobilePhoneNumber",
                table: "AppPatients",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "EmergencyPhoneNumber",
                table: "AppPatients",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");
        }
    }
}
