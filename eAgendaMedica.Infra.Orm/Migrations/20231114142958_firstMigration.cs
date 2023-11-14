using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCirurgia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraTermino = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCirurgia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Crm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponibilidade = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBConsulta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraTermino = table.Column<TimeSpan>(type: "time", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBConsulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBMedico_TBConsulta",
                        column: x => x.MedicoId,
                        principalTable: "TBMedico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBMedico_TBCirurgia",
                columns: table => new
                {
                    CirurgiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico_TBCirurgia", x => new { x.CirurgiasId, x.MedicosId });
                    table.ForeignKey(
                        name: "FK_TBMedico_TBCirurgia_TBCirurgia_CirurgiasId",
                        column: x => x.CirurgiasId,
                        principalTable: "TBCirurgia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBMedico_TBCirurgia_TBMedico_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBConsulta_MedicoId",
                table: "TBConsulta",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBMedico_TBCirurgia_MedicosId",
                table: "TBMedico_TBCirurgia",
                column: "MedicosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBConsulta");

            migrationBuilder.DropTable(
                name: "TBMedico_TBCirurgia");

            migrationBuilder.DropTable(
                name: "TBCirurgia");

            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
