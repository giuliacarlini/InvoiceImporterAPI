using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.ValueObjects;
using Flunt.Notifications;

namespace ControleFinanceiro.Domain.Entities
{
    public class Fatura : Notifiable
    {
        private List<Lancamento> _lancamentos;

        public Fatura(TipoImportacao tipoImportacao, DateTime vencimento, CaminhoArquivo caminhoArquivo)
        {
            IdFatura = Guid.NewGuid();
            TipoImportacao = tipoImportacao;
            Vencimento = vencimento;
            DataHoraCadastro = DateTime.Now;
            CaminhoArquivo = caminhoArquivo;
            _lancamentos = new List<Lancamento>();

            AddNotifications(caminhoArquivo);      
        }

        public Guid IdFatura { get; private set; }

        public TipoImportacao TipoImportacao { get; private set; }

        public DateTime Vencimento { get; private set; }

        public DateTime DataHoraCadastro { get; private set; }

        public CaminhoArquivo CaminhoArquivo { get; private set; }

        public IReadOnlyCollection<Lancamento>? Lancamentos { get { return _lancamentos.ToArray(); } }

        public void AdicionarLancamento(Lancamento lancamento)
        {
            _lancamentos.Add(lancamento);
        }

        public void LerArquivoCSV()
        {
            List<string> arquivo = File.ReadLines(CaminhoArquivo.Diretorio + CaminhoArquivo.Nome).ToList();

            foreach (var linhas in arquivo.Skip(1))
            {
                var lancamento = new Lancamento(TipoImportacao, linhas);
                AdicionarLancamento(lancamento);
            }            
        }
    }
}
