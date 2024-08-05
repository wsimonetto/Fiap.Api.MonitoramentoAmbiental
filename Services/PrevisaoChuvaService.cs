using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;
using System.Drawing;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class PrevisaoChuvaService : IPrevisaoChuvaService
    {
        private readonly IPrevisaoChuvaRepository _previsaoChuvaRepository;

        public PrevisaoChuvaService(IPrevisaoChuvaRepository previsaoChuvaRepository)
        {
            _previsaoChuvaRepository = previsaoChuvaRepository;
        }

        public IEnumerable<PrevisaoChuvaModel> ListarPrevisoesChuva()
        {
            return _previsaoChuvaRepository.GetAll();
        }

        public IEnumerable<PrevisaoChuvaModel> ListarPrevisoesChuvaReference(int lastReference, int pageSize)
        {
            return _previsaoChuvaRepository.GetAllReference(lastReference, pageSize);
        }

        public PrevisaoChuvaModel ObterPrevisaoChuvaPorId(int id)
        {
            return _previsaoChuvaRepository.GetById(id);
        }

        public void CriarPrevisaoChuva(PrevisaoChuvaModel previsaoChuva)
        {
            _previsaoChuvaRepository.Add(previsaoChuva);
        }

        public void AtualizarPrevisaoChuva(PrevisaoChuvaModel previsaoChuva)
        {
            _previsaoChuvaRepository.Update(previsaoChuva);
        }

        public void DeletarPrevisaoChuva(int id)
        {
            var previsaoChuva = _previsaoChuvaRepository.GetById(id);
            if (previsaoChuva == null)
            {
                _previsaoChuvaRepository.Delete(previsaoChuva);
            }
        }

    }

} //FIM
