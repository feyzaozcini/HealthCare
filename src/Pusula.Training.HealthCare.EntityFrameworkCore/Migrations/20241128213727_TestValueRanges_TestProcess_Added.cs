using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class TestValueRanges_TestProcess_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTestProcesses");

            migrationBuilder.DropTable(
                name: "AppTestValueRanges");
        }
    }
}
