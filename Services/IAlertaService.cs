using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface IAlertaService
    {

        IEnumerable<AlertaModel> ListarAlertas();
        IEnumerable<AlertaModel> ListarAlertasReference(int lastReference, int pageSize);
        AlertaModel ObterAlertasPorId(int id);
        void CriarAlerta(AlertaModel alerta);
        void AtualizarAlerta(AlertaModel alerta);
        void DeletarAlerta(int id);

    }

} //FIM
