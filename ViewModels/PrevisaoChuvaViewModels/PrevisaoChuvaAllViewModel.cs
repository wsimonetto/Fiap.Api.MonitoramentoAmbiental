using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.PrevisaoChuvaViewModels
{
    public class PrevisaoChuvaAllViewModel
    {
        public int PrevisaoChuvaId { get; set; }
        public DateTime DataPrevisaoChuva { get; set; }
        public string? Previsao { get; set; }

    }

} //FIM
