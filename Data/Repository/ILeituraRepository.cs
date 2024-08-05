using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public interface ILeituraRepository
    {
        IEnumerable<LeituraModel> GetAll();
        IEnumerable<LeituraModel> GetAllReference(int lastReference, int pageSize);
        LeituraModel GetById(int id);
        void Add(LeituraModel leitura);
        void Update(LeituraModel leitura);
        void Delete(LeituraModel leitura);
    }

} //FIM
