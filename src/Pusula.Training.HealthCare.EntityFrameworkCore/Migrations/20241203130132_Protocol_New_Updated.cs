using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class Protocol_New_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDepartments_DepartmentId",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppPatients_PatientId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppProtocols");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "AppProtocols",
                newName: "Patient");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "AppProtocols",
                newName: "Doctor");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AppProtocols",
                newName: "Department");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_PatientId",
                table: "AppProtocols",
                newName: "IX_AppProtocols_Patient");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_DoctorId",
                table: "AppProtocols",
                newName: "IX_AppProtocols_Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_DepartmentId",
                table: "AppProtocols",
                newName: "IX_AppProtocols_Department");

            migrationBuilder.CreateSequence<int>(
                name: "ProtocolNoSequence",
                startValue: 20000L);

            migrationBuilder.AddColumn<Guid>(
                name: "Insurance",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "AppProtocols",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"ProtocolNoSequence\"')");

            migrationBuilder.AddColumn<Guid>(
                name: "Note",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ProtocolStatus",
                table: "AppProtocols",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolType",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_Insurance",
                table: "AppProtocols",
                column: "Insurance");

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_Note",
                table: "AppProtocols",
                column: "Note",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_ProtocolType",
                table: "AppProtocols",
                column: "ProtocolType");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDepartments_Department",
                table: "AppProtocols",
                column: "Department",
                principalTable: "AppDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDoctors_Doctor",
                table: "AppProtocols",
                column: "Doctor",
                principalTable: "AppDoctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppInsurances_Insurance",
                table: "AppProtocols",
                column: "Insurance",
                principalTable: "AppInsurances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppNotes_Note",
                table: "AppProtocols",
                column: "Note",
                principalTable: "AppNotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppPatients_Patient",
                table: "AppProtocols",
                column: "Patient",
                principalTable: "AppPatients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppProtocolTypes_ProtocolType",
                table: "AppProtocols",
                column: "ProtocolType",
                principalTable: "AppProtocolTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDepartments_Department",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDoctors_Doctor",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppInsurances_Insurance",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppNotes_Note",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppPatients_Patient",
                table: "AppProtocols");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppProtocolTypes_ProtocolType",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_Insurance",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_Note",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_ProtocolType",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "No",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "ProtocolStatus",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "ProtocolType",
                table: "AppProtocols");

            migrationBuilder.DropSequence(
                name: "ProtocolNoSequence");

            migrationBuilder.RenameColumn(
                name: "Patient",
                table: "AppProtocols",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "Doctor",
                table: "AppProtocols",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "AppProtocols",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_Patient",
                table: "AppProtocols",
                newName: "IX_AppProtocols_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_Doctor",
                table: "AppProtocols",
                newName: "IX_AppProtocols_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProtocols_Department",
                table: "AppProtocols",
                newName: "IX_AppProtocols_DepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AppProtocols",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDepartments_DepartmentId",
                table: "AppProtocols",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppPatients_PatientId",
                table: "AppProtocols",
                column: "PatientId",
                principalTable: "AppPatients",
                principalColumn: "Id");
        }
    }
}
