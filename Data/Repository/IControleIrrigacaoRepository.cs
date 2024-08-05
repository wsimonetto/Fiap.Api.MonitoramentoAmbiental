using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public interface IControleIrrigacaoRepository
    {
        IEnumerable<ControleIrrigacaoModel> GetAll();
        IEnumerable<ControleIrrigacaoModel> GetAllReference(int lastReference, int pageSize);
        ControleIrrigacaoModel GetById(int id);
        void Add(ControleIrrigacaoModel controleIrrigacao);
        void Update(ControleIrrigacaoModel controleIrrigacao);
        void Delete(ControleIrrigacaoModel controleIrrigacao);

    }

} //FIM
