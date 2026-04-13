using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeuPlantao.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantoes_Profissionais_SolicitanteId",
                table: "Plantoes");

            migrationBuilder.DropIndex(
                name: "IX_Plantoes_SolicitanteId",
                table: "Plantoes");

            migrationBuilder.DropColumn(
                name: "SolicitanteId",
                table: "Plantoes");

            migrationBuilder.AddColumn<float>(
                name: "Valor",
                table: "Plantoes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "SolicitacaoModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantaoId = table.Column<long>(type: "bigint", nullable: false),
                    ProfissionalId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoModel_Plantoes_PlantaoId",
                        column: x => x.PlantaoId,
                        principalTable: "Plantoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoModel_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoModel_PlantaoId_ProfissionalId",
                table: "SolicitacaoModel",
                columns: new[] { "PlantaoId", "ProfissionalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoModel_ProfissionalId",
                table: "SolicitacaoModel",
                column: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoModel");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Plantoes");

            migrationBuilder.AddColumn<long>(
                name: "SolicitanteId",
                table: "Plantoes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plantoes_SolicitanteId",
                table: "Plantoes",
                column: "SolicitanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantoes_Profissionais_SolicitanteId",
                table: "Plantoes",
                column: "SolicitanteId",
                principalTable: "Profissionais",
                principalColumn: "Id");
        }
    }
}
