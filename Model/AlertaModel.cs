using System.ComponentModel.DataAnnotations.Schema;
using Fiap.Api.MonitoramentoAmbiental.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Model
{
    public class AlertaModel
    {
            [Column("id_alerta")] // Mapeia a propriedade para a coluna "id_alerta"
            public int AlertaId { get; set; }

            [Column("tipo_alerta")] // Mapeia a propriedade para a coluna "tipo_alerta"
            public string? TipoAlerta { get; set; }

            public string? Descricao { get; set; } // Correspondente a "private String descricao;" em Java

            [Column("data_hora")] // Mapeia a propriedade para a coluna "data_hora"
            public DateTime DataHora { get; set; } // Correspondente a "private Timestamp dataHora;" em Java
        }

    } //FIM

