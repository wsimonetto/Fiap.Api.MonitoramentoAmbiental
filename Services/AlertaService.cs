using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;

        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public IEnumerable<AlertaModel> ListarAlertas()
        {
            return _alertaRepository.GetAll();
        }

        public IEnumerable<AlertaModel> ListarAlertasReference(int lastReference, int pageSize)
        {
            return _alertaRepository.GetAllReference(lastReference, pageSize);
        }

        public AlertaModel ObterAlertasPorId(int id)
        {
            return _alertaRepository.GetById(id);
        }
        public void CriarAlerta(AlertaModel alerta)
        {
            _alertaRepository.Add(alerta);
        }

        public void AtualizarAlerta(AlertaModel alerta)
        {
            _alertaRepository.Update(alerta);
        }

        public void DeletarAlerta(int id)
        {
            var alerta = _alertaRepository.GetById(id);
            if (alerta != null)
            {
                _alertaRepository.Delete(alerta);
            }
        }

    }

}
