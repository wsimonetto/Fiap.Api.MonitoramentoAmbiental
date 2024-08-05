namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.LeituraViewModels
{
    public class LeituraPaginacaoReferenceViewModel
    {
        public IEnumerable<LeituraViewModel> Leituras { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Leitura?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Leitura?referencia={NextRef}&tamanho={PageSize}" : "";
    }

} //FIM
