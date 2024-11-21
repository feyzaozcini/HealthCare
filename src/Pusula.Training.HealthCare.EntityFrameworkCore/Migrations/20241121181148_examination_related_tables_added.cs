using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.Training.HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class examination_related_tables_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAnamneses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Complaint = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Story = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnamneses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAnamneses_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppDiagnosisGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDiagnosisGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppFallRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HasFallHistory = table.Column<bool>(type: "boolean", nullable: false),
                    UsesMedications = table.Column<bool>(type: "boolean", nullable: false),
                    HasAddiction = table.Column<bool>(type: "boolean", nullable: false),
                    HasBalanceDisorder = table.Column<bool>(type: "boolean", nullable: false),
                    HasVisionImpairment = table.Column<bool>(type: "boolean", nullable: false),
                    MentalState = table.Column<bool>(type: "boolean", nullable: false),
                    GeneralHealthState = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFallRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFallRisks_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPhysicalExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Height = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    BMI = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    VYA = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Temperature = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Pulse = table.Column<int>(type: "integer", nullable: true),
                    SystolicBP = table.Column<int>(type: "integer", nullable: true),
                    DiastolicBP = table.Column<int>(type: "integer", nullable: true),
                    SPO2 = table.Column<int>(type: "integer", nullable: true),
                    Note = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPhysicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPhysicalExaminations_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppPshychologicalStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPshychologicalStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPshychologicalStates_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDiagnoses_AppDiagnosisGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AppDiagnosisGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppExaminationDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosisType = table.Column<int>(type: "integer", nullable: false),
                    InitialDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ProtocolId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExaminationDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppExaminationDiagnoses_AppDiagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "AppDiagnoses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppExaminationDiagnoses_AppProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "AppProtocols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAnamneses_ProtocolId",
                table: "AppAnamneses",
                column: "ProtocolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDiagnoses_GroupId",
                table: "AppDiagnoses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExaminationDiagnoses_DiagnosisId",
                table: "AppExaminationDiagnoses",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExaminationDiagnoses_ProtocolId",
                table: "AppExaminationDiagnoses",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFallRisks_ProtocolId",
                table: "AppFallRisks",
                column: "ProtocolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPhysicalExaminations_ProtocolId",
                table: "AppPhysicalExaminations",
                column: "ProtocolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPshychologicalStates_ProtocolId",
                table: "AppPshychologicalStates",
                column: "ProtocolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAnamneses");

            migrationBuilder.DropTable(
                name: "AppExaminationDiagnoses");

            migrationBuilder.DropTable(
                name: "AppFallRisks");

            migrationBuilder.DropTable(
                name: "AppPhysicalExaminations");

            migrationBuilder.DropTable(
                name: "AppPshychologicalStates");

            migrationBuilder.DropTable(
                name: "AppDiagnoses");

            migrationBuilder.DropTable(
                name: "AppDiagnosisGroups");
        }
    }
}
