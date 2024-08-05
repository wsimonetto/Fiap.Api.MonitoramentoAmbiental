namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.ControleIrrigacaoViewModels
{
    public class ControleIrrigacaoAllViewModel
    {
        public int ControleId { get; set; }
        public int? PrevisaoChuvaId { get; set; }
        public string Localizacao { get; set; }
        public string Estado { get; set; }
        public DateTime DataHora { get; set; }

    }

} //FIM
