using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public class LeituraRepository : ILeituraRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISensorRepository _sensorRepository;


        public LeituraRepository(DatabaseContext databaseContext, ISensorRepository sensorRepository)
        {
            _databaseContext = databaseContext;
            _sensorRepository = sensorRepository;
        }

        public IEnumerable<LeituraModel> GetAll()
        {
            return _databaseContext.Leituras.Include(c => c.Sensor).ToList();
        }

        public IEnumerable<LeituraModel> GetAllReference(int lastReference, int pageSize)
        {
            var leituras = _databaseContext.Leituras.Include(x => x.Sensor)
                .Where(c => c.LeituraId > lastReference)
                .OrderBy(c => c.LeituraId)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

                return leituras;
        }

        public LeituraModel GetById(int id)
        {
            return _databaseContext.Leituras.Include(x => x.Sensor).FirstOrDefault(x => x.LeituraId == id);
        }

        public void Add(LeituraModel leitura)
        {
            //verificando se sensor existe
            var sensor = _sensorRepository.GetById(leitura.SensorId);

            if (sensor == null)
            {
                throw new ArgumentException("Sensor especificado não existe..");
                //vai retornar para o controller no post
            }

            try
            {
                _databaseContext.Add(leitura);
                _databaseContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Leitura no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição da Leitura.", ex);
            }

        }

        public void Update(LeituraModel leitura)
        {
            var sensor = _sensorRepository.GetById(leitura.SensorId);
            var existingLeitura = _databaseContext.Leituras.Find(leitura.LeituraId);

            if (sensor == null)
            {
                throw new ArgumentException("Sensor especificado não existe.");
            }

            try
            {
                if (existingLeitura != null)
                {
                    _databaseContext.Entry(existingLeitura).CurrentValues.SetValues(leitura);
                    _databaseContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Leitura no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição da Leitura.", ex);
            }

        }

        public void Delete(LeituraModel leitura)
        {
            _databaseContext.Remove(leitura);
            _databaseContext.SaveChanges();
        }

       
    }

} //FIM
