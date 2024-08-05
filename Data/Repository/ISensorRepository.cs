using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public interface ISensorRepository
    {
        IEnumerable<SensorModel> GetAll();
        IEnumerable<SensorModel> GetAllReference(int lastReference, int pageSize);
        SensorModel GetById(int id);
        void Add(SensorModel sensor);
        void Update(SensorModel sensor);
        void Delete(SensorModel sensor);

    }

} //FIM
