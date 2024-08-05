using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.MonitoramentoAmbiental.Model
{
    public class PrevisaoChuvaModel
    {
        [Column("id_previsao_chuva")] // Mapeia a propriedade para a coluna "id_previsao"
        public int PrevisaoChuvaId { get; set; }

        [Column("data_previsao")] // Mapeia a propriedade para a coluna "data_previsao"
        public DateTime DataPrevisaoChuva { get; set; }

        public string? Previsao { get; set; }
    }

} //FIM
