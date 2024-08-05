using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.MonitoramentoAmbiental.Migrations
{
    /// <inheritdoc />
    public partial class AddApiMonitoramentoAmbiental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipo_alerta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.id_alerta);
                });

            migrationBuilder.CreateTable(
                name: "PrevisoesChuva",
                columns: table => new
                {
                    id_previsao_chuva = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_previsao = table.Column<DateTime>(type: "date", nullable: false),
                    previsao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevisoesChuva", x => x.id_previsao_chuva);
                });

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipo_sensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.id_sensor);
                });

            migrationBuilder.CreateTable(
                name: "ControleIrrigacoes",
                columns: table => new
                {
                    id_controle = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "date", nullable: false),
                    id_previsao_chuva = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleIrrigacoes", x => x.id_controle);
                    table.ForeignKey(
                        name: "FK_ControleIrrigacoes_PrevisoesChuva_id_previsao_chuva",
                        column: x => x.id_previsao_chuva,
                        principalTable: "PrevisoesChuva",
                        principalColumn: "id_previsao_chuva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leituras",
                columns: table => new
                {
                    id_leitura = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    valor = table.Column<decimal>(type: "float(126)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "date", nullable: false),
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leituras", x => x.id_leitura);
                    table.ForeignKey(
                        name: "FK_Leituras_Sensores_id_sensor",
                        column: x => x.id_sensor,
                        principalTable: "Sensores",
                        principalColumn: "id_sensor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControleIrrigacoes_id_previsao_chuva",
                table: "ControleIrrigacoes",
                column: "id_previsao_chuva");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_id_sensor",
                table: "Leituras",
                column: "id_sensor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "ControleIrrigacoes");

            migrationBuilder.DropTable(
                name: "Leituras");

            migrationBuilder.DropTable(
                name: "PrevisoesChuva");

            migrationBuilder.DropTable(
                name: "Sensores");
        }
    }
}
