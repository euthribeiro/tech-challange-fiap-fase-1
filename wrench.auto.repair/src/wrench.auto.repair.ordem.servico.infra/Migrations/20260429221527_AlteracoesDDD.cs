using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wrench.auto.repair.ordem.servico.infra.Migrations
{
    /// <inheritdoc />
    public partial class AlteracoesDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnostico",
                schema: "ordem_servico");

            migrationBuilder.CreateTable(
                name: "Orcamento",
                schema: "ordem_servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdemServicoDiagnostico",
                schema: "ordem_servico",
                columns: table => new
                {
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    MecanicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SolucaoProposta = table.Column<string>(type: "text", nullable: false),
                    DataDiagnostico = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoDiagnostico", x => x.OrdemServicoId);
                    table.ForeignKey(
                        name: "FK_OrdemServicoDiagnostico_OrdemServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalSchema: "ordem_servico",
                        principalTable: "OrdemServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orcamento",
                schema: "ordem_servico");

            migrationBuilder.DropTable(
                name: "OrdemServicoDiagnostico",
                schema: "ordem_servico");

            migrationBuilder.CreateTable(
                name: "Diagnostico",
                schema: "ordem_servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDiagnostico = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MecanicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SolucaoProposta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnostico_OrdemServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalSchema: "ordem_servico",
                        principalTable: "OrdemServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostico_OrdemServicoId",
                schema: "ordem_servico",
                table: "Diagnostico",
                column: "OrdemServicoId",
                unique: true);
        }
    }
}
