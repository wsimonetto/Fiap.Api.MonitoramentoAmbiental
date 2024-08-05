using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface IControleIrrigacaoService
    {
        IEnumerable<ControleIrrigacaoModel> ListarControlesIrrigacoes();
        IEnumerable<ControleIrrigacaoModel> ListarControlesIrrigacoesReference(int lastReference, int pageSize);
        ControleIrrigacaoModel ObterControleIrrigacaoPorId(int id);
        void CriarControleIrrigacao(ControleIrrigacaoModel controleIrrigacao);
        void AtualizarControleIrrigacao(ControleIrrigacaoModel controleIrrigacaoleitura);
        void DeletarControleIrrigacao(int id);

    }

} //FIM
