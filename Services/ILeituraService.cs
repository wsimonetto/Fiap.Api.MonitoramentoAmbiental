using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface ILeituraService
    {
        IEnumerable<LeituraModel> ListarLeituras();
        IEnumerable<LeituraModel> ListarLeiturasReference(int lastReference, int pageSize);
        LeituraModel ObterLeituraPorId(int id);
        void CriarLeitura(LeituraModel leitura);
        void AtualizarLeitura(LeituraModel leitura);
        void DeletarLeitura(int id);
    }

} //FIM
