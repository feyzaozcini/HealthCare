using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class Appointment_Type_Duration_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppTitles_TitleId",
                table: "AppDoctors");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "AppAppointmentTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppDoctorAppoinmentTypes",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppoinmentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDoctorAppoinmentTypes", x => new { x.DoctorId, x.AppoinmentTypeId });
                    table.ForeignKey(
                        name: "FK_AppDoctorAppoinmentTypes_AppAppointmentTypes_AppoinmentType~",
                        column: x => x.AppoinmentTypeId,
                        principalTable: "AppAppointmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDoctorAppoinmentTypes_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctorAppoinmentTypes_AppoinmentTypeId",
                table: "AppDoctorAppoinmentTypes",
                column: "AppoinmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppTitles_TitleId",
                table: "AppDoctors",
                column: "TitleId",
                principalTable: "AppTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppTitles_TitleId",
                table: "AppDoctors");

            migrationBuilder.DropTable(
                name: "AppDoctorAppoinmentTypes");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "AppAppointmentTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppTitles_TitleId",
                table: "AppDoctors",
                column: "TitleId",
                principalTable: "AppTitles",
                principalColumn: "Id");
        }
    }
}
