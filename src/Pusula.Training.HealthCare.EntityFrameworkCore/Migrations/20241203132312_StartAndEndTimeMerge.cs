using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class StartAndEndTimeMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
            name: "EndTime",
            table: "AppProtocols",
            type: "timestamp without time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "EndTime",
            table: "AppProtocols");
        }
    }
}
