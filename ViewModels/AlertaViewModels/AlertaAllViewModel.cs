using System;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels
{
    public class AlertaAllViewModel
    {
        public int AlertaId { get; set; }
        public string? TipoAlerta { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataHora { get; set; }

    }

} //FIM
