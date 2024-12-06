using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestGroupItemTestValueRangeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppTestValueRanges_TestGroupItemId",
                table: "AppTestValueRanges");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestValueRanges_TestGroupItemId",
                table: "AppTestValueRanges",
                column: "TestGroupItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppTestValueRanges_TestGroupItemId",
                table: "AppTestValueRanges");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestValueRanges_TestGroupItemId",
                table: "AppTestValueRanges",
                column: "TestGroupItemId");
        }
    }
}
