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
            migrationBuilder.CreateTable(
                name: "Estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimentos", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO \"Estabelecimentos\" (\"Nome\") VALUES ('HBU');");

            migrationBuilder.AddColumn<long>(
                name: "EstabelecimentoId",
                table: "Setores",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_Setores_EstabelecimentoId",
                table: "Setores",
                column: "EstabelecimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setores_Estabelecimentos_EstabelecimentoId",
                table: "Setores",
                column: "EstabelecimentoId",
                principalTable: "Estabelecimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Setores_Estabelecimentos_EstabelecimentoId",
                table: "Setores");

            migrationBuilder.DropIndex(
                name: "IX_Setores_EstabelecimentoId",
                table: "Setores");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoId",
                table: "Setores");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");
        }
    }
}