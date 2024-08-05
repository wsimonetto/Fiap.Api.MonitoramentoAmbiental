using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public class PrevisaoChuvaRepository : IPrevisaoChuvaRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PrevisaoChuvaRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<PrevisaoChuvaModel> GetAll()
        {
            return _databaseContext.PrevisoesChuva.ToList();
        }

        public IEnumerable<PrevisaoChuvaModel> GetAllReference(int lastReference, int pageSize)
        {
            var previsoesChuva = _databaseContext.PrevisoesChuva
                        .Where(c => c.PrevisaoChuvaId > lastReference)
                        .OrderBy(c => c.PrevisaoChuvaId)
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToList();
            return previsoesChuva;
        }

            public PrevisaoChuvaModel GetById(int id)
        {
            return _databaseContext.PrevisoesChuva.Find(id);
        }

        public void Add(PrevisaoChuvaModel previsaoChuva)
        {
            try
            {
                _databaseContext.Add(previsaoChuva);
                _databaseContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Previsão de Chuva no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição da Previsão de Chuva.", ex);
            }
        }

        public void Update(PrevisaoChuvaModel previsaoChuva)
        {
            var existingPrevisaoChuva = _databaseContext.PrevisoesChuva.Find(previsaoChuva.PrevisaoChuvaId);

            if (existingPrevisaoChuva == null)
            {
                throw new ArgumentException("Previsão de Chuva especificada não existe.");
            }

            try
            {
                if (existingPrevisaoChuva != null)
                {
                    _databaseContext.Entry(existingPrevisaoChuva).CurrentValues.SetValues(previsaoChuva);
                    _databaseContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Previsao de Chuva no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição da Previsao de Chuva.", ex);
            }

        }

        public void Delete(PrevisaoChuvaModel previsaoChuva)
        {
            _databaseContext.Remove(previsaoChuva);
            _databaseContext.SaveChanges();
        }

    }

} //FIM
