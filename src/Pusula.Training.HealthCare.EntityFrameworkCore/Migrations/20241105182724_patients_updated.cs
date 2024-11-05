using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class patients_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePhoneNumber",
                table: "AppPatients");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "AppPatients",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "AppPatients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPhoneNumber",
                table: "AppPatients",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "AppPatients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_CompanyId",
                table: "AppPatients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPatients_CountryId",
                table: "AppPatients",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPatients_AppPatientCompanies_CompanyId",
                table: "AppPatients",
                column: "CompanyId",
                principalTable: "AppPatientCompanies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppCountries_CountryId",
                table: "AppPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPatients_AppPatientCompanies_CompanyId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_CompanyId",
                table: "AppPatients");

            migrationBuilder.DropIndex(
                name: "IX_AppPatients_CountryId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "EmergencyPhoneNumber",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "No",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "AppPatients");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppPatients");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AppPatients",
                newName: "EmailAddress");

            migrationBuilder.AddColumn<string>(
                name: "HomePhoneNumber",
                table: "AppPatients",
                type: "text",
                nullable: true);
        }
    }
}
