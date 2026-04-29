using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wrench.auto.repair.estoque.infra.Migrations
{
    /// <inheritdoc />
    public partial class NomePecaUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                schema: "peca",
                table: "Pecas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Pecas_Nome",
                schema: "peca",
                table: "Pecas",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pecas_Nome",
                schema: "peca",
                table: "Pecas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                schema: "peca",
                table: "Pecas");
        }
    }
}
