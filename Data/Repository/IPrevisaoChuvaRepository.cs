using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Data.Repository
{
    public interface IPrevisaoChuvaRepository
    {
        IEnumerable<PrevisaoChuvaModel>GetAll();
        IEnumerable<PrevisaoChuvaModel> GetAllReference(int lastReference, int pageSize);
        PrevisaoChuvaModel GetById(int id);
        void Add(PrevisaoChuvaModel previsaoChuva);
        void Update(PrevisaoChuvaModel previsaoChuva);
        void Delete(PrevisaoChuvaModel previsaoChuva);

    }

} //FIM
