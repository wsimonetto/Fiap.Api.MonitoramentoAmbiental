namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.ControleIrrigacaoViewModels
{
    public class ControleIrrigacaoPaginacaoReferenceViewModel
    {
        public IEnumerable<ControleIrrigacaoAllViewModel> ControleIrrigacoes { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/ControleIrrigacao?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/ControleIrrigacao?referencia={NextRef}&tamanho={PageSize}" : "";
    }
}
