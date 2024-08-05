using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public interface IAlertaRepository
    {
        IEnumerable<AlertaModel> GetAll();
        IEnumerable<AlertaModel> GetAllReference(int lastReference, int pageSize);
        AlertaModel GetById(int id);
        void Add(AlertaModel alerta);
        void Update(AlertaModel alerta);
        void Delete(AlertaModel alerta);
    }

} //FIM
