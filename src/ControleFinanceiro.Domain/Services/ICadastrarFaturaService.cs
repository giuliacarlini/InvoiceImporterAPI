using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Services
{
    public interface ICadastrarFaturaService
    {
        int ImportarArquivo(string CaminhoArquivo, DateTime Vencimento, TipoImportacao TipoImportacao, List<Lancamento> listaImportacao);
    }
}
