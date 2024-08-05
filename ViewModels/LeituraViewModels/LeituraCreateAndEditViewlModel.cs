using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.LeituraViewModels
{
    public class LeituraCreateAndEditViewlModel
    {
        public int LeituraId { get; set; }
        public float Valor { get; set; }
        public DateTime DataHora { get; set; }
        public int SensorId { get; set; }

    }

} //FIM
