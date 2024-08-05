using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface IPrevisaoChuvaService
    {
        IEnumerable<PrevisaoChuvaModel> ListarPrevisoesChuva();
        IEnumerable<PrevisaoChuvaModel> ListarPrevisoesChuvaReference(int lastReference, int pageSize);
        PrevisaoChuvaModel ObterPrevisaoChuvaPorId(int id);
        void CriarPrevisaoChuva(PrevisaoChuvaModel previsaoChuva);
        void AtualizarPrevisaoChuva(PrevisaoChuvaModel previsaoChuva);
        void DeletarPrevisaoChuva(int id);

    }

} //FIM
