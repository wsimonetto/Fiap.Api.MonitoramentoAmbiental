using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AlertaRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<AlertaModel> GetAll()
        {
            return _databaseContext.Alertas.ToList();
        }

        public IEnumerable<AlertaModel> GetAllReference()
        {
            return _databaseContext.Alertas.ToList();
        }

        public IEnumerable<AlertaModel> GetAllReference(int lastReference, int pageSize)
        {
            var alertas = _databaseContext.Alertas
                        .Where(c => c.AlertaId > lastReference)
                        .OrderBy(c => c.AlertaId)
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToList();
            return alertas;
        }

        public AlertaModel GetById(int id)
        {
            return _databaseContext.Alertas.Find(id);
        }

        public void Add(AlertaModel alerta)
        {
            _databaseContext.Add(alerta);
            _databaseContext.SaveChanges();
        }

        public void Update(AlertaModel alerta)
        {
            var existingAlerta = _databaseContext.Alertas.Find(alerta.AlertaId);

            if (existingAlerta != null)
            {
                _databaseContext.Entry(existingAlerta).CurrentValues.SetValues(alerta);
                _databaseContext.SaveChanges();
            }
        }
        public void Delete(AlertaModel alerta)
        {
            _databaseContext.Remove(alerta);
            _databaseContext.SaveChanges();
        }
    }

} //FIM
