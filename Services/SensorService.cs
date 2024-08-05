using Fiap.Api.MonitoramentoAmbiental.Data;
using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly DatabaseContext _databaseContext;


        public SensorService(ISensorRepository sensorRepository, DatabaseContext databaseContext)
        {
            _sensorRepository = sensorRepository;
            _databaseContext = databaseContext;
        }

        public IEnumerable<SensorModel> ListarSensores()
        {
            return _sensorRepository.GetAll();
        }

        public IEnumerable<SensorModel> ListarSensoresReference(int lastReference, int pageSize)
        {
            return _sensorRepository.GetAllReference(lastReference, pageSize);
        }

        public SensorModel ObterSensorPorId(int id)
        {
            return _sensorRepository.GetById(id);
        }

        public void CriarSensor(SensorModel sensor)
        {
            _sensorRepository.Add(sensor);
        }

        public void AtualizarSensor(SensorModel sensor)
        {
            _sensorRepository.Update(sensor);
        }

        public void DeletarSensor(int id)
        {
            var sensor = _sensorRepository.GetById(id);
            if (sensor != null)
            {
                _sensorRepository.Delete(sensor);
            }
        }

    }

} //FIM
