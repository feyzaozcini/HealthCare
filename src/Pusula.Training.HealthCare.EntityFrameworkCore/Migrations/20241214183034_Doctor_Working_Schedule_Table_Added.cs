using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class Doctor_Working_Schedule_Table_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDoctorWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingDays = table.Column<int[]>(type: "integer[]", nullable: false),
                    StartHour = table.Column<string>(type: "text", nullable: false),
                    EndHour = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDoctorWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDoctorWorkSchedules_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctorWorkSchedules_DoctorId",
                table: "AppDoctorWorkSchedules",
                column: "DoctorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDoctorWorkSchedules");
        }
    }
}
