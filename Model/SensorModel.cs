using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.MonitoramentoAmbiental.Model
{
    public class SensorModel
    {
        [Column("id_sensor")] // Mapeia a propriedade para a coluna "id_sensor"
        public int SensorId { get; set; }

        [Column("tipo_sensor")] // Mapeia a propriedade para a coluna "tipo_sensor"
        public string? TipoSensor { get; set; }

        public string? Localizacao { get; set; } // Mapeia para a coluna "localizacao" por convenção

        public ICollection<LeituraModel> Leitura { get; set; } // Relacionamento OneToMany co

    }

} //FIM
