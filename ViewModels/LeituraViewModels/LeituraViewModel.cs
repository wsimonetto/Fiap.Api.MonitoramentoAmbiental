using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.LeituraViewModels
{
    public class LeituraViewModel
    {
        public int LeituraId { get; set; }
        public float Valor { get; set; }
        public DateTime DataHora { get; set; }
        public int SensorId { get; set; }
        public string TipoSensor { get; set; }
        public string Localizacao { get; set; }

    }

} //FIM
