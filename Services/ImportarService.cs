using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Interface;
using ControleFinanceiroAPI.Model;
using ControleFinanceiroAPI.Repositories.Interface;
using ControleFinanceiroAPI.Utils;

namespace ControleFinanceiroAPI.Services
{
    public class ImportarService : IImportar
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFaturaRepository _faturaRepository;        
        private readonly IImportacaoRepository _importacaoRepository;
        private readonly ILancamentoRepository _lancamentosRepository;

        public ImportarService(IUnitOfWork unitOfWork,
                               IFaturaRepository faturaRepository,
                               IImportacaoRepository importacaoRepository,
                               ILancamentoRepository lancamentosRepository) 
        {          
            _unitOfWork = unitOfWork;
            _faturaRepository = faturaRepository;
            _importacaoRepository = importacaoRepository;
            _lancamentosRepository = lancamentosRepository;
        }

        public bool ImportarArquivo(string CaminhoArquivo, DateTime Vencimento, TipoImportacao TipoImportacao)
        {            
            try
            {
                _unitOfWork.BeginTransaction();
                int idFatura = AdicionarFatura(Vencimento, TipoImportacao);

                if (idFatura <= 0)
                    return false;

                var data = File.ReadLines(CaminhoArquivo);
                var listaImportacao = TransformarLinhasEmObjeto(data, TipoImportacao);

                foreach (var importacao in listaImportacao)
                {
                    if (DateTime.TryParse(importacao.date, out DateTime dataConvertida) == false)
                        continue;

                    importacao.IdFatura = idFatura;

                    AdicionarImportacao(importacao);

                    if ((importacao.IdImportacao > 0) && (float.Parse(importacao.amount) > 0))
                    {                        
                        AdicionarLancamento(importacao);
                    }
                }

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
                _unitOfWork.Rollback();
                return false;
            }
        }

        private List<Importacao> TransformarLinhasEmObjeto(IEnumerable<string> data, TipoImportacao tipoImportacao)
        {
            List<Importacao> listaImportacao = new();

            foreach (var line in data)
            {
                switch (tipoImportacao)
                {
                    case TipoImportacao.Nubank:
                        var lineSplit = line.Split(",");

                        listaImportacao.Add(
                            new Importacao() {
                                date = lineSplit[0],
                                category = lineSplit[1],
                                title = lineSplit[2],
                                amount = lineSplit[3],
                                IdOrigemImportacao = (int)tipoImportacao,
                                DataHoraImportacao = DateTime.Now
                            });
                        break;
                }
            }

            return listaImportacao;
        }

        private void AdicionarLancamento(Importacao importacao)
        {
            _lancamentosRepository.Save(new Lancamento()
            {
                Data = DateTime.Parse(importacao.date),
                Categoria = importacao.category,
                Descricao = importacao.title,
                Valor = float.Parse(importacao.amount),
                Parcelado = LocalizarParcela(importacao, false) != "",
                Parcela = LocalizarParcela(importacao, false),
                TotalParcela = LocalizarParcela(importacao, true),
                IdImportacao = importacao.IdImportacao
            });
        }

        private void AdicionarImportacao(Importacao importacao)
        {
            importacao.IdImportacao = _importacaoRepository.Save(importacao);
        }

        private int AdicionarFatura(DateTime vencimento, TipoImportacao tipoImportacao)
        {
            return _faturaRepository.Save(new Fatura()
                {
                    IdOrigem = (int)tipoImportacao,
                    Vencimento = vencimento,
                    DataHoraCadastro = DateTime.Now
                });           
        }

        private string LocalizarParcela(Importacao file, bool TotalParcela)
        {
            try
            {
                if (file.title.IndexOf("/") > 0)
                {
                    string[] retornoSplit = file.title.Split(' ');

                    foreach (string s in retornoSplit)
                    {
                        if (s.Contains("/"))
                        {
                            var resultado = TotalParcela ? s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/")) : s.Substring(0, s.IndexOf("/"));

                            return int.Parse(resultado).ToString();
                        }
                    }
                }

                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
