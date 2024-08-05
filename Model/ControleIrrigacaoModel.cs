using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.MonitoramentoAmbiental.Model
{
    public class ControleIrrigacaoModel
    {
        [Column("id_controle")] // Mapeia a propriedade para a coluna "id_controle"
        public int ControleId { get; set; }

        public string? Localizacao { get; set; }

        public string? Estado { get; set; }

        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        public int PrevisaoChuvaId { get; set; } // Adiciona a propriedade PrevisaoChuvaId

        [ForeignKey("PrevisaoChuvaId")]
        public PrevisaoChuvaModel PrevisaoChuva { get; set; } // Mapeia a propriedade

    }

} //FIM
