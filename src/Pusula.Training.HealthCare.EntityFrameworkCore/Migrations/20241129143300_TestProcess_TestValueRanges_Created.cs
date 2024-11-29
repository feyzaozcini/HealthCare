using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class TestProcess_TestValueRanges_Created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLabRequests_AppTestGroupItems_TestGroupItemId",
                table: "AppLabRequests");

            migrationBuilder.DropIndex(
                name: "IX_AppLabRequests_TestGroupItemId",
                table: "AppLabRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppLabRequests");

            migrationBuilder.DropColumn(
                name: "TestGroupItemId",
                table: "AppLabRequests");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppLabRequests",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppTestProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LabRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestGroupItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<decimal>(type: "numeric", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestProcesses_AppLabRequests_LabRequestId",
                        column: x => x.LabRequestId,
                        principalTable: "AppLabRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTestProcesses_AppTestGroupItems_TestGroupItemId",
                        column: x => x.TestGroupItemId,
                        principalTable: "AppTestGroupItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppTestValueRanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestGroupItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    MinValue = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxValue = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestValueRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestValueRanges_AppTestGroupItems_TestGroupItemId",
                        column: x => x.TestGroupItemId,
                        principalTable: "AppTestGroupItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_AppLabRequests_DoctorId",
                table: "AppLabRequests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLabRequests_ProtocolId",
                table: "AppLabRequests",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestProcesses_LabRequestId",
                table: "AppTestProcesses",
                column: "LabRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestProcesses_TestGroupItemId",
                table: "AppTestProcesses",
                column: "TestGroupItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestValueRanges_TestGroupItemId",
                table: "AppTestValueRanges",
                column: "TestGroupItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLabRequests_AppDoctors_DoctorId",
                table: "AppLabRequests",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppLabRequests_AppProtocols_ProtocolId",
                table: "AppLabRequests",
                column: "ProtocolId",
                principalTable: "AppProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLabRequests_AppDoctors_DoctorId",
                table: "AppLabRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AppLabRequests_AppProtocols_ProtocolId",
                table: "AppLabRequests");

            migrationBuilder.DropTable(
                name: "AppTestProcesses");

            migrationBuilder.DropTable(
                name: "AppTestValueRanges");

            migrationBuilder.DropIndex(
                name: "IX_AppLabRequests_DoctorId",
                table: "AppLabRequests");

            migrationBuilder.DropIndex(
                name: "IX_AppLabRequests_ProtocolId",
                table: "AppLabRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppLabRequests");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppLabRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TestGroupItemId",
                table: "AppLabRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppLabRequests_TestGroupItemId",
                table: "AppLabRequests",
                column: "TestGroupItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLabRequests_AppTestGroupItems_TestGroupItemId",
                table: "AppLabRequests",
                column: "TestGroupItemId",
                principalTable: "AppTestGroupItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
