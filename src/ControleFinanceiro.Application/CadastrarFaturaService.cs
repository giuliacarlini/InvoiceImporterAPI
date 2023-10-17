using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Adapters.Repository;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;

namespace ControleFinanceiro.Application
{
    public class CadastrarFaturaService : ICadastrarFaturaService
    {

        private readonly IFaturaRepository _faturaRepository;
        private readonly ILancamentoImportacaoRepository _importacaoRepository;
        private readonly ILancamentoRepository _lancamentosRepository;

        public CadastrarFaturaService(
                               IFaturaRepository faturaRepository,
                               ILancamentoImportacaoRepository importacaoRepository,
                               ILancamentoRepository lancamentosRepository)
        {

            _faturaRepository = faturaRepository;
            _importacaoRepository = importacaoRepository;
            _lancamentosRepository = lancamentosRepository;
        }

        public int ImportarArquivo(string caminhoArquivo, DateTime vencimento, TipoImportacao tipoImportacao, List<Lancamento> listaImportacao)
        {
            try
            {
            //    var Fatura = _faturaRepository.Adicionar(new Fatura((int)tipoImportacao, 
            //                                                   vencimento, 
            //                                                   DateTime.Now,
            //                                                   Path.GetFileName(caminhoArquivo)));
            //
            //    if (Fatura == null)
            //        throw new Exception("Erro ao adicionar fatura!");
            //
            //    foreach (var importacao in listaImportacao)
            //    {
            //        if (DateTime.TryParse(importacao.Data, out DateTime dataConvertida) == false)
            //            continue;
            //
            //        var idImportacao = _importacaoRepository.Adicionar(importacao);
            //
            //        if (idImportacao > 0)
            //        {
            //            AdicionarLancamento(importacao, idImportacao);
            //        }
            //    }
            //
            //    return Fatura.IdFatura;

            return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
