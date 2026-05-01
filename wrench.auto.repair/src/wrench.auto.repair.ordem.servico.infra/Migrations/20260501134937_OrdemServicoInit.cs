using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wrench.auto.repair.ordem.servico.infra.Migrations
{
    /// <inheritdoc />
    public partial class OrdemServicoInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdemServicoDiagnostico",
                columns: table => new
                {
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SolucaoProposta = table.Column<string>(type: "text", nullable: false),
                    DataDiagnostico = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoDiagnostico", x => x.OrdemServicoId);
                    table.ForeignKey(
                        name: "FK_OrdemServicoDiagnostico_OrdemServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalTable: "OrdemServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdemServicoOrcamento",
                columns: table => new
                {
                    OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoOrcamento", x => x.OrdemServicoId);
                    table.ForeignKey(
                        name: "FK_OrdemServicoOrcamento_OrdemServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalTable: "OrdemServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemServicoDiagnostico");

            migrationBuilder.DropTable(
                name: "OrdemServicoOrcamento");

            migrationBuilder.DropTable(
                name: "OrdemServico");
        }
    }
}
