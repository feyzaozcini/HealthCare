using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class system_check_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "AppSystemChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneralSystemCheck = table.Column<bool>(type: "boolean", nullable: false),
                    GenitoUrinary = table.Column<bool>(type: "boolean", nullable: true),
                    Skin = table.Column<bool>(type: "boolean", nullable: true),
                    Respiratory = table.Column<bool>(type: "boolean", nullable: true),
                    Nervous = table.Column<bool>(type: "boolean", nullable: true),
                    MusculoSkeletal = table.Column<bool>(type: "boolean", nullable: true),
                    Circulatory = table.Column<bool>(type: "boolean", nullable: true),
                    GastroIntestinal = table.Column<bool>(type: "boolean", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSystemChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSystemChecks_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSystemChecks_ProtocolId",
                table: "AppSystemChecks",
                column: "ProtocolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSystemChecks");
        }
    }
}
