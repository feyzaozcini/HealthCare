using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class pain_details_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPainDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    PainTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PainRhythm = table.Column<int>(type: "integer", nullable: false),
                    OtherPain = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPainDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPainDetails_AppPainTypes_PainTypeId",
                        column: x => x.PainTypeId,
                        principalTable: "AppPainTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppPainDetails_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPainDetails_PainTypeId",
                table: "AppPainDetails",
                column: "PainTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPainDetails_ProtocolId",
                table: "AppPainDetails",
                column: "ProtocolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPainDetails");
        }
    }
}
