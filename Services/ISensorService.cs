using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface ISensorService
    {
        IEnumerable<SensorModel> ListarSensores();
        IEnumerable<SensorModel> ListarSensoresReference(int pageNumber, int pageSize);
        SensorModel ObterSensorPorId(int id);
        void CriarSensor(SensorModel sensor);
        void AtualizarSensor(SensorModel sensor);
        void DeletarSensor(int id);
    }

} //FIM
