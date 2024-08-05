using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public class ControleIrrigacaoRepository : IControleIrrigacaoRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IPrevisaoChuvaRepository _previsaoChuvaRepository;

        public ControleIrrigacaoRepository(DatabaseContext databaseContext, IPrevisaoChuvaRepository previsaoChuvaRepository)
        {
            _databaseContext = databaseContext;
            _previsaoChuvaRepository = previsaoChuvaRepository;

        }

        public IEnumerable<ControleIrrigacaoModel> GetAll()
        {
            return _databaseContext.ControleIrrigacoes.ToList();
        }

        public IEnumerable<ControleIrrigacaoModel> GetAllReference(int lastReference, int pageSize)
        {
            var controleIrrigacoes = _databaseContext.ControleIrrigacoes
               .Where(c => c.ControleId > lastReference)
               .OrderBy(c => c.ControleId)
               .Take(pageSize)
               .AsNoTracking()
               .ToList();

            return controleIrrigacoes;
        }

        public ControleIrrigacaoModel GetById(int id)
        {
            return _databaseContext.ControleIrrigacoes.Find(id);
        }

        public void Add(ControleIrrigacaoModel controleIrrigacao)
        {
            var previsaoChuva = _previsaoChuvaRepository.GetById(controleIrrigacao.PrevisaoChuvaId);

            if (previsaoChuva == null)
            {
                throw new ArgumentException("Previsão de Chuva especificada não existe..");
            }

            try
            {
                _databaseContext.Add(controleIrrigacao);
                _databaseContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Controle de Irrigação no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição do Controle de Irrigação.", ex);
            }

        }

        public void Update(ControleIrrigacaoModel controleIrrigacao)
        {
            var previsaoChuva = _previsaoChuvaRepository.GetById(controleIrrigacao.PrevisaoChuvaId);
            var existingControleIrrigacao = _databaseContext.ControleIrrigacoes.Find(controleIrrigacao.ControleId);

            if (previsaoChuva == null)
            {
                throw new ArgumentException("Previsão de Chuva especificada não existe.");
            }

            try
            {
                if (existingControleIrrigacao != null)
                {
                    _databaseContext.Entry(existingControleIrrigacao).CurrentValues.SetValues(controleIrrigacao);
                    _databaseContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception("Erro ao salvar a Controle de Irrigação no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a operação de adição do Controle de Irrigação.", ex);
            }

        }

        public void Delete(ControleIrrigacaoModel controleIrrigacao)
        {
            _databaseContext.Remove(controleIrrigacao);
            _databaseContext.SaveChanges();
        }

    }

} //FIM
