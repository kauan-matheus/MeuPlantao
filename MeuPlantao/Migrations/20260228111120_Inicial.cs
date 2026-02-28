using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeuPlantao.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Crm = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissionais_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plantoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SetorId = table.Column<int>(type: "integer", nullable: false),
                    ProfissionalResponsavelId = table.Column<int>(type: "integer", nullable: false),
                    Inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantoes_Profissionais_ProfissionalResponsavelId",
                        column: x => x.ProfissionalResponsavelId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plantoes_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrocaPlantoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantaoId = table.Column<int>(type: "integer", nullable: false),
                    SolicitanteId = table.Column<int>(type: "integer", nullable: false),
                    DestinatarioId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrocaPlantoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrocaPlantoes_Plantoes_PlantaoId",
                        column: x => x.PlantaoId,
                        principalTable: "Plantoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrocaPlantoes_Profissionais_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrocaPlantoes_Profissionais_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Historico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrocaPlantaoId = table.Column<int>(type: "integer", nullable: false),
                    Evento = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historico_TrocaPlantoes_TrocaPlantaoId",
                        column: x => x.TrocaPlantaoId,
                        principalTable: "TrocaPlantoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historico_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historico_TrocaPlantaoId",
                table: "Historico",
                column: "TrocaPlantaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Historico_UsuarioId",
                table: "Historico",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantoes_ProfissionalResponsavelId",
                table: "Plantoes",
                column: "ProfissionalResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantoes_SetorId",
                table: "Plantoes",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UserId",
                table: "Profissionais",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrocaPlantoes_DestinatarioId",
                table: "TrocaPlantoes",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TrocaPlantoes_PlantaoId",
                table: "TrocaPlantoes",
                column: "PlantaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrocaPlantoes_SolicitanteId",
                table: "TrocaPlantoes",
                column: "SolicitanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historico");

            migrationBuilder.DropTable(
                name: "TrocaPlantoes");

            migrationBuilder.DropTable(
                name: "Plantoes");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
