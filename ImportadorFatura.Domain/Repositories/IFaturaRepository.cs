using ImportadorFatura.Domain.Entities;
using ImportadorFatura.Domain.Enum;

namespace ImportadorFatura.Domain.Repositories
{
    public interface IFaturaRepository
    {
        bool FaturaJaExiste(DateTime vencimento, ETipoImportacao tipoImportacao, string NomeArquivo);

        void CriarFatura(Fatura fatura);
    }
}
