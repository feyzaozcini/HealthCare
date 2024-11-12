using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientNoSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PatientNoSequence",
                startValue: 10000L);

            migrationBuilder.AlterColumn<int>(
                name: "No",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"PatientNoSequence\"')",
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "PatientNoSequence");

            migrationBuilder.AlterColumn<int>(
                name: "No",
                table: "AppPatients",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('\"PatientNoSequence\"')");
        }
    }
}
