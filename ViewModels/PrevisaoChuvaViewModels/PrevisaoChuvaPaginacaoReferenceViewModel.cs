namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.PrevisaoChuvaViewModels
{
    public class PrevisaoChuvaPaginacaoReferenceViewModel
    {
        public IEnumerable<PrevisaoChuvaAllViewModel> PrevisoesChuva { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/PrevisaoChuva?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/PrevisaoChuva?referencia={NextRef}&tamanho={PageSize}" : "";
    }

} //FIM
