using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;
using System.Drawing;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class LeituraService : ILeituraService
    {
        private readonly ILeituraRepository _leituraRepository;

        public LeituraService(ILeituraRepository leituraRepository)
        {
            _leituraRepository = leituraRepository;
        }

        public IEnumerable<LeituraModel> ListarLeituras()
        {
            return _leituraRepository.GetAll();
        }


        IEnumerable<LeituraModel> ILeituraService.ListarLeiturasReference(int lastReference, int pageSize)
        {
            return _leituraRepository.GetAllReference(lastReference, pageSize);
        }

        public LeituraModel ObterLeituraPorId(int id)
        {
            return _leituraRepository.GetById(id);
        }

        public void CriarLeitura(LeituraModel leitura)
        {
            _leituraRepository.Add(leitura);
        }

        public void AtualizarLeitura(LeituraModel leitura)
        {
            _leituraRepository.Update(leitura);
        }

        public void DeletarLeitura(int id)
        {
            var leitura = _leituraRepository.GetById(id);
            if (leitura != null)
            {
                _leituraRepository.Delete(leitura);
            }
        }

    }

} //FIM
