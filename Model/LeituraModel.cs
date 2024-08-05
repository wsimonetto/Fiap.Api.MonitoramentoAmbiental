using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fiap.Api.MonitoramentoAmbiental.Model
{
    public class LeituraModel
    {
        [Column("id_leitura")]
        public int LeituraId { get; set; }

        [Column("valor")]
        public float Valor { get; set; }

        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        [Column("id_sensor")]
        public int SensorId { get; set; }

        [JsonIgnore]
        public SensorModel? Sensor { get; set; }
    }

}
