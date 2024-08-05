using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;
using System.Collections.Generic;

namespace Fiap.Api.MonitoramentoAmbiental.ViewModels
{
    public class SensorPaginacaoReferenceViewModel
    {
        public IEnumerable<SensorAllViewModel> Sensores { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Sensor?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Sensor?referencia={NextRef}&tamanho={PageSize}" : "";
    }

}
