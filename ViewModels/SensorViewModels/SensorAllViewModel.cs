using Fiap.Api.MonitoramentoAmbiental.Model;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels
{
    public class SensorAllViewModel
    {
        public int SensorId { get; set; }
        public string? TipoSensor { get; set; }
        public string? Localizacao { get; set; }

    }

} //FIM
