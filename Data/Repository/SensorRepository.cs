using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public class SensorRepository : ISensorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public SensorRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext; ;
        }
        public IEnumerable<SensorModel> GetAll()
        {
            return _databaseContext.Sensores.ToList();
        }

        public IEnumerable<SensorModel> GetAllReference(int lastReference, int pageSize)
        {
            var sensores = _databaseContext.Sensores
                        .Where(c => c.SensorId > lastReference)
                        .OrderBy(c => c.SensorId)
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToList();
            return sensores;
        }

        public SensorModel GetById(int id)
        {
            return _databaseContext.Sensores.FirstOrDefault(s => s.SensorId == id); // alterado para buscar sensor no Add Leitura repository
        }

        public void Add(SensorModel sensor)
        {
            _databaseContext.Add(sensor);
            _databaseContext.SaveChanges();
        }

        public void Update(SensorModel sensor)
        {
            var existingSensor = _databaseContext.Sensores.Find(sensor.SensorId);

            if (existingSensor != null)
            {
                _databaseContext.Entry(existingSensor).CurrentValues.SetValues(sensor);
                _databaseContext.SaveChanges();
            }
        }

        public void Delete(SensorModel sensor)
        {
            _databaseContext.Remove(sensor);
            _databaseContext.SaveChanges();
        }

    }

} //FIM
