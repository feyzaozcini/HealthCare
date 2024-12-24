using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class patient_history_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPatientHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Habit = table.Column<string>(type: "text", nullable: true),
                    Disease = table.Column<string>(type: "text", nullable: false),
                    Medicine = table.Column<string>(type: "text", nullable: true),
                    Operation = table.Column<string>(type: "text", nullable: false),
                    Vaccination = table.Column<string>(type: "text", nullable: true),
                    Allergy = table.Column<string>(type: "text", nullable: false),
                    SpecialCondition = table.Column<string>(type: "text", nullable: true),
                    Device = table.Column<string>(type: "text", nullable: true),
                    Therapy = table.Column<string>(type: "text", nullable: true),
                    Job = table.Column<string>(type: "text", nullable: true),
                    EducationLevel = table.Column<int>(type: "integer", nullable: false),
                    MaritalStatus = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPatientHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPatientHistories_AppPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AppPatients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPatientHistories_PatientId",
                table: "AppPatientHistories",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPatientHistories");
        }
    }
}
