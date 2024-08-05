using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class ControleIrrigacaoService : IControleIrrigacaoService
    {
        private readonly IControleIrrigacaoRepository _controleIrrigacaoRepository;

        public ControleIrrigacaoService(IControleIrrigacaoRepository controleIrrigacaoRepository)
        {
            _controleIrrigacaoRepository = controleIrrigacaoRepository;
        }

        public IEnumerable<ControleIrrigacaoModel> ListarControlesIrrigacoes()
        {
            return _controleIrrigacaoRepository.GetAll();
        }

        public IEnumerable<ControleIrrigacaoModel> ListarControlesIrrigacoesReference(int lastReference, int pageSize)
        {
            return _controleIrrigacaoRepository.GetAllReference(lastReference, pageSize);
        }

        public ControleIrrigacaoModel ObterControleIrrigacaoPorId(int id)
        {
            return _controleIrrigacaoRepository.GetById(id);
        }

        public void CriarControleIrrigacao(ControleIrrigacaoModel controleIrrigacao)
        {
            _controleIrrigacaoRepository.Add(controleIrrigacao);
        }

        public void AtualizarControleIrrigacao(ControleIrrigacaoModel controleIrrigacaoleitura)
        {
            _controleIrrigacaoRepository.Update(controleIrrigacaoleitura);
        }

        public void DeletarControleIrrigacao(int id)
        {
            var controleIrrigacao = _controleIrrigacaoRepository.GetById(id);
            if (controleIrrigacao != null)
            {
                _controleIrrigacaoRepository.Delete(controleIrrigacao);
            }
        }

    }

} //FIM
