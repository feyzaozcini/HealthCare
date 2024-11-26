using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class protocol_table_revision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "AppProtocols",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProtocols_DoctorId",
                table: "AppProtocols",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProtocols_AppDoctors_DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropIndex(
                name: "IX_AppProtocols_DoctorId",
                table: "AppProtocols");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppProtocols");
        }
    }
}
