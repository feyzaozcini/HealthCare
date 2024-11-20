using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class District_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDistricts_AppCities_CityId",
                        column: x => x.CityId,
                        principalTable: "AppCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDistricts_CityId",
                table: "AppDistricts",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDistricts");
        }
    }
}
