using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels.AlertaViewModels
{
    public class AlertaPaginacaoReferenceViewModel
    {
        public IEnumerable<AlertaAllViewModel> Alertas { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Alerta?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Alerta?referencia={NextRef}&tamanho={PageSize}" : "";
    }

} //FIM
