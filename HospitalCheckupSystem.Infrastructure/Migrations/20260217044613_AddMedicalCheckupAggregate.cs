using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalCheckupSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalCheckupAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalCheckups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    McuNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CheckupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCheckups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckupAnamneses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Complaints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastIllness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    Alcohol = table.Column<bool>(type: "bit", nullable: false),
                    WorkHazards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCheckupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupAnamneses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupAnamneses_MedicalCheckups_MedicalCheckupId",
                        column: x => x.MedicalCheckupId,
                        principalTable: "MedicalCheckups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckupConclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fit = table.Column<bool>(type: "bit", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalCheckupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupConclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupConclusions_MedicalCheckups_MedicalCheckupId",
                        column: x => x.MedicalCheckupId,
                        principalTable: "MedicalCheckups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckupLabResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NormalMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsNormal = table.Column<bool>(type: "bit", nullable: false),
                    MedicalCheckupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupLabResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupLabResults_MedicalCheckups_MedicalCheckupId",
                        column: x => x.MedicalCheckupId,
                        principalTable: "MedicalCheckups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckupPhysicalExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralAppearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lungs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neurology = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCheckupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupPhysicalExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupPhysicalExams_MedicalCheckups_MedicalCheckupId",
                        column: x => x.MedicalCheckupId,
                        principalTable: "MedicalCheckups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckupVitals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicalCheckupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupVitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupVitals_MedicalCheckups_MedicalCheckupId",
                        column: x => x.MedicalCheckupId,
                        principalTable: "MedicalCheckups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckupAnamneses_MedicalCheckupId",
                table: "CheckupAnamneses",
                column: "MedicalCheckupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckupConclusions_MedicalCheckupId",
                table: "CheckupConclusions",
                column: "MedicalCheckupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckupLabResults_MedicalCheckupId",
                table: "CheckupLabResults",
                column: "MedicalCheckupId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckupPhysicalExams_MedicalCheckupId",
                table: "CheckupPhysicalExams",
                column: "MedicalCheckupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckupVitals_MedicalCheckupId",
                table: "CheckupVitals",
                column: "MedicalCheckupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckupAnamneses");

            migrationBuilder.DropTable(
                name: "CheckupConclusions");

            migrationBuilder.DropTable(
                name: "CheckupLabResults");

            migrationBuilder.DropTable(
                name: "CheckupPhysicalExams");

            migrationBuilder.DropTable(
                name: "CheckupVitals");

            migrationBuilder.DropTable(
                name: "MedicalCheckups");
        }
    }
}
